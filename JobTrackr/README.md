# JobTrackr 🎯

A full-stack job application tracker built with **.NET Core 8** backend and **Angular** frontend. Built to manage a real Europe job search.

## Tech Stack

**Backend**
- .NET Core 8 Web API — Clean Architecture
- Entity Framework Core 8 + SQL Server
- JWT Authentication
- Swagger / OpenAPI
- Docker + Docker Compose
- GitHub Actions CI/CD

**Frontend** *(in progress)*
- Angular 17 (standalone components)
- TypeScript

## Architecture

```
JobTrackr/
├── src/
│   ├── JobTrackr.API           → Controllers, Program.cs
│   ├── JobTrackr.Application   → Services, DTOs, Interfaces
│   ├── JobTrackr.Domain        → Entities, Enums
│   └── JobTrackr.Infrastructure → EF Core, Repositories, JWT
├── JobTrackr.Angular/          → Angular frontend
├── docker-compose.yml
└── .github/workflows/          → CI/CD pipeline
```

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server (local or Docker)
- Node.js 18+ (for Angular)

### Run Locally

1. Clone the repo
```bash
git clone https://github.com/PBWim/JobTrackr.git
cd JobTrackr
```

2. Update connection string in `src/JobTrackr.API/appsettings.json`

3. Run the API
```bash
cd src/JobTrackr.API
dotnet run
```

4. Open Swagger at `https://localhost:5001/swagger`

### Run with Docker
```bash
docker-compose up --build
```

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | /api/auth/register | Register new user |
| POST | /api/auth/login | Login and get JWT token |
| GET | /api/jobapplications | Get all applications |
| POST | /api/jobapplications | Create application |
| PUT | /api/jobapplications/{id} | Update application |
| DELETE | /api/jobapplications/{id} | Delete application |
| GET | /api/jobapplications/dashboard | Get stats |

## Features
- Track job applications with status (Applied → Screening → Interview → Offer)
- Dashboard with response rate, interview count, stats by country
- JWT secured endpoints
- Clean Architecture with proper separation of concerns
