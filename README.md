
# 🗃️ Arkive API — Sistema de Gestión Documental y Analítica

![.NET](https://img.shields.io/badge/.NET-8.0-blue?style=flat-square&logo=dotnet)
![SQL Server](https://img.shields.io/badge/Base%20de%20Datos-SQL%20Server-red?style=flat-square&logo=microsoftsqlserver)
![Estado](https://img.shields.io/badge/Estado-Estable-success?style=flat-square)
![Licencia](https://img.shields.io/badge/Licencia-MIT-green?style=flat-square)

> **Arkive** es una API moderna para la gestión documental y analítica de información.  
> Construida en **ASP.NET Core** con **Entity Framework Core** y **SQL Server**, implementa arquitectura limpia, principios **SOLID**, y un diseño escalable y mantenible.

---

##  Tecnologías Utilizadas

| Capa | Tecnología |
|------|-------------|
| **Backend Principal** | ASP.NET Core 8 |
| **Acceso a Datos** | Entity Framework Core |
| **Base de Datos** | Microsoft SQL Server |
| **Lenguaje** | C# |
| **Arquitectura** | Clean Architecture (API, Application, Domain, Infrastructure) |
| **Documentación** | Swagger / OpenAPI 3.0 |
| **Control de Versiones** | Git & GitHub |

---

##  Características Principales

- API RESTful con operaciones CRUD para documentos  
- Búsqueda avanzada por autor, tipo y estado  
- Soporte de paginación y filtros dinámicos  
- Patrón **Repositorio** con **Inyección de Dependencias (DI)**  
- Uso de principios **SOLID** en la capa de aplicación  
- Integración completa con **Entity Framework Core + SQL Server**  
- Scripts SQL para configuración manual de base de datos  
- Arquitectura limpia, modular y de fácil mantenimiento  

---

##  Estructura del Proyecto

```
Arkive/
├── Arkive.Api/              → Punto de entrada, controladores, Swagger
├── Arkive.Application/      → Lógica de negocio, DTOs, interfaces
├── Arkive.Domain/           → Entidades del dominio
├── Arkive.Infrastructure/   → Persistencia, repositorios, contexto EF
└── docs/                    → Scripts SQL y documentación técnica
```

---

##  Configuración de la Base de Datos

Puedes inicializar la base de datos manualmente ejecutando los scripts en el siguiente orden:

1. `database_setup.sql` — crea la base y la tabla `Documentos`  
2. `seed_data.sql` — inserta registros de prueba  
3. `queries_optimizadas.sql` — crea índices y procedimientos almacenados  

O hacerlo mediante **Entity Framework Core**:

```bash
dotnet ef database update -p Arkive.Infrastructure -s Arkive.Api
```

---

##  Endpoints Principales

Una vez ejecutado el proyecto, accede a Swagger UI en:  
🔗 [https://localhost:7281/swagger](https://localhost:7281/swagger)

| Método | Endpoint | Descripción |
|---------|-----------|-------------|
| **POST** | `/api/Documentos` | Crear un documento |
| **GET** | `/api/Documentos` | Listar documentos con paginación |
| **GET** | `/api/Documentos/{id}` | Obtener documento por ID |
| **PUT** | `/api/Documentos/{id}` | Actualizar documento existente |
| **DELETE** | `/api/Documentos/{id}` | Eliminar documento |
| **GET** | `/api/Documentos/buscar` | Buscar por autor, tipo o estado |

---

##  Carpeta `docs/`

Incluye documentación y scripts de base de datos:

- `database_setup.sql` → Crea la base **ArkiveDb**
- `seed_data.sql` → Inserta datos de prueba
- `queries_optimizadas.sql` → Procedimientos e índices
- `README_SQL.md` → Guía de ejecución SQL
- `Arkive_Tecnico_SOLID.md` → Documento técnico sobre SOLID y patrones aplicados

---

## ✨ Autor

**Holman Alba**  
 Software Developer 
 Contacto: holman.alba@repremundo.com.co  
 GitHub: [holman25](https://github.com/holman25)

---

##  Licencia

Este proyecto se distribuye bajo la licencia **MIT**.  
Puedes usarlo, modificarlo y adaptarlo libremente citando al autor original.

---
