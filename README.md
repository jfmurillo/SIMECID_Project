# SIMECID — Sistema de Manejo de Establecimientos de Ciencias de la Salud

A full-stack medical clinic management web application built with ASP.NET Core 8 and Razor Pages. SIMECID enables clinics to manage appointments, patients, staff, branches, services, medical records, prescriptions, and examinations through a role-based portal.

---

## Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│  Browser                                                        │
│  WebApp (Razor Pages :7176)  ←→  WebAPI (REST :7144)           │
└──────────────┬──────────────────────────┬───────────────────────┘
               │                          │
         ┌─────▼──────┐            ┌──────▼──────┐
         │  CoreApp   │            │  CoreApp    │
         │ (Business  │            │ (Business   │
         │  Logic)    │            │  Logic)     │
         └─────┬──────┘            └──────┬──────┘
               │                          │
         ┌─────▼──────────────────────────▼──────┐
         │           DataAccess (DAOs / CRUD)     │
         └──────────────────┬─────────────────────┘
                            │
                     ┌──────▼──────┐
                     │    DTO      │
                     │ (Shared     │
                     │  Models)    │
                     └──────┬──────┘
                            │
                    ┌───────▼────────┐
                    │  Azure SQL DB  │
                    └────────────────┘
```

**Projects:**

| Project | Role |
|---|---|
| `WebApp` | Razor Pages UI — serves HTML, handles cookie auth, calls WebAPI via JS |
| `WebAPI` | REST API — exposes endpoints consumed by WebApp's client-side JavaScript |
| `CoreApp` | Business logic layer — managers, validation, email/OTP services |
| `DataAccess` | Data access layer — ADO.NET DAOs calling SQL Server stored procedures |
| `DTO` | Shared data transfer objects (POCOs) used across all layers |

---

## Tech Stack

- **Runtime:** .NET 8 / ASP.NET Core 8
- **Frontend:** Razor Pages, Bootstrap 5, jQuery 3, SweetAlert2, DataTables
- **Backend:** ADO.NET with SQL Server stored procedures
- **Database:** Azure SQL Server
- **Email / OTP:** Azure Communication Services
- **Auth:** ASP.NET Core Cookie Authentication with role-based authorization
- **Secrets:** `dotnet user-secrets` (never committed to source control)

---

## User Roles

| Role | Dashboard Capabilities |
|---|---|
| **Admin** | Manage users, branches, services, appointments; view reports; configure system settings |
| **Doctor** | View assigned appointments; create examinations, medical reports, and prescriptions |
| **Nurse** | Record examinations; view and update medical reports and prescriptions |
| **Secretary** | Schedule and manage appointments; access patient medical reports |
| **User (Patient)** | Book appointments; view personal medical records and settings |

---

## Local Development Setup

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Access to an Azure SQL Server instance with the SIMECID schema
- An Azure Communication Services resource (for email/OTP)

### 1. Clone the repository

```bash
git clone <repo-url>
cd SIMECID_Project
```

### 2. Set user secrets — WebApp

```bash
cd WebApp
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=<server>;Database=<db>;User Id=<user>;Password=<password>;"
dotnet user-secrets set "ConnectionStrings:CalendarContext" "Server=<server>;Database=<db>;User Id=<user>;Password=<password>;"
dotnet user-secrets set "AzureCommunication:ConnectionString" "endpoint=https://<resource>.communication.azure.com/;accesskey=<key>"
cd ..
```

### 3. Set user secrets — WebAPI

```bash
cd WebAPI
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=<server>;Database=<db>;User Id=<user>;Password=<password>;"
dotnet user-secrets set "AzureCommunication:ConnectionString" "endpoint=https://<resource>.communication.azure.com/;accesskey=<key>"
cd ..
```

### 4. Run both projects

Open two terminal windows:

**Terminal 1 — WebAPI (port 7144):**
```bash
cd WebAPI
dotnet run
```

**Terminal 2 — WebApp (port 7176):**
```bash
cd WebApp
dotnet run
```

Then open `https://localhost:7176` in your browser.

> The WebApp's `appsettings.json` has `"ApiBaseUrl": "https://localhost:7144/api/"` pre-configured. If your WebAPI runs on a different port, update this value.

---

## Project Structure

```
SIMECID_Project/
├── WebApp/                     # Razor Pages frontend
│   ├── Pages/                  # All .cshtml + .cshtml.cs page files
│   │   ├── Shared/             # _Layout.cshtml
│   │   ├── Login.cshtml        # Entry point (cookie auth)
│   │   ├── Doctor-*.cshtml     # Doctor role pages
│   │   ├── Nurse-*.cshtml      # Nurse role pages
│   │   ├── Secretary-*.cshtml  # Secretary role pages
│   │   ├── Patient-*.cshtml    # Patient role pages
│   │   └── *.cshtml            # Admin + shared pages
│   ├── wwwroot/
│   │   ├── css/                # Per-page and shared stylesheets
│   │   └── js/
│   │       ├── Pages/          # Per-page JavaScript logic
│   │       ├── Redirect/       # Post-login redirect scripts
│   │       ├── ControlActions.js  # Shared API client (jQuery AJAX)
│   │       └── login.js / signup.js / logout.js
│   ├── Controllers/            # MVC controllers (file upload)
│   └── Program.cs              # DI, cookie auth, CORS configuration
├── WebAPI/
│   ├── Controllers/            # REST API controllers by domain
│   └── Program.cs
├── CoreApp/                    # Business logic managers
│   ├── UserManager.cs
│   ├── AppointmentManager.cs
│   ├── EmailManager.cs
│   └── ApptAlertManager.cs
├── DataAccess/
│   ├── DAOs/SqlDao.cs          # ADO.NET base DAO
│   └── CRUD/                   # Per-entity CRUD factories
├── DTO/                        # Shared POCOs (User, Appointment, etc.)
└── SIMECID_Project.sln
```

---

## Known Limitations

- **Passwords are stored as plain text.** Phase 5 (password hashing via `PasswordHasher<User>`) is planned but gated on a database migration — all existing stored procedures that compare passwords must be updated to fetch-by-email-only before hashing can be enabled.
- **No shared auth layout.** Each role's pages include the sidebar directly rather than inheriting from a shared `_AuthLayout`. This is a refactoring candidate.
- **Calendar feature is incomplete.** `Calendario.cshtml` is present but the backing logic is not fully wired up.

---

## Screenshots

---

## License

© 2024 Codecrafters. All rights reserved.
