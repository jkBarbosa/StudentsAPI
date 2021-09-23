# StudentsAPI
example API using asp net core + CQRS and Entity Framework


## Setup:

- ### Docker:
  - just executing docker-compose up -d in root folder.
  - execute these commands in a terminal:
  ```
  docker exec -it studentsapp-api /bin/bash
  cd ..
  cd migration
  ./EFMigrationUtility
  ```
  note: can you press ctrl + d to exit.
 
  after these steps the application is already configured, and can be accessed through url: localhost:5000/

- ### Host Manually Using IDE
 - just change connectionString inside Api.Rest and Entity Framework projects.



