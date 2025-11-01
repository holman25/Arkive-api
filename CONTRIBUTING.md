# Contributing to Arkive API

Thank you for considering contributing to **Arkive API**!  
This project follows clean architecture principles and enforces code quality, consistency, and maintainability.  
Please take a moment to review these guidelines before submitting any changes.

---

## 🧩 Project structure
```
Arkive.Api/             → Entry point (.NET Web API)
Arkive.Application/     → DTOs, interfaces, and service logic
Arkive.Domain/          → Core entities and business models
Arkive.Infrastructure/  → Persistence, repositories, EF context
docs/                   → Technical documentation, SQL scripts, guides
```

---

## 🚀 How to contribute

### 1. Fork and clone
```bash
git clone https://github.com/<your-username>/Arkive-api.git
cd Arkive-api
```

### 2. Create a branch
Use a short descriptive name:
```bash
git checkout -b feat/add-pagination
```

### 3. Follow conventions
- **Naming:** Use PascalCase for classes, camelCase for variables.
- **Commits:** Follow [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/):
  - `feat:` new feature  
  - `fix:` bug fix  
  - `docs:` documentation  
  - `refactor:` code improvement  
  - `test:` unit/integration tests  

### 4. Code style
- Apply **SOLID** principles.
- Avoid circular dependencies between layers.
- Validate nulls and asynchronous calls properly.
- Use **dependency injection** for all services and repositories.

### 5. Run tests and lint
Before committing:
```bash
dotnet build
dotnet test
```

### 6. Submit Pull Request
Open a PR against `develop` (not `main`) and include:
- A clear description of the change.
- Screenshots or logs if applicable.
- Reference any related issues.

---

## 🧱 Development setup
- **.NET SDK:** 8.0+
- **Database:** SQL Server 2019+
- **IDE:** Visual Studio 2022 or VS Code
- **Connection string:** Configured via `appsettings.Development.json` (not committed to Git)

---

## 🧠 Best practices
- Keep functions small and focused.
- Write meaningful commit messages.
- Document public methods when logic is not obvious.
- Keep your branch up to date with `develop`.

---

Thank you for contributing to Arkive ❤️
