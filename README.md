# StudentsAPI
example API using asp net core + CQRS and Entity Framework


Setup:


*log path*
 open StudentsDemoApp\StudentsDemoApp/nlog.config and set correct path to save log files.
 
 
 *connection string*
 in both projects Api.Rest and Entity Framework open appsettigs.json and add this key: 
 "ConnectionStrings": {
    "StudentsAppDbContext": "Server=localhost;Port=3306;Database={yourDatabase};Uid=root;Pwd={yourPassword}"
  }
  
  *update database schema*
  using console terminal in EntityFramework Project and execute "dotnet ef database update"


