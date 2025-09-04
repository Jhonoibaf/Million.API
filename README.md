# Million.Properties

La versión 1 de Million.Properties.API es un proyecto de tipo API Web que proporciona un CRUD para gestionar propiedades inmobiliarias.  
La aplicación permite realizar operaciones como:

- Consultar una lista de propiedades.
- Crear una nueva propiedad.
- Agregar imágenes a una propiedad.

La aplicación utiliza una arquitectura **Clean Architecture** con separación en capas y sigue el patrón **CQRS**. Para la persistencia de datos se usa **MongoDB** como base de datos principal.

---

## Tecnologías y Paquetes Usados

- **.NET 8** - Framework para la construcción de aplicaciones web.
- **MongoDB.Driver** - Cliente oficial para conectarse a MongoDB.
- **MediatR** - Librería para implementar el patrón CQRS y manejar comandos y consultas.
- **AutoMapper** - Mapeo entre entidades y DTOs.
- **NUnit** - Framework de pruebas unitarias.
- **Moq** - Librería para mocks en pruebas.
- **Docker & Docker Compose** - Para contenerización y despliegue de la aplicación.
- **Swagger / Swashbuckle** - Documentación interactiva de los endpoints.

## Estructura del Proyecto

La solución sigue un esquema de **Clean Architecture**:

- `Million.Properties.API` - Capa de **Presentación** que contiene los controladores de la API, configuración inicial (Program.cs) y Swagger.

- `Million.Properties.Application` - Capa de **Aplicación** que contiene la lógica de negocio.
  - **Contracts** - Interfaces de repositorios.
  - **DTOs** - Objetos de transferencia de datos.
  - **Features** - Comandos y consultas organizados con CQRS.
  - **Mappings** - Configuración de AutoMapper.

- `Million.Properties.Domain` - Capa de **Dominio** que contiene las entidades principales del negocio.

- `Million.Properties.Infrastructure` - Capa de **Infraestructura** que maneja la conexión con la base de datos MongoDB, los repositorios e inicialización.

- `Million.Properties.Test` - Capa de **Pruebas** que incluye:
  - **Application.UnitTest** - Pruebas unitarias de la lógica de negocio.
  - **Data.Test** - Pruebas relacionadas con datos.
  - **IntegrationTest** - Pruebas de integración para conectividad con MongoDB y repositorios.
  
## Ejecución del Proyecto
### 1. Clonar el repositorio

```bash
git clone <url-del-repositorio>
cd Million.Properties.API

### Ejecutar local
1. Configura `appsettings.json` -> `MongoSettings`.
2. `dotnet run --project ./Million.API`

### Docker (API + Mongo)
`docker-compose up --build`

La API queda en: http://localhost:5000  
Health: http://localhost:5000/health  
Swagger: http://localhost:5000/swagger

## Tests
- Unit: `dotnet test ./Million.Properties.Application.UnitTest`
- Integration (con Testcontainers): `dotnet test ./Million.Properties.IntegrationTest`

## Endpoints
- POST `/api/v1/Property`  
- GET  `/api/v1/Property/{id}`  
- GET  `/api/v1/Property`  (filtros: `name`, `address`, `minPrice`, `maxPrice`)
