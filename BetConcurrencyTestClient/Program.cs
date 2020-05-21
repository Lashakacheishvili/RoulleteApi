using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetConcurrencyTestClient
{
    // The code is just terrible, I wrote it for myself but changed mind to delete.
    // It works...
    // This is test program (not standard type of test) that has only one goal:
    // To Test Jackpot concurrency update by making bets from different threads.
    // Bet amount is 1.
    // I recommend no to change anything except threadCount, that is equal to 20 now
    // Everything is hard coded, even endpoint of api.

    class Program
    {
        static void Main(string[] args)
        {
            var threadCount = 20;

            var token = new LoginClient().GetToken();

            var jackpotBeforeUpdate = new JackpotClient().GetJackpot();

            Console.WriteLine($"Jackpot: {jackpotBeforeUpdate}");

            var tasks = new List<Task>();

            for (int i = 0; i < threadCount; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    Console.WriteLine("Making new bet at: " + DateTime.Now.ToLongTimeString());
                    new BetClient().MakeBet(token);
                }));
            }

            Task.WhenAll(tasks).Wait();

            var jackpot = new JackpotClient().GetJackpot();

            Console.WriteLine($"Jackpot before update: {jackpotBeforeUpdate}");
            Console.WriteLine($"Jackpot after update: {new JackpotClient().GetJackpot()}");
            Console.WriteLine($"Should have increased by {threadCount}");
            Console.ReadLine();
        }

        class BetStringModel
        {
            public string BetString { get; set; }
        }

        class AccessTokenModel
        {
            public string AccessToken { get; set; }
        }

        class LoginClient
        {
            public string GetToken()
            {
                var loginClient = new RestClient("http://localhost:1010/api/user/session");
                loginClient.Timeout = -1;
                var loginRequest = new RestRequest(Method.POST);
                loginRequest.AddParameter("application/json", "{\n    \"username\": \"gisaiashvili\",\n    \"password\": \"123456!Aa\"\n}", ParameterType.RequestBody);
                IRestResponse loginResponse = loginClient.Execute(loginRequest);
                var token = JsonConvert.DeserializeObject<AccessTokenModel>(loginResponse.Content).AccessToken;
                return token;
            }
        }

        class BetClient
        {
            public void MakeBet(string token)
            {
                var betClient = new RestClient("http://localhost:1010/api/user/bet");
                betClient.AddDefaultHeader("Authorization", "Bearer " + token);
                betClient.Timeout = -1;
                var betRequest = new RestRequest(Method.POST);
                var inputString = "[{\"T\": \"v\", \"I\": 20, \"C\": 1, \"K\": 1}]";
                var betStringModel = new BetStringModel() { BetString = inputString };
                betRequest.AddParameter("application/json", JsonConvert.SerializeObject(betStringModel), ParameterType.RequestBody);
                betClient.Execute(betRequest);
            }
        }

        class JackpotClient
        {
            public string GetJackpot()
            {
                var client = new RestClient("http://localhost:1010/api/jackpot");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                return response.Content;
            }
        }
    }
}
