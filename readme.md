
<p align="center">
  <a href="#">
    <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcS49HPDAbme5yIhOyFf-rOKCAzmXPCOdQyqMsbdE0tP61uE8ccP&usqp=CAU" alt="Logo" width="110" height="110">
  </a>
  <h1 align="center">RouletteApi</h1>
  <p align="center">
    Application created for Singular. You can see the actual task in 'NET Task' folder.
    <br />

 
  </p>
  <br>  
  
  ## Contents

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

Trying to follow clean architecture and clean coding principles, several layers and projects are made.
The main project that should be started is in Presentation layer, <b>RoulleteApi</b> (Rest API).
There is two more projects in the solution that can be started, <b>BetConcurrencyTestClient</b> and <b>SignalRClientForJackpot</b>.

---


## Installation
- Clone github repository, or download and unzip it. 
- Make startup project RoulleteApi.
- Here is the connection string: Data Source=localhost;Initial Catalog=RoulleteDb; integrated security=true
  Make sure you have MSSQL Server with access of creating database. If localhost don't work for you configure it as you wish.
- run "update-database" command in package manager console, if you use visual visual studio. 
  Don't forget startup project should be RoulleteApi, but db context is in RoulleteApi.Repository.Ef project.

<b>Note<b>: There is just one user in database, and regisration functionality is not added for now. Username: <b>gisaiashvili</b>, Password:<b>123456!Aa</b>

- Run the project and if everything is cool you will see the Swagger Documentation.

You can also check for video instructions in <b>RoulleteApi\Additional Resources\Video Instructions<b>
   
---


## Prerequisites

```c#
-.Net core 3.1 and MSSQL SERVER should be installed. 
-You may need to change connection string, just target the server where you have permission to create database
```

## Testing

You can check testing instructions in <b>RoulleteApi\Additional Resources\Video Instructions<b>

You can test it by swagger doc or postman.
If you decide to test with postman you don't need to configure it yourself, just import json from Additional Resoures/Postman Test Collection folder.

Basic flow to test:

First of all you need to login and get access token.
Getting access token is possible from <b>user/session</b> post endpoint, by passing Username: <b>gisaiashvili</b>, Password:<b>123456!Aa</b>

Tokens format is 'Bearer accessToken'. e.x. 'Bearer eydasdasjlkdj32oijd209dj239jd23d23d23nd92nvu8r934v3r98nr3'
If you are testing it from swagger click Authorize and paste token there, or add this token in Authorization header. Again, don't forget to add 'Bearer ' before token

Note: <b>The tokens life lasts for five minutes, 
	  If authorized client makes a new api call, new token will be returned in response header X-Response-Access-Token.</b>
   
---


## Author
👤 **Giorgi Isaiashvili**

- Linkedin: [@giorgi-isaiashvili](https://www.linkedin.com/in/isaiashvili/)
