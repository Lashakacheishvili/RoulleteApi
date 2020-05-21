
====== >>>>> Check "RoulleteApi\Additional Resources\Video Instructions" Folder  =============================

Welcome here.

This project is created for Singular. You can see the task in 'NET Task' folder.

It includes latest Microsoft technologies for 20/05/2020. 
Part of That technologies are: .Net Core, EF Core, SignalR, Nswag, MSSQL Server.

Trying to follow clean architecture and clean coding principles, several layers and projects are made.
The main project that should be started is in Presentation layer, RoulleteApi (Rest API).
There is two more projects in the solution that can be started, BetConcurrencyTestClient and SignalRClientForJackpot.

Both of them is simple small console apps for testing purposes.

BetConcurrencyTestClient calls /user/bet api endpoint in several threads, and in Console will be result given.
There is no custom Assertion after calls, I tested it by looking at the result in console. The goal was to make sure
the functionality works for now, and this solution is more than enough.

SignalRClientForJackpot has just one goal, watch at Jackpot hub, that is registered in Api and write it in console.

You will see some Api tests in Additional Resoures/Postman Test Collection folder. Just import it in postman and run.
Not all scenarios are tested, but small easy functionality is.

Now Steps to install and run project Successfully:

1. Clone github repository, or download and unzip it. 
2. Make startup project RoulleteApi.
3. Here is the connection string: Data Source=localhost;Initial Catalog=RoulleteDb; integrated security=true
   Make sure you have MSSQL Server with access of creating database. If localhost don't work for you configure it as you wish.
4. run "update-database" command in package manager console, if you use visual visual studio. 
   Don't forget startup project should be RoulleteApi, but db context is in RoulleteApi.Repository.Ef project.

Note: There is just one user in database, and regisration functionality is not added for now. Username: gisaiashvili, Password:123456!Aa

5. Run the project and if everything is cool you will see the Swagger Documentation.

Note: You can test it by swagger doc or postman, just import json from Additional Resoures/Postman Test Collection folder.

Basic flow to test:

Get access token from user/session post endpoint, by passing Username: gisaiashvili, Password:123456!Aa
If you are testing it from swagger just click Authorize and follow instruction, or add this token in Authorization header, with 'Bearer ' before it.

Note: The tokens life lasts for five minutes, If authorized client makes a new api call, new token will be returned in response header X-Response-Access-Token.

Happy betting.
