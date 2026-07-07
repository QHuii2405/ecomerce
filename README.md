# Ecommerce Order & Inventory System

This repository contains the backend and frontend services for the E-commerce platform. The system is designed to handle order processing and inventory management, built with .NET 10 and React.

## Architecture & Tech Stack

- **Backend:** .NET 10, ASP.NET Core Web API, Entity Framework Core
- **Frontend:** React
- **Database:** Microsoft SQL Server
- **Caching:** Redis
- **Infrastructure:** Docker, Kubernetes, Terraform, GitHub Actions

## Key Features

- Structured using Clean Architecture principles.
- Inventory reservation mechanism to prevent overselling during checkout.
- JWT-based authentication and authorization.
- Redis caching for product query optimization.
- Background services for automated tasks (e.g., canceling unpaid orders).

## Prerequisites

- Docker and Docker Compose
- .NET 10 SDK
- Node.js 20+ (for frontend development)

## Local Development Setup

1. Start the required infrastructure (SQL Server and Redis):
   ```bash
   docker-compose up -d sqlserver redis
   ```
2. Apply database migrations:
   ```bash
   dotnet ef database update --project src/Infrastructure --startup-project src/WebAPI
   ```
3. Run the backend service:
   ```bash
   dotnet run --project src/WebAPI
   ```
4. The Swagger API documentation will be available at `http://localhost:5092/swagger`.

## Deployment

The repository includes CI/CD pipelines via GitHub Actions and infrastructure as code configurations.
- **Docker Compose:** Use `docker-compose.prod.yml` for standard deployments.
- **Kubernetes:** Manifests are located in the `k8s/` directory.
- **Terraform:** Infrastructure provisioning scripts are located in `terraform/`.