using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoulleteApi.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RoulleteApi.Application
{
    public class JackpotWatcher : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHubContext<JackpotHub> _jackpotHub;

        public JackpotWatcher(IServiceScopeFactory serviceScopeFactory, IHubContext<JackpotHub> jackpotHub)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _jackpotHub = jackpotHub;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    Thread.Sleep(500);
                    using var scope = _serviceScopeFactory.CreateScope();
                    var jackpotService = scope.ServiceProvider.GetRequiredService<IJackpotService>();
                    var jackpotResult = await jackpotService.GetAsync();
                    var jackpot = jackpotResult.Result.AmountInMillyCents;
                    await _jackpotHub.Clients.All.SendAsync("ShowJackpot", jackpot);
                }
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}