# Cargo Management WEB API
The RESTful API project that automates the order progress in Cargo Management. It finds the cheapest carrier regarding the order's desi. Therefore, the order is automatically made from that the most eligible company.

## What were the tools that I used;
- .NET Core 6,
- MSSQL,
- Swagger UI,
- Entity Framework Core - Code First Migration,
- ASP.NET WEB API,
- Hangfire Cron Jobs,
- Repository Design Pattern,
- Three Layered Architecture,
- Async Programming,


This project was developed with the "Visual Studio 2022" IDE.

# Run

With Visual Studio Code, You should follow these instructions to easily run it;

1 - Clone the project;

```
git clone https://github.com/kadirtuna/CargoManagementWEBAPI.git
```
2 - In MSSQL, Execute the given Sql Script named "CargoManagementDBScript.sql" in the repository, 

3 - Modify the "ConnectionStrings" pair in the "appsettings.json" file under "CargoManagement/Properties" Folder to connect the database, For Example;
Change the Server value regarding to your database server name like below;

NOTE: Due to hangfire database which is wanted to be used for jobs, is not created automatically. Before using the project, don't forget to create the hangfire sql database manually. Then the project will be able to connect to the hangfire sql database and manages of the creation of the table for using Hangfire.
```
"ConnectionStrings": {
    "Sql": "Server=localhost; initial catalog=CargoManagementDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True",
    "HangfireSql": "Server=localhost; initial catalog=CargoManagementHangfireDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True"
  },
```

4 - After all, In Terminal of VS Code, Go to the "CargoManagement" Folder in the root directory. Type the command and enter;

```
dotnet run
```

5 - Congrutulations! The project is successfully running from now on.

Do not forget to test the project in a Browser with Swagger UI or Postman etc.

# Test it with Swagger UI:

- By default, When the project is running, type the URL "https://localhost:7139/swagger/index.html" in address search bar of the internet browser. 

If everything is alright, you should see the documentation and can test "CargoManagement" project.  

# Projects Documentation in Swagger UI

The endpoints of Carrier Controller with all CRUD Operations,
![Carrier Controller Test](https://github.com/kadirtuna/CargoManagementWEBAPI/blob/master/Images/CargoManagementImage1.jpg)

The endpoints of CarrierConfiguration associated with CarrierId,
![CarrierConfiguration Controller Test](https://github.com/kadirtuna/CargoManagementWEBAPI/blob/master/Images/CargoManagementImage2.jpg)

The endpoints of Order. All automation to determine the order price has been carrying out inside of the Order controller.,
![Order Controller Test](https://github.com/kadirtuna/CargoManagementWEBAPI/blob/master/Images/CargoManagementImage3.jpg)

***If you have any question, don't hesitate to contact me.***
# Authors

- [@kadirtuna](https://github.com/kadirtuna)
