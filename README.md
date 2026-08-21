👥 Employees Management System

A desktop-based Employees Management System developed using C#, Windows Forms, ADO.NET, and SQL Server.

The system is designed to help manage employees and their related administrative operations, including employee records, departments, attendance and absence, salary scales, and employee leaves.

This project was developed as a personal project to practice building a complete business-oriented desktop application and to strengthen my C#, SQL Server, ADO.NET, and software architecture skills.

⸻

📌 About the Project

The Employees Management System provides a centralized application for managing employee-related operations.

The system allows users to manage employee information and organize important administrative data such as departments, salary scales, attendance, absences, and employee leaves.

The application was designed using a 3-Tier Architecture to separate the presentation, business logic, and data access responsibilities.

⸻

✨ Main Features

👤 Employee Management

* Add new employees.
* Edit employee information.
* Delete employees.
* View employee information.
* Search and manage employee records.

🏢 Department Management

* Manage company departments.
* Associate employees with their departments.
* Organize employee records based on departments.

🕒 Attendance & Absence

* Record employee attendance.
* Track employee absences.
* Manage attendance-related information.

💰 Salary Scales

* Manage employee salary scales.
* Organize salary-related information.
* Associate salary information with employees where applicable.

🏖️ Leave Management

* Manage employee leaves.
* Record leave information.
* Track employee leave records.

⸻

🏗️ Architecture

The application follows a 3-Tier Architecture that separates the system into three main layers:

┌──────────────────────────────┐
│     Presentation Layer       │
│          WinForms            │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│      Business Logic Layer    │
│            BLL               │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│       Data Access Layer      │
│            DAL               │
│           ADO.NET            │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│          SQL Server          │
└──────────────────────────────┘

Presentation Layer

Responsible for:

* Windows Forms user interfaces.
* User interaction.
* Displaying information.
* Collecting user input.
* UI-level validation.

Business Logic Layer

Responsible for:

* Business rules.
* Application logic.
* Validation of business operations.
* Coordinating operations between the presentation and data access layers.

Data Access Layer

Responsible for:

* Communicating with SQL Server.
* Executing database operations.
* Calling Stored Procedures.
* Managing database access through ADO.NET.

⸻

🛠️ Technologies Used

Technology	Usage
C#	Application development
.NET	Application framework
Windows Forms	Desktop user interface
ADO.NET	Database access
SQL Server	Database management
Stored Procedures	Database operations
Transactions	Maintaining data consistency
3-Tier Architecture	Application architecture

⸻

🗄️ Database

The application uses Microsoft SQL Server as its relational database.

The database stores information related to:

* Employees
* Departments
* Attendance
* Absence
* Salary scales
* Employee leaves
* Other related employee-management entities

The application communicates with SQL Server using ADO.NET.

⸻
🔐 Database Transactions

The project uses database transactions for operations where multiple database changes need to be treated as a single unit of work.

This helps maintain data consistency by ensuring that related operations are either completed successfully or rolled back when an error occurs.

⸻

⚙️ Stored Procedures

The project uses SQL Server Stored Procedures for database operations.

Stored Procedures are used to encapsulate selected database operations and provide a structured way for the application to communicate with SQL Server.

⸻

🎯 Project Goals

The main goals of this project were to gain practical experience in:

* Building a complete C# desktop application.
* Developing Windows Forms applications.
* Designing and working with relational databases.
* Using SQL Server.
* Accessing databases using ADO.NET.
* Creating and using Stored Procedures.
* Working with database Transactions.
* Applying 3-Tier Architecture.
* Applying Object-Oriented Programming principles.
* Implementing business rules.
* Managing relationships between different business entities.
* Building maintainable database-driven applications.

⸻

📸 Screenshots

<img width="1919" height="1010" alt="Screenshot 2026-08-21 164624" src="https://github.com/user-attachments/assets/08753121-5667-46af-a178-025ddb180492" />
<img width="1919" height="1012" alt="Screenshot 2026-08-21 164556" src="https://github.com/user-attachments/assets/8d45d178-f51c-495a-8b87-d142502ddf2a" />
<img width="1919" height="1012" alt="Screenshot 2026-08-21 164532" src="https://github.com/user-attachments/assets/91b52ebd-5303-44da-8cdc-9d1f35d09475" />
<img width="1043" height="670" alt="Screenshot 2026-08-21 164510" src="https://github.com/user-attachments/assets/b2e46f73-cc0f-4aa1-ba94-a0182a19ab6e" />
<img width="1012" height="929" alt="Screenshot 2026-08-21 165309" src="https://github.com/user-attachments/assets/4be452d3-38e0-4d53-b703-836d06945626" />
<img width="977" height="881" alt="Screenshot 2026-08-21 165216" src="https://github.com/user-attachments/assets/c9ac8437-2ac2-4bf1-8715-520bdfd5dc6e" />
<img width="986" height="896" alt="Screenshot 2026-08-21 165126" src="https://github.com/user-attachments/assets/b4fda659-9cca-4a2a-8ba8-0fe3cbe3476d" />
<img width="1919" height="1012" alt="Screenshot 2026-08-21 164926" src="https://github.com/user-attachments/assets/d9b26718-6475-450b-8e35-bfc48d05e7d9" />


⸻

📂 Project Structure

The project follows a layered structure similar to:

Employees Management System
│
├── Presentation Layer
│   └── Windows Forms
│
├── Business Logic Layer
│   └── Business Classes
│
├── Data Access Layer
│   └── ADO.NET Data Access Classes
│
└── Database
    └── SQL Server Scripts

The exact structure may vary depending on the current implementation of the project.

⸻

🧠 What I Learned

Through this project, I gained practical experience in:

* C# desktop application development.
* Windows Forms.
* SQL Server.
* ADO.NET.
* Stored Procedures.
* Database Transactions.
* 3-Tier Architecture.
* Object-Oriented Programming.
* Business rules and validation.
* Relational database design.
* Managing relationships between business entities.
* Building database-driven business applications.
* Separating presentation, business logic, and data access responsibilities.

⸻

👨‍💻 Author

Ibrahim AL-MOKHTAR

Junior .NET Developer

📄 License

This project was developed as a personal project for educational and portfolio purposes.
