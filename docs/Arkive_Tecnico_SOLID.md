# Arkive — Patrones y Principios SOLID

## Patrones aplicados

**Repository Pattern**
- **Dónde:** `Arkive.Application/Abstractions/IDocumentoRepository.cs` (contrato) y `Arkive.Infrastructure/Repositories/DocumentoRepository.cs` (implementación).
- **Para qué:** aislar el acceso a datos (EF Core/SQL Server) del resto de la app. Permite cambiar el storage (p.ej. Mongo) sin tocar controladores ni servicios.

**Service Layer**
- **Dónde:** `Arkive.Application/Services/IDocumentoService.cs` y `DocumentoService.cs`.
- **Para qué:** concentrar la lógica de negocio (validaciones, mapeos DTO↔Entidad) y orquestar repositorios. Controladores quedan delgados.

**Dependency Injection (DI)**
- **Dónde:** `Arkive.Api/Program.cs`
  ```csharp
  builder.Services.AddScoped<IDocumentoRepository, DocumentoRepository>();
  builder.Services.AddScoped<IDocumentoService, DocumentoService>();
  ```
- **Para qué:** invertir dependencias y facilitar pruebas (mocks), cumpliendo DIP.

## Principios SOLID usados

**S — Single Responsibility Principle**
- Cada capa tiene una única razón de cambio:
  - `Documento` (Domain): solo modelo.
  - `DocumentoService` (Application): reglas de negocio/DTOs.
  - `DocumentoRepository` (Infrastructure): persistencia EF.
  - `DocumentosController` (Api): transporte HTTP.

**O — Open/Closed Principle**
- El repositorio está abierto a extensión (nuevos filtros/búsquedas) y cerrado a modificación del contrato existente. Nuevas queries pueden añadirse sin romper a los clientes.

**L — Liskov Substitution Principle**
- Cualquier implementación de `IDocumentoRepository` (EF, memoria, pruebas) puede sustituir a `DocumentoRepository` sin romper a `DocumentoService`.

**I — Interface Segregation Principle**
- Interfaces finas y específicas: `IDocumentoRepository` y `IDocumentoService` solo exponen lo que consume la capa superior (no interfaces “Dios”).

**D — Dependency Inversion Principle**
- Controladores dependen de **abstracciones** (`IDocumentoService`) y servicios dependen de **abstracciones** (`IDocumentoRepository`). La infraestructura (detalles) se inyecta en tiempo de ejecución.

## Arquitectura seleccionada (breve)
- **Clean-ish por capas**: Domain (modelos), Application (DTOs/servicios), Infrastructure (EF/Repos), Api (transport).
- Beneficios: testabilidad, mantenibilidad, reemplazo de infraestructura sin tocar reglas, separación clara de responsabilidades.

## Diagrama mínimo de dependencias
```
Arkive.Api --> Arkive.Application --> Arkive.Domain
         \--> Arkive.Infrastructure --> Arkive.Domain
```

## Evidencia de endpoints
- CRUD y búsqueda funcionando vía Swagger (`/swagger`).
- Respuestas 201/200/204/404 según caso.


📘 **Autor:** Holman Alba  
