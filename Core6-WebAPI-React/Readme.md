## Section 2: First Steps

npx create-react-app@5 reactweb --template typescript
* React: Initial Application and Component
* API: Initial Application with Swagger
```
dotnet new webapi -minimal
dotnet run
```
## Section 3: Exposing, Getting, and Displaying Data
* API: Creating a Database
```
dotnet add package Microsoft.EntityFrameworkCore.Sqllite --version 6.*
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.*
dotnet ef migrations add initial
dotnet ef database update
```
* API: Adding Data Endpoints