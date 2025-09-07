# Expense Tracker (ASP.NET Core + MySQL + Entity Framework Core)

This project is an **ASP.NET Core MVC application** that uses **Entity Framework Core** with **MySQL** for data storage.

---
## 🚀 Prerequisites

Make sure you have installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download) (or your project’s target version)
- [MySQL Server](https://dev.mysql.com/downloads/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [VS Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

---

## ⚙️ Project Setup

1.  **Clone the repository**
    ```bash
    git clone [https://github.com/kishor-23/Expense-Tracker-using-ASP.NET.git](https://github.com/kishor-23/Expense-Tracker-using-ASP.NET.git)
    cd Expense-Tracker-using-ASP.NET
    ```

2.  **Update MySQL connection string**
    Open `appsettings.json` and configure your MySQL connection:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "server=localhost;port=3306;database=ExpenseTrackerDb;user=root;password=yourpassword;"
    }
    ```

3.  **Install EF Core MySQL provider**
    Open a terminal in the project directory and run the following command to add the MySQL provider for Entity Framework Core. This library allows EF Core to communicate with a MySQL database.
    ```bash
    dotnet add package Pomelo.EntityFrameworkCore.MySql
    ```

4.  **Create the database**
    Run the Entity Framework Core migrations to create the database schema based on your `DbContext` and model classes. This will create the `ExpenseTrackerDb` database and its tables.
    ```bash
    dotnet ef database update
    ```

5.  **Run the application**
    You can now run the application from the terminal or using Visual Studio/VS Code.
    ```bash
    dotnet run
    ```
