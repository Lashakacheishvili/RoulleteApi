using RoulleteApi.Common;
using RoulleteApi.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoulleteApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<LoginResponseModel>> LoginAsync(string username, string password);
        ServiceResponse<UserBalanceResponseModel> GetUserBalanceInCents(Guid userId);
        Task<ServiceResponse<MakeBetResponseModel>> MakeBetAsync(Guid guid, string betString);
        ServiceResponse<List<GameHistoryModel>> GetHistory(Guid userId);
    }
}
