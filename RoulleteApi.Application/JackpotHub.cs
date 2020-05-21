using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace RoulleteApi.Application
{
    public class JackpotHub : Hub
    {
        public async Task ShowJackpotAsync(long jackpotInMillyCents)
        {
            await Clients.All.SendAsync("ShowJackpot", jackpotInMillyCents);
        }
    }
}
