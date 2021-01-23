IMPORTANT
=============
Prerequisites:
-Node.js
-npm
-Angular cli
-.NET Core 5 SDK
-SQL server

To run the app
========================
1) open dist folder
2) run the api by running API.exe in the API folder
3) the web application is in the web folder you can serve it by installing live-server `npm install -g live-server` then run `live-server` command inside the Web folder

To build from source code
=========================
API:
-Modify the 'Default' connection string in appsettings.json to match your environment
-Run the application by pressing Ctrl + F5
-Ensure the database is created, if not select the Web project and run 'update-database' command in NuGet Package Manager Console 

Web app (Angular):
-modify the apiURl in 'environment.ts' to match the running API, default is 'https://localhost:5001/api'
-Open a terminal in the project folder then run 'ng s -o'
-Make sure the api is running with the url that is configured is angular environment.ts
