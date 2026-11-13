# Sistema de Gestión de Pacientes

CRUD completo de un sistema de pacientes con **API REST en .NET 8** siguiendo
**arquitectura N-Capas**, consumido por un **frontend en Angular 17**.
Base de datos **SQLite** (no requiere instalar nada; el archivo se crea solo).

## Arquitectura N-Capas (y dónde está cada concepto)

```
src/
├── SistemaPacientes.Domain          → Capa de Dominio (núcleo, sin dependencias)
│   ├── Common/BaseEntity.cs           herencia + auditoría
│   ├── Entities/*.cs                  12 entidades del modelo
│   ├── Enums/                         enumeraciones de negocio
│   └── Interfaces/                    IGenericRepository, IUnitOfWork (abstracciones)
│
├── SistemaPacientes.Application     → Capa de Aplicación (lógica de negocio)
│   ├── DTOs/                          objetos de transferencia (desacople del modelo)
│   ├── Interfaces/                    IGenericService + I{Entidad}Service
│   ├── Services/                      GenericService + servicios por entidad
│   ├── Mappings/MappingProfile.cs     AutoMapper (Entidad <-> DTO)
│   └── DependencyInjection.cs         AddApplication()
│
├── SistemaPacientes.Infrastructure → Capa de Infraestructura (acceso a datos)
│   ├── Persistence/ApplicationDbContext.cs   EF Core Code First + Fluent API
│   ├── Persistence/DbInitializer.cs          datos de ejemplo (seed)
│   ├── Repositories/GenericRepository.cs     patrón Repository
│   ├── Repositories/UnitOfWork.cs            patrón Unit of Work
│   └── DependencyInjection.cs                AddInfrastructure()
│
└── SistemaPacientes.API             → Capa de Presentación (Web API)
    ├── Controllers/GenericController.cs       controlador base REST
    ├── Controllers/*Controller.cs            un controlador por entidad
    ├── Middleware/ExceptionMiddleware.cs      manejo global de errores
    └── Program.cs                             composición e inyección de dependencias
```

**Conceptos de N-Capas aplicados:** separación de responsabilidades · inversión de
dependencias (las capas dependen de interfaces, no de implementaciones) · patrón
Repository genérico · Unit of Work · capa de servicios con interfaces · DTOs ·
AutoMapper · inyección de dependencias · EF Core (Code First) · middleware ·
validaciones con DataAnnotations.

## Las 12 tablas

Especialidad, Consultorio, Médico, Paciente, Cita, Consulta, SignoVital,
Diagnóstico, Tratamiento, Medicamento, Receta, RecetaDetalle — con relaciones
1..*, 1..1 y tablas de detalle.

## Cómo ejecutar

### 1) API (.NET 8)
Requisitos: **.NET 8 SDK**.

```bash
cd src
dotnet restore
dotnet run --project SistemaPacientes.API
```
- Al arrancar crea `sistema_pacientes.db` y carga datos de ejemplo.
- Swagger: http://localhost:5159/swagger
- En Visual Studio: abre `src/SistemaPacientes.sln`, marca `SistemaPacientes.API`
  como proyecto de inicio y presiona **F5**.

### 2) Frontend (Angular 17)
Requisitos: **Node.js 18+**.

```bash
cd frontend-angular
npm install
npm start
```
- App: http://localhost:4200
- El API ya tiene CORS habilitado para ese origen.
- La URL del API se configura en `src/environments/environment.development.ts`.

> Si cambias el puerto del API, actualiza esa URL.

## Migraciones (opcional)
El proyecto usa `EnsureCreated()` para no depender de la CLI. Si prefieres
migraciones de EF Core:
```bash
cd src
dotnet tool install --global dotnet-ef
dotnet ef migrations add Inicial -p SistemaPacientes.Infrastructure -s SistemaPacientes.API
dotnet ef database update -p SistemaPacientes.Infrastructure -s SistemaPacientes.API
```
(y reemplaza `EnsureCreated()` por `context.Database.Migrate()` en `Program.cs`).
