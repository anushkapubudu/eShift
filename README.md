# eShift - Desktop Logistics Management System

eShift is a comprehensive desktop application designed to streamline the management of logistics and shifting operations. It provides a centralized platform for administrators to manage jobs, customers, staff, vehicles, and finances, and for customers to create and track their own shipping jobs.

## Tech Stack

- **Framework:** .NET Framework 4.7.2
- **Language:** C#
- **UI:** Windows Forms (WinForms)
- **Database:** Microsoft SQL Server
- **Libraries:**
  - **BCrypt.Net-Next:** For secure password hashing and verification.
  - **ClosedXML:** For generating reports in Microsoft Excel format.
  - **PDFsharp:** For creating and managing PDF documents, such as invoices.

## Features

- **Role-Based Access Control:** Separate interfaces and functionalities for Admins and Customers.
- **Admin Dashboard:** A central hub for administrators to get an overview of ongoing operations, including summaries of jobs, revenue, and customer activity.
- **Job Management:** Admins can create, update, assign, and monitor all shipping jobs.
- **Customer Management:** Admins can view and manage customer information.
- **Staff & Vehicle Management:** Admins can manage employee details and the fleet of vehicles.
- **Container Management:** Keep track of shipping containers.
- **Invoice & Payment Tracking:** Generate and manage invoices for services rendered.
- **Reporting:** Generate detailed reports on jobs, customers, and revenue in both Excel and PDF formats.
- **Customer Portal:**
  - Customers can register and log in to the system.
  - Create new shipping jobs.
  - View and manage their existing jobs.
  - View their invoice history.
  - Manage their profile information.

## Prerequisites

- **Visual Studio:** 2019 or later (with .NET desktop development workload installed).
- **Microsoft SQL Server:** A local instance (like SQL Server Express) running.
- **.NET Framework:** Version 4.7.2.

## Setup and Installation

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/anushkapubudu/eShift.git
    ```
2.  **Open in Visual Studio:**
    - Open Visual Studio.
    - Select "Open a project or solution".
    - Navigate to the cloned directory and open the `eShift.sln` file.

3.  **Restore NuGet Packages:**
    - Right-click on the solution in the Solution Explorer.
    - Select "Restore NuGet Packages". Visual Studio should do this automatically upon opening the solution.

## Database Setup

The application is configured to connect to a local SQL Server database named `eShiftDB` using integrated security. You can set up the database in one of two ways:

**Option 1: Restore from Backup**

1.  **Locate the Backup File:**
    - A database backup file named `eShiftDB.bak` is located in the `Database/` folder of this project.

2.  **Restore the Database:**
    - Open SQL Server Management Studio (SSMS).
    - Right-click on the "Databases" node and select "Restore Database...".
    - Select "Device" and click the "..." button to locate and add the `eShiftDB.bak` file.
    - Click "OK" to restore the database. This will create the `eShiftDB` database with all the necessary tables and data.

**Option 2: Manual Setup**

1.  **Create the Database:**
    - Open SQL Server Management Studio (SSMS) or use the `sqlcmd` utility.
    - Execute the following command to create the database:
      ```sql
      CREATE DATABASE eShiftDB;
      ```

2.  **Create Database Tables:**
    - A script file named `scripts.sql` is located in the `Database/` folder.
    - Open this file in SSMS and execute the script against the `eShiftDB` database to create all the necessary tables.

3.  **Verify Connection String:**
    - The connection string is hardcoded in the `Utilities/DbConst.cs` file.
    - If your SQL Server instance has a different name or requires different credentials, update the `ConnectionString` constant in this file:
      ```csharp
      public static class DbConst
      {
          public const string ConnectionString = "Server=YOUR_SERVER_NAME;Database=eShiftDB;Integrated Security=true;TrustServerCertificate=True;";
      }
      ```

## How to Use

1.  **Run the Application:**
    - Press `F5` or click the "Start" button in Visual Studio to build and run the project.

2.  **Login / Register:**
    - The application will start with the **Login** screen (`FrmLogin`).
    - If you restored the database from the backup file, you can use the following credentials to test the system:
        - **Admin User:**
            - **Email:** `admin@eshift.com`
            - **Password:** `Pass1234#`
        - **Customer User:**
            - **Email:** `testcustomer@eshift.com`
            - **Password:** `Pass1234#`
    - If you set up the database manually, you will need to register a new customer account or manually insert user records into the `Users` table.

3.  **Navigate the Application:**
    - Based on the user's role (Admin or Customer), the appropriate dashboard will be displayed after a successful login, granting access to the features listed above.
