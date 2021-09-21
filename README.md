# StudentsAPI
example API using asp net core + CQRS and Entity Framework


Setup:

<br/>
*log path*<br/>
 open StudentsDemoApp\StudentsDemoApp/nlog.config and set correct path to save log files.
 
 <br/>
 *connection string*<br/>
 in both projects Api.Rest and Entity Framework open appsettigs.json and add this key: 
 "ConnectionStrings": {
    "StudentsAppDbContext": "Server=localhost;Port=3306;Database={yourDatabase};Uid=root;Pwd={yourPassword}"
  }
  <br/>
  *update database schema*<br/>
  using console terminal in EntityFramework Project and execute "dotnet ef database update"


