using ge.singular.roulette;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RoulleteApi.Common;
using RoulleteApi.Core;
using RoulleteApi.Repository.Interfaces;
using RoulleteApi.Services.Interfaces;
using RoulleteApi.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RoulleteApi.Application
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IJackpotService _jackpotService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly int _amountPercentgeToPutInJackpot;
        private readonly string _JwtKey;
        private readonly int _tokenExpiresAfterInMinutes;

        private readonly TokenHelper _tokenHelper;

        public UserService(
            IConfiguration configuration,
            TokenHelper tokenHelper,
            SignInManager<User> signinManager,
            UserManager<User> userManager,
            IUserRepository userRepository,
           IJackpotService jackpotService
            )
        {
            _configuration = configuration;
            _tokenHelper = tokenHelper;
            _userManager = userManager;
            _signInManager = signinManager;
            _userRepository = userRepository;
            _jackpotService = jackpotService;
            _JwtKey = _configuration["Jwt:key"];
            _tokenExpiresAfterInMinutes = int.Parse(_configuration["Jwt:ExpiresAfterInMinutes"]);
            _amountPercentgeToPutInJackpot = int.Parse(_configuration["Jackpot:AmountPercentgeToPutInJackpot"]);
        }

        public ServiceResponse<List<GameHistoryModel>> GetHistory(Guid userId)
        {
            if (_userRepository.NotExists(userId))
            {
                return UserNotFoundResponse<List<GameHistoryModel>>(userId);
            }

            var user = _userRepository.GetById(userId, nameof(User.GameHistories));

            var list = user.GameHistories.Select(c =>
            new GameHistoryModel(c.Id, c.SpinId, c.BetString, c.WinningNumber, c.BetAmountInCents, c.WonAmountInCents, c.CreatedAt, c.UpdatedAt))
                .OrderByDescending(c => c.CreatedAt).ToList();

            return new ServiceResponse<List<GameHistoryModel>>().Ok(list);
        }

        public ServiceResponse<UserBalanceResponseModel> GetUserBalanceInCents(Guid userId)
        {
            if (_userRepository.NotExists(userId))
            {
                return UserNotFoundResponse<UserBalanceResponseModel>(userId);
            }

            var user = _userRepository.GetById(userId);

            return new ServiceResponse<UserBalanceResponseModel>().
                    Ok(new UserBalanceResponseModel(user.BalanceInCents));
        }

        public async Task<ServiceResponse<LoginResponseModel>> LoginAsync(string username, string password)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(username, password, false, false);

            if (!signInResult.Succeeded)
            {
                return new ServiceResponse<LoginResponseModel>()
                    .Fail(new ServiceErrorMessage()
                    .BadRequest("Incorrect username or password"));
            }

            var user = await _userManager.Users.SingleOrDefaultAsync(c => c.UserName == username);

            var userClaims = new Claim[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            var tokenExpiresAt = DateTime.Now.AddMinutes(_tokenExpiresAfterInMinutes);

            var token = _tokenHelper.CreateToken(_JwtKey, tokenExpiresAt,userClaims);

            return new ServiceResponse<LoginResponseModel>().Ok(new LoginResponseModel(token));
        }

        public async Task<ServiceResponse<MakeBetResponseModel>> MakeBetAsync(Guid userId, string betString)
        {

#if DEBUG
            Console.WriteLine($"New bet amount: { CheckBets.IsValid(betString).getBetAmount() } at {DateTime.Now.ToLongTimeString()}");
#endif

            // Checking if user exists for the current spacial case:
            // Imagine that I have logged in, got access token and started playing unethically.
            // I was got cought by administrators and disabled my user immediately (IsDeleted = true for now).
            // Without this check user, variable that I get below, will be null and nullreferance error will occure.

            if (_userRepository.NotExists(userId))
            {
                return UserNotFoundResponse<MakeBetResponseModel>(userId);
            }

            var user = _userRepository.GetById(userId, nameof(User.GameHistories));

            var isBetValidResponse = CheckBets.IsValid(betString);

            if (!isBetValidResponse.getIsValid())
            {
                return new ServiceResponse<MakeBetResponseModel>()
                       .Fail(new ServiceErrorMessage()
                       .InvalidValue("BetString"));
            }

            var betAmountInCents = isBetValidResponse.getBetAmount();

            if (user.BalanceInCents < betAmountInCents)
            {
                return new ServiceResponse<MakeBetResponseModel>()
                        .Fail(new ServiceErrorMessage()
                        .BadRequest("Not enough balance"));
            }

            user.SubtractBetAmountFromBalance(betAmountInCents);

            var betAmountInMillyCents = betAmountInCents.ConvertCentsToMillyCents();
            var amountToPutInJackpot = betAmountInMillyCents * _amountPercentgeToPutInJackpot / 100;
            var increaceJackpotResult = await _jackpotService.IncreaseJackpotAmountAsync(amountToPutInJackpot);

            if (increaceJackpotResult.Failed)
            {
                return new ServiceResponse<MakeBetResponseModel>()
                        .Fail(new ServiceErrorMessage()
                        .ChangesNotSaved("Something went wrong, could not update jackpot amount"));
            }

            var winningNumber = new Random().Next(0, 36);
            int wonAmountInCents = CheckBets.EstimateWin(betString, winningNumber);

            user.GameHistories.Add(new GameHistory(betString, winningNumber, betAmountInCents, wonAmountInCents));
            user.AddAmountToBalance(wonAmountInCents);

            _userRepository.Update(user);
            var rowsUpdated = await _userRepository.SaveChangesAsync();

            if (rowsUpdated == 0)
            {
                return new ServiceResponse<MakeBetResponseModel>()
                        .Fail(new ServiceErrorMessage()
                        .ChangesNotSaved("Something went wrong, could not update user data"));
            }

            return new ServiceResponse<MakeBetResponseModel>().Ok(new MakeBetResponseModel(wonAmountInCents));
        }

        private ServiceResponse<T> UserNotFoundResponse<T>(Guid userId)
            => new ServiceResponse<T>()
                .Fail(new ServiceErrorMessage()
                .NotFound(userId.ToString()));
    }
}
