using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoulleteApi.Common;
using RoulleteApi.Services.Interfaces;
using RoulleteApi.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoulleteApi.Controllers
{
    [Route("api/user/")]
    [ApiController]
    [Authorize]
    public class UserController : BaseAPiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("session")]
        [AllowAnonymous]
        [ProducesResponseType(400, Type = typeof(ServiceResponse))]
        [ProducesResponseType(200, Type = typeof(ServiceResponse<LoginResponseModel>))]
        public async Task<IActionResult> Login([FromBody]UserLoginModel model)
        => HandleResult(await _userService.LoginAsync(model.Username, model.Password));

        [HttpGet("balance")]
        [ProducesResponseType(400, Type = typeof(ServiceResponse))]
        [ProducesResponseType(401, Type = typeof(ServiceResponse))]
        [ProducesResponseType(404, Type = typeof(ServiceResponse))]
        [ProducesResponseType(200, Type = typeof(ServiceResponse<UserBalanceResponseModel>))]
        public IActionResult GetUserBalance()
        => HandleResult( _userService.GetUserBalanceInCents(UserId));

        [HttpPost("bet")]
        [ProducesResponseType(400, Type = typeof(ServiceResponse))]
        [ProducesResponseType(401, Type = typeof(ServiceResponse))]
        [ProducesResponseType(404, Type = typeof(ServiceResponse))]
        [ProducesResponseType(200, Type = typeof(ServiceResponse<MakeBetResponseModel>))]
        public async Task<IActionResult> MakeBet([FromBody]BetModel model)
            => HandleResult(await _userService.MakeBetAsync(UserId, model.BetString));

        [HttpGet("history")]
        [ProducesResponseType(400, Type = typeof(ServiceResponse))]
        [ProducesResponseType(401, Type = typeof(ServiceResponse))]
        [ProducesResponseType(404, Type = typeof(ServiceResponse))]
        [ProducesResponseType(200, Type = typeof(ServiceResponse<List<GameHistoryModel>>))]
        public IActionResult GetUserGameHistory()
            => HandleResult(_userService.GetHistory(UserId));
    }
}
