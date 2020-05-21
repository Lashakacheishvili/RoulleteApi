using Microsoft.EntityFrameworkCore;
using RoulleteApi.Common;
using RoulleteApi.Core;
using RoulleteApi.Repository.Interfaces;
using RoulleteApi.Services.Interfaces;
using RoulleteApi.Services.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RoulleteApi.Application
{
    public class JackpotService : IJackpotService
    {
        private readonly IJackpotRepository _jackpotRepository;

        public JackpotService(IJackpotRepository jackpotRepository)
        {
            _jackpotRepository = jackpotRepository;
        }

        public async Task<ServiceResponse<JackpotModel>> GetAsync()
        {
            var jackpotResult = await GetJackpotAsync();

            if (jackpotResult.Failed)
            {
                return JackpotNotFoundResponse<JackpotModel>();
            }

            var jackpot = jackpotResult.Result;
            var jackpotModel = new JackpotModel(jackpot.Id, jackpot.AmountInMillyCents, jackpot.ConcurrencyStamp, jackpot.CreatedAt, jackpot.UpdatedAt);

            return new ServiceResponse<JackpotModel>()
                .Ok(jackpotModel);
        }

        // Handling of concurrency updates of jackpot amount is done using database concurrency stamp.
        // If concurrent update is detected concurrency error will be thrown and handled in the code.
        // In handle I mean, just refreshing current entity data and trying to update again untill concurrency
        // error stops to be thrown.
        // Another solution could be to store jackpot variable in memory of the server (not database, server memory is faster),
        // and write code   that would concurrently update that static variable.
        // It would not be enough, I would have still needed to update the database, that would be background service job,
        // to run every second and update database. So the delay could one second. If the server shuts down 1 second data would be lost.
        // But if we trust google, the second one is better solution, they are using it to count views in youtube. But views and jackpot amount
        // is not the same.

        public async Task<ServiceResponse> IncreaseJackpotAmountAsync(long amountInMillyCents)
        {
            var jackpot = await _jackpotRepository.GetAll().SingleOrDefaultAsync();

            if (jackpot == null)
            {
                return JackpotNotFoundResponse();
            }

            var updateJackpot = true;

            do
            {
                try
                {
                    jackpot.IncreaseJackpotAmount(amountInMillyCents);
                    _jackpotRepository.Update(jackpot);
                    var rowsUpdated = await _jackpotRepository.SaveChangesAsync();

                    updateJackpot = false;

                    if (rowsUpdated == 0)
                    {
                        return new ServiceResponse()
                               .Fail(new ServiceErrorMessage()
                               .ChangesNotSaved("Jackpot"));
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ex.Entries.Single().Reload();
                }

            } while (updateJackpot);

            return new ServiceResponse()
                .Ok();
        }

        private async Task<ServiceResponse<Jackpot>> GetJackpotAsync()
        {
            var jackpot = await _jackpotRepository.GetAll().SingleOrDefaultAsync();

            return jackpot == null ?
                JackpotNotFoundResponse<Jackpot>()
                : new ServiceResponse<Jackpot>()
                .Ok(jackpot);
        }

        private ServiceResponse<T> JackpotNotFoundResponse<T>()
            => new ServiceResponse<T>()
                     .Fail(new ServiceErrorMessage()
                     .NotFound("Jackpot"));

        private ServiceResponse JackpotNotFoundResponse()
            => new ServiceResponse()
                     .Fail(new ServiceErrorMessage()
                     .NotFound("Jackpot"));
    }
}
