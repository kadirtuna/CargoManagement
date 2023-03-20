# CargoManagementWEBAPI
The RESTful API project that automates the order progress in Cargo Management. It finds the cheapest carrier regarding the order's desi. Therefore, the order is automatically made from that the most eligible company.

## What were the tools that I used;
- .NET Core 6,
- MSSQL,
- Swagger UI,
- Entity Framework Core - Code First Migration,
- ASP.NET WEB API,
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

```sql
"ConnectionStrings": {
    "Sql": "Server=(localdb)\\trialSchool; initial catalog=CargoManagementDB;Trusted_Connection=True;MultipleActiveResultSets=True;"
  },
```
