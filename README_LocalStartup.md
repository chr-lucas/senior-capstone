# Local Deployment Guide for **Nebula Workbook** App

This guide outlines the steps required to run and test the workbook from your local machine.

---

## Prerequisites
- .NET 9 SDK
- Node.js
- g++
- Emscripten SDK
- ASP.NET Runtime
- Database management tool (SSMS, MySQL, etc.)
- IDE (Visual Studio 2022 prefered)

## Running the App Locally Using VS 2022
### 1. Set up a database connection using your prefered method
Learning Link: https://learn.microsoft.com/en-us/ssms/quickstarts/ssms-connect-query-sql-server?tabs=modern

### 2. Set up database tables
  - Delete all files from the project folder "migrations"

  - In the NuGet Package Manager run the following commands in order:
  ```bash
  Add-Migration InitialMigration
  Update-Database
  ```
  These commands create the SQL and runs in on your database based on the models defined in ~/Models.

  ERD: 
  ![Blank diagram (3)](https://github.com/user-attachments/assets/90ef464b-4dad-482a-b201-332a1f6d8fef)
  (Sorry for the flash bang!)


### 3. Navigate to the project .sln in Visual Studio 2022 (VS)

![image](https://github.com/user-attachments/assets/ba820015-c722-455f-906f-cb0deae6cb86)

### 4. Edit Connection string
Navigate to the Appsettings.json file in VS or other IDE and edit the connection string to be your IP.

![image](https://github.com/user-attachments/assets/9f049c61-d35d-421a-9875-7b09feead68b)

### 5. Run the project
Select the Run button or press CTRL + F5 for non-debug mode and F5 for debug mode. The database should populate with seed data at this point.

![image](https://github.com/user-attachments/assets/ba7da0b8-8d1f-484a-aefe-e755339cb1cd)

### 6. Navigate to http://localhost:5077

### 5. Stop the app
You can just close the page to close the app. If you want to completely stop the service navigate to the generated command prompt window and use CTRL + C.

## Notes on Configuration

- If the database connection string changes, update it in `appsettings.json`:
```
"ConnectionStrings": {
  "DefaultConnection": "Server=137.190.19.215;Database=NebulaWorkbook;..."
}
```

- The server compilers (g++, emcc) are invoked by the backend controller `IdeController.cs`.

- The app runs in development mode unless published.
