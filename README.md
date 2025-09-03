# Million.Properties

## Requisitos
- .NET 8 SDK
- Docker (opcional para levantar Mongo y API)

## Backend
### Ejecutar local
1. Configura `appsettings.json` -> `MongoSettings`.
2. `dotnet run --project ./Million.API`

### Docker (API + Mongo)
`docker-compose up --build`

La API queda en: http://localhost:8080  
Health: http://localhost:8080/health  
Swagger: http://localhost:8080/swagger

## Tests
- Unit: `dotnet test ./Million.Properties.Application.UnitTest`
- Integration (con Testcontainers): `dotnet test ./Million.Properties.IntegrationTest`

## Endpoints
- POST `/api/v1/Property`  
- GET  `/api/v1/Property/{id}`  
- GET  `/api/v1/Property`  (filtros: `name`, `address`, `minPrice`, `maxPrice`)
