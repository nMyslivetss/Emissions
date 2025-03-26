# Emissions

Emissions is a simple API application for retrieving and transforming environmental emissions data. It is built with **ASP.NET Core** and simulates external integration via a **Fake Background API**, using **clean architecture principles**, **AutoMapper**, and **Swagger** for documentation.

## Features

- Versioned API endpoints (v1, v2)
- RESTful GET endpoint to retrieve emissions data by customer, facility, and time period
- Fake Background API simulating external service
- AutoMapper for DTO mapping
- Exception middleware for unified error handling
- Flexible filtering via query parameters
- Swagger UI for testing and documentation

## Projects Structure

- `Emissions.API` – Main API with controllers, middleware, startup logic
- `Emissions.Application` – Business logic, services, mapping profiles
- `Emissions.Contracts` – DTOs and shared models
- `Emissions.Domain` – Constants and enums (e.g. Scope mapping)
- `Emissions.Infrastructure` – HTTP client for background API and helpers
- `Emissions.FakeBackgroundApi` – Simulated background API providing fake emission data
