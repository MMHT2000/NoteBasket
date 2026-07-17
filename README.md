# NoteBasket

NoteBasket is a C# Windows Forms course project for managing and sharing academic notes. The application was built as part of a university C# course and demonstrates desktop UI development, role-based navigation, SQL Server database operations, authentication, and basic subscription/payment simulation.

## Project Summary

The system is designed for students who want to browse study materials and for approved contributors who can upload notes. It uses separate dashboards for regular users, NoteMasters, and admins.

## Main Features

- User registration, login, password reset, and password change
- Password hashing with `BCrypt.Net-Next`
- Role-based access for `Free`, `Silver`, `Gold`, `NoteMaster`, and `Admin` users
- User profile view and edit support
- Note upload with title, description, category, file path, and subscription level
- Note approval workflow for pending, approved, and rejected notes
- Note browsing and preview
- Download limit/activity tracking for regular users
- Bookmarking notes
- Rating and review system
- Note reporting and admin-side reported note management
- Subscription tier upgrade flow with a mock bKash-style payment screen
- Admin tools for managing users, notes, and NoteMaster requests

## Technology Stack

- Language: C#
- UI: Windows Forms
- Framework: .NET Framework 4.7.2
- Database: Microsoft SQL Server
- Data Access: ADO.NET / `System.Data.SqlClient`
- Package Manager: NuGet packages.config
- Main dependency: `BCrypt.Net-Next`

## Repository Structure

```text
NoteBasket/
├── NoteBasket.sln
├── README.md
├── images/
└── NoteBasket/
    ├── Program.cs
    ├── App.config
    ├── NoteBasket.csproj
    ├── packages.config
    ├── *_Dashboard.cs
    ├── Login_Form.cs
    ├── UserRegistration_Form.cs
    ├── UploadNote.cs
    ├── NotePreview.cs
    ├── Ratings_Review.cs
    ├── BookMarks.cs
    ├── Resources/
    └── images/
```

## Database Notes

The application expects a SQL Server database named `NoteBasketDB`. The connection string is configured in `NoteBasket/App.config`:

```text
data source=Mohaiminul\SQLEXPRESS; database=NoteBasketDB; integrated security=SSPI
```

Before running the project on another machine, update the `NoteBasketDB` connection string in `NoteBasket/App.config` to match your local SQL Server instance.

The database tables referenced by the application include:

- `Users`
- `Notes`
- `Ratings`
- `Bookmarks`
- `Reports`
- `UserDailyActivity`

## Getting Started

1. Clone the repository.

   ```bash
   git clone https://github.com/mmHT2000/NoteBasket.git
   ```

2. Open `NoteBasket.sln` in Visual Studio.

3. Restore NuGet packages if Visual Studio does not restore them automatically.

4. Create or restore the `NoteBasketDB` database in Microsoft SQL Server.

5. Update the SQL Server connection string in `NoteBasket/App.config` for your machine.

6. Build and run the project.

The application starts from `Program.cs` and opens the login form first.

## Usage

1. Register a user account or sign in with an existing account.
2. Browse notes from the dashboard based on the current role/subscription level.
3. Upload notes as a NoteMaster or admin.
4. Approve, reject, edit, or delete notes from the relevant management screens.
5. Rate, review, bookmark, download, or report notes.
6. Upgrade a user subscription through the simulated payment flow.

## Academic Context

This repository is a university C# course project. It focuses on practicing:

- Windows Forms application design
- Event-driven programming
- CRUD operations with SQL Server
- Authentication and role-based workflows
- Multi-form navigation
- Basic desktop application architecture

## Configuration

Database access is centralized through `DatabaseConfig`, which reads the `NoteBasketDB` connection string from `NoteBasket/App.config`.

## Author

Md. Mohaiminul Haque

For questions or support, contact [mohaiminul.acc@gmail.com](mailto:mohaiminul.acc@gmail.com).
