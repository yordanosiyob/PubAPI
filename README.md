# PubAPI

A RESTful Web API for managing authors and books, built with ASP.NET Core and Entity Framework Core.

## Features
- Full CRUD endpoints for authors and books
- Related data loaded with eager loading (authors with their books, books with their author)
- Interactive API documentation and testing with Swagger
- Database managed with EF Core migrations

## Endpoints
| Method | Route | Description |
|---|---|---|
| GET | `/authors` | List all authors with their books |
| GET | `/authors/{id}` | Get one author |
| POST | `/authors` | Create an author |
| PUT | `/authors/{id}` | Update an author |
| DELETE | `/authors/{id}` | Delete an author |
| GET | `/books` | List all books with their author |
| GET | `/books/{id}` | Get one book |
| POST | `/books` | Create a book |
| PUT | `/books/{id}` | Update a book |
| DELETE | `/books/{id}` | Delete a book |

## Tech stack
- C# / .NET 9, ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI

## Project structure
- **PubAPI**: Web API with controllers
- **PublisherData**: DbContext and migrations
- **PublisherDomain**: domain models

## My contribution
The data model and initial migrations were provided by the course instructor. I built the Web API layer: the controllers with CRUD operations, dependency injection of the DbContext, Swagger configuration and JSON handling of circular references, and I updated the migrations to work with the current setup.

## How to run
1. Make sure SQL Server is running and update the connection string in `appsettings.json` if needed
2. Apply migrations: `dotnet ef database update --project PublisherData --startup-project PubAPI`
3. Run: `dotnet run --project PubAPI`
4. Open `/swagger` in the browser

## Possible improvements
- DTOs instead of exposing entities directly
- Input validation
- Unit and integration tests
