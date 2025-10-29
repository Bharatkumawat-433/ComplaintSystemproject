# Complaint Management System

## Project Title
Complaint Management System (CMS)

## Description
The Complaint Management System is a simple web application built using ASP.NET Core MVC and SQL Server.  
It helps users to register their complaints online and allows the admin to manage and resolve them easily.  
This project makes the complaint process faster, more transparent, and well-organized.

## Features
- User registration and login system  
- Add, view, and track complaints  
- Admin panel to view and update complaints  
- Complaint status (Pending / In Progress / Resolved)  
- Role-based access for User and Admin  
- Database managed using SQL Server and Entity Framework Core  

## How It Works
1. A user can register and log in to submit complaints.  
2. Complaints are saved in the database with a default “Pending” status.  
3. The admin can log in, view all complaints, and update their status.  
4. The user can later log in again to check if their complaint is resolved.  

## How to Run the Project

1. Open the project folder in **Visual Studio Code** or **Visual Studio**.  
2. Make sure your **SQL Server** is running on your system.  
3. Open the **terminal** inside VS Code.  
4. Type the command:
   ```bash
   dotnet run

5. Wait for the project to start. You will see something like this in the terminal
Now listening on: https://localhost:7021

6. Copy that link and open it in your browser.

7. The website will open — now you can:

Register as a new User and submit a complaint.

Log in as an Admin to manage and update complaints.

example . admin email : admin@test.com
password : Admin@123