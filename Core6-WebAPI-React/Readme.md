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
* API: Separating Concerns
* React: Consuming Endpoints
* React: Custom Hooks and useEffect
* React: Caching and Re-fetch with react-query
* React: Formatting Incoming Data and displaying API Status Information
## Section 4: Adding Frontend Routing and Navigation
* API: Details Endpoint
* React: Setting up Routing
* * npm i react-router-dom@6 @types/react-router-dom
* React: Details Component
* React: Navigation
## Section 5: Creating， Updating，and Deleting Data
* API: Create, Edit and Delete Endpoints