using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace SignalRClientForJackpot
{
    // Just start it, to be updated about jackpot.
    
    class Program
    {
        static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:1010/hubs/jackpot")
                .Build();

            connection.On<long>("ShowJackpot", (jackpot) => Console.WriteLine($"Jackpot amount is: {jackpot / 10000m}$"));

            while (true)
            {
                try
                {
                    await connection.StartAsync();
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.Delay(1000);
                }
            }

            Console.ReadLine();
        }
    }
}