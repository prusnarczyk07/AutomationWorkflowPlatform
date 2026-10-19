# Automation Platform

A mini workflow automation platform built with ASP.NET Core and PostgreSQL.

The application allows users to create workflows consisting of multiple steps and execute them through webhook triggers.

## Features

- Workflow CRUD operations
- Webhook-triggered workflow execution
- Multiple workflow steps
- HTTP step handler
- Discord step handler
- Log step handler
- PostgreSQL database with Entity Framework Core
- Input validation
- Error handling
- Docker and Docker Compose support
- Unit tests
- OpenAPI documentation

## Architecture

The application follows a simple layered architecture:

Controller -> Service -> Repository -> Entity Framework Core -> PostgreSQL

### Workflow execution

Webhook -> Workflow -> Workflow Steps -> Step Handlers - HTTP/Discord/Log

## Technologies

- C#
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Docker
- Docker Compose
- xUnit
- Moq

## Running with Docker

The easiest way to run the application is with Docker Compose.

Start the application:

```bash
docker compose up -d --build

```

The API will be available at:

http://localhost:8080


Stop the application:
```bash
docker compose down

```


## Configuration

Discord notifications require a Discord webhook URL. Set the `DISCORD_WEBHOOK_URL` environment variable before starting Docker Compose.

In PowerShell:

```bash

$env:DISCORD_WEBHOOK_URL = "YOUR_DISCORD_WEBHOOK_URL"

docker compose up -d --build
```

Keep the webhook URL secret. Do not commit it to source control.

## API

### Workflows

Create a workflow:

POST /api/Webhook/workflow
{
  "name": "HTTP notification",
  "trigger": "test",
  "steps": [
    {
      "type": "http",
      "url": "https://httpbin.org/post",
      "method": "POST"
    }
  ]
}
Execute the workflow:

POST /api/Webhook/test

Example webhook body:
{
  "id": 1,
  "name": "Example webhook",
  "description": "Test webhook payload"
}

Example Discord workflow:

{
  "name": "Discord test",
  "trigger": "discord-test",
  "steps": [
    {
      "type": "discord",
      "url": "https://example.com",
      "method": "POST"
    }
  ]
}

Example Discord workflow execution:

POST /api/Webhook/discord-test

Example webhook body:

{
  "id": 1,
  "name": "Test Discord",
  "description": "Test message from Automation Platform"
}


The Discord step uses the webhook URL configured through DISCORD_WEBHOOK_URL.

Get all workflows:

GET /api/Webhook/workflows

Get a workflow:

GET /api/Webhook/workflow/{id}

Update a workflow:

PUT /api/Webhook/workflow/{id}

Delete a workflow:

DELETE /api/Webhook/workflow/{id}

### Webhook

Execute a workflow:

POST /api/Webhook/{trigger}

## Testing

Run the tests:

dotnet test

The project includes unit tests using xUnit and Moq.

## Project Structure

- `Controllers/` - API endpoints
- `Services/` - business logic and workflow execution
- `Repositories/` - data access
- `Models/` - application models
- `Dtos/` - API request models
- `Data/` - Entity Framework Core DbContext
- `automation-platform.Tests/` - unit tests
- `docker-compose.yml` - API and PostgreSQL containers
