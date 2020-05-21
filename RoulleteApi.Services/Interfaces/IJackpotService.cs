using RoulleteApi.Common;
using RoulleteApi.Services.Models;
using System.Threading.Tasks;

namespace RoulleteApi.Services.Interfaces
{
    public interface IJackpotService
    {
        Task<ServiceResponse<JackpotModel>> GetAsync();
        Task<ServiceResponse> IncreaseJackpotAmountAsync(long amountInCents);
    }
}
