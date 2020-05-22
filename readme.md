
  <h2>RouletteApi</h2>
    Application created for Singular. You can see the actual task in <b>RoulleteApi/Additional Resources/NET Task</b> folder.
    <br />

 
  </p>
  <br>  
  
  ## Navigation

- [About](#About)
- [Installation](#installation)
- [Prerequisites](#Prerequisites)
- [Testing](#Testing)
---

## About
Project is written in Microsofts latest technologies for 20/05/2020.

- .Net Core 3.1
-  EF Core
-  SignalR
-  Nswag
-  MSSQL Server

This is the test project. You wont be able see full functionality of Roullete here. The main purpose of this project was to show creators ability of coding and solving tasks. Still you will find it useful, there might be some good technical and architectural points.

Trying to follow clean architecture and clean coding principles, several layers and projects are made.
The main project that should be started is in Presentation layer, <b>RoulleteApi</b> (Rest API).
There is two more projects in the solution that can be started, <b>BetConcurrencyTestClient</b> and <b>SignalRClientForJackpot</b>.


---

## Prerequisites

- .Net core 3.1 and MSSQL SERVER should be installed. 
- You may need to change connection string, just target the server where you have permission to create database

---

## Installation
- Clone github repository, or download and unzip it. 
- Make startup project RoulleteApi.
- Here is the connection string: Data Source=localhost;Initial Catalog=RoulleteDb; integrated security=true
  Make sure you have MSSQL Server with access of creating database. If localhost don't work for you configure it as you wish.
- run "update-database" command in package manager console, if you use visual studio. 
  Don't forget startup project should be RoulleteApi, but db context is in RoulleteApi.Repository.Ef project.

<b>Note</b>: There is just one user in database, and regisration functionality is not added for now. <b>Username:</b> gisaiashvili, 		  <b>Password</b>: 123456!Aa

- Run the project and if everything is cool you will see the Swagger Documentation.

  You can also check for video instructions in <b>RoulleteApi\Additional Resources\Video Instructions</b>
   
---

## Testing

You can check testing instructions in <b>RoulleteApi/Additional Resources/Video Instructions</b>

You can test it by swagger doc or postman.
If you decide to test with postman you don't need to configure it yourself, just import json from Additional Resoures/Postman Test Collection folder.

Basic flow to test:

First of all you need to login and get access token.
Getting access token is possible from <b>user/session</b> post endpoint, by passing <b>Username</b>: gisaiashvili, <b>Password</b>:123456!Aa

Tokens format is 'Bearer accessToken'. e.x. 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI...'
If you are testing it from swagger click Authorize and paste token there, or add this token in Authorization header. Again, don't forget to add 'Bearer ' before token

<b>Note:</b> The tokens life lasts for five minutes, 
	     If authorized client makes a new api call, new token will be returned in response header X-Response-Access-Token.
   
---


## Author
  🧔 **Giorgi Isaiashvili**

- Linkedin: [Giorgi Isaiashvili](https://www.linkedin.com/in/isaiashvili/)
