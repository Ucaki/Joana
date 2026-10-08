# Joana — AgroApoteka Online Shop

A full-stack e-commerce platform for an agricultural supply store, built as a diploma
project with the goal of evolving into a production application.

## Tech Stack

- **Backend:** ASP.NET Core Web API (.NET 10, C# 14)
- **ORM:** Entity Framework Core + Npgsql
- **Database:** PostgreSQL
- **Frontend:** Blazor
- **Auth:** JWT (role-based access for Customer / Administrator)
- **Architecture:** Clean Architecture

## Project Structure

| Project                 Responsibility |

| `Joana.Domain`         | Entities, enums, business rules                     |
| `Joana.Application`    | Interfaces, services, DTOs                          |
| `Joana.Infrastructure` | EF Core, repositories, DbContext, migrations        | 
| `Joana.API`            | Controllers, middleware, composition root           |
| `Joana.Web`            | Blazor frontend (customer storefront + admin panel) |

## Getting Started

### Prerequisites

- .NET 10 SDK
- PostgreSQL running locally

## API Documentation

When the API is running in Development mode, Swagger UI is available at:

https://localhost:7288/swagger

(adjust the port to match your `launchSettings.json` profile)

### Setup

1. Clone the repository.
2. Create a local PostgreSQL database (any name).
3. This project keeps credentials out of source control - supply your own locally, using **one** of the two options below.

   **Option A — `appsettings.Development.json`** (create in `Joana.API/`, already gitignored):
```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=<your_db_name>;Username=<your_user>;Password=<your_password>;Options=-c TimeZone=Europe/Belgrade"
     },
     "Jwt": {
       "Key": "<a random string, 32+ characters>",
       "Issuer": "JoanaAPI",
       "Audience": "JoanaClient",
       "ExpiresInMinutes": 60
     }
   }
```

   **Option B — .NET User Secrets:**
```bash
   cd Joana.API
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=<your_db_name>;Username=<your_user>;Password=<your_password>"
   dotnet user-secrets set "Jwt:Key" "<a random string, 32+ characters>"
```

4. Apply migrations (creates schema + seed data):
```bash
   dotnet ef database update --project Joana.Infrastructure --startup-project Joana.API
```
5. Run the API:
```bash
   cd Joana.API && dotnet run
```
6. Run the Blazor frontend (separate terminal):
```bash
   cd Joana.Web && dotnet run
```

## Status

Actively in development
