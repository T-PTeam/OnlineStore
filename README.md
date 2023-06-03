# OnlineStore

This is an online store developed using C#, .NET 6 and the MVC pattern. The front end of the project is written using Razor.

## Requirements

The following components must be installed for this project to work:

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [MSSQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Installation and setup

1. Clone the repository with the following command:
  ```shell
   git clone https://github.com/MrWoody666/OnlineStore.git
  ```

 2. Set up the MSSQL Server database. Change the database connection string in the appsettings.json file to match your configuration:
    ```json
      "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=YourDatabase;Trusted_Connection=True;"
    }
    ```
3. You need to check all migrations, write this command in Package Manager Console
  ```shell
    get-migration
  ```  
4. Perform database migrations with the following command:
  ```shell
    update-database
   ````
# Usage

In this online store, you can carry out various actions, such as:

- View products and categories
- Adding items to the cart
- Create product and category
- Edit product and category
- Delete product and category

# Additional Information

- This project uses the MediatR pattern to process commands and requests. You can check out its documentation [here](https://github.com/jbogard/MediatR).
