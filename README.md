# Project details
This project about ticket management. For now is ready data access layer and business layer where validation logic and database access take place. Also resently added "ThirdPartyEventEditor" project for creating event, and "TicketManagement.Web" web project.

# Build & Run
1. Right click on solution in "Solution Explorer" and select "Set Startup Projects...".\
   ![image](https://user-images.githubusercontent.com/93681186/159213806-62419c56-eb26-456a-ad4f-5352c03ee8a7.png)
2. Setup as in the picture below for starting project in non debug mode\
   ![image](https://user-images.githubusercontent.com/93681186/159214006-184b0036-fc80-4303-b86a-1ebecf5cacac.png)\
   or for starting in non debug mode\
   ![image](https://user-images.githubusercontent.com/93681186/159214186-7e6a8f5e-a34a-4e3e-84d4-62d71e7b2ce6.png)
   
# Path to "ThirdPartyEventEditor" project
"*project_folder*/src/ThirdPartyEventEditor/ThirdPartyEventEditor.sln"

# Path to "TicketManagement" project
"*project_folder/TicketManagement.sln"

# Connection strings
1. "ThirdPartyEventEditor" project. Need set name of json file, that contains in "App_Data" folder of project.
   ![image](https://user-images.githubusercontent.com/93681186/156408932-dbd683af-697e-42c4-9c2c-0930c71c740c.png)
2. "TicketManagement" project.\
   2.1. "Testing".
      ![image](https://user-images.githubusercontent.com/93681186/156410129-5588a463-ab48-4fad-82af-d96d7cc4023a.png)
   2.2. "Main".
      ![image](https://user-images.githubusercontent.com/93681186/156410302-e6d3b1e0-d766-4543-b876-965b9ba5b6b1.png)

# Used technologies
1. C# 5.0
2. Entity Framework Core as ORM
3. ASP.NET Core for web application
4. Moq for testing

# Users
1. Login: admin@epam.com\
   Password: Admin+1\
   Roles: "Admin", "Manager", "Default"
2. Login: manager@epam.com\
   Password: Admin+1\
   Roles: "Manager", "Default"
3. Login: default@epam.com\
   Password: Admin+1\
   Roles: "Default"

# Steps how to check integration tests
1. Need publish database project (TicketManagement.Database).
2. In "TicketManagement.IntegrationTests" project change file "appsettings.testing.json" and point self database name.
   ![image](https://user-images.githubusercontent.com/93681186/156394629-0dfbd28f-60be-4757-a9fc-0e9d1e162b2c.png)
3. Run tests! :)

### (c) Vitali Talatai
