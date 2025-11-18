# Estructura general del proyecto

Este archivo describe la estructura objetivo del proyecto **PokedexProject**.  
Algunas carpetas pueden no existir todavía en el repositorio y se irán creando conforme avance el desarrollo.

PokedexProject/
├── backend/                             # Backend ASP.NET Core (API REST)
│   ├── PokedexAPI.sln                   # Solución principal del backend (si aplica)
│   ├── PokedexAPI/                      # Proyecto Web API
│   │   ├── Controllers/                 # Controladores de la API (Auth, Pokemon, Users, Logs, etc.)
│   │   ├── Models/                      # Entidades de dominio / modelos de base de datos
│   │   ├── DTOs/                        # Data Transfer Objects para requests/responses
│   │   ├── Services/                    # Lógica de negocio (servicios de Pokémon, usuarios, auditoría, etc.)
│   │   ├── Repositories/                # Acceso a datos (Entity Framework Core)
│   │   ├── Data/                        # DbContext, configuración de EF Core, migraciones
│   │   │   ├── Migrations/              # Migraciones de base de datos
│   │   ├── Configuration/               # Configuración de CORS, JWT, Swagger, etc.
│   │   ├── Logging/                     # Configuración de log4net u otros proveedores de logs
│   │   ├── Middleware/                  # Middlewares personalizados (ej. manejo de excepciones, logs)
│   │   ├── Utils/                       # Clases utilitarias, helpers, constantes
│   │   ├── appsettings.json             # Configuración de conexión a SQL Server y JWT
│   │   ├── appsettings.Development.json # Config local (opcional)
│   │   ├── Program.cs                   # Punto de entrada de la Web API
│   │   └── PokedexAPI.csproj            # Proyecto de ASP.NET Core
│   ├── PokedexAPI.Tests/                # Proyecto de pruebas unitarias (xUnit)
│   │   ├── UnitTests/                   # Pruebas a servicios, controladores, repositorios, etc.
│   │   ├── IntegrationTests/            # Pruebas de integración (opcional)
│   │   └── PokedexAPI.Tests.csproj      # Proyecto de pruebas
│   ├── logs/                            # Carpeta donde se generan logs físicos (configurados en log4net)
│   └── README_BACKEND.md                # Documentación específica del backend (opcional)
│
├── frontend/                            # Frontend del proyecto
│   └── pokedex-frontend/                # Aplicación Angular (UI de la Pokedex)
│       ├── src/
│       │   ├── app/
│       │   │   ├── core/                # Servicios globales, guards, interceptores
│       │   │   ├── shared/              # Componentes y módulos compartidos (botones, cards, pipes, etc.)
│       │   │   ├── modules/
│       │   │   │   ├── auth/            # Módulo de autenticación (login, registro)
│       │   │   │   └── pokemon/         # Módulo principal de Pokedex (listado, detalle, búsqueda)
│       │   │   ├── app-routing.module.ts# Rutas principales de la app
│       │   │   └── app.component.*      # Componente raíz
│       │   ├── assets/                  # Imágenes, iconos, estilos globales
│       │   ├── environments/            # Archivos de configuración por entorno (API base URL, etc.)
│       │   └── index.html               # HTML principal de la SPA
│       ├── angular.json                 # Configuración del workspace de Angular
│       ├── package.json                 # Dependencias de Node/NPM para el frontend
│       ├── tsconfig.json                # Configuración de TypeScript
│       └── README_FRONTEND.md           # Documentación específica del frontend (opcional)
│
├── .gitignore                           # Archivos y carpetas ignorados por Git
├── DiagramaDeArquitectura_Pokedex.png   # Diagrama de arquitectura general del sistema
├── DiagramaERPokedex.png                # Diagrama Entidad-Relación (ER) de la base de datos
├── ESTRUCTURA_GENERAL.md                # Este archivo (vista general de estructura)
└── README.md                            # Documentación principal del proyecto