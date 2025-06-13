# 🏢 Community Space Reservation System

This is a web-based reservation system that allows users to book community spaces such as halls, courts, and auditoriums. The application is built using **ASP.NET Core Web API**, follows **Hexagonal Architecture**, and applies the **CQRS** design pattern for organizing application logic. It is fully containerized with **Docker**.

---

## 🚀 Features

- ✅ User Registration & JWT Authentication
- 📅 Browse available community spaces with filtering, pagination, and sorting
- 🗓️ Make reservations by specifying start and end dates
- 📬 Email confirmation sent automatically with a unique confirmation link
- 👤 Users can view their own reservations
- 🔒 Admins can view all existing reservations
- ⛔ Unauthorized users are restricted from creating or viewing reservations
- 🐳 Docker-based deployment (Web API, SQL Server, SMTP server)

---

## 🧱 Tech Stack

- **Backend**: ASP.NET Core Web API
- **Architecture**: Hexagonal Architecture
- **Pattern**: CQRS (Command and Query Responsibility Segregation)
- **Database**: SQL Server
- **Authentication**: JWT (JSON Web Token)
- **Email**: Mock SMTP server Papercut
- **Containerization**: Docker & Docker Compose

---

## 🛠️ Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/products/docker-desktop/)

### Cloning the Repository
```bash
https://github.com/skeytor/reserve-hub.git
cd reserve-hub
```

### Running the Project with Docker

```bash
docker-compose up --build
```
### API Documentation
- **Scalar UI**: [http://localhost:5000](http://localhost:5000/scalar/v1/)

