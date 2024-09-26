# Pokedex Project

Este es un proyecto de **Pokedex** que utiliza la [PokeAPI](https://pokeapi.co/) para obtener información de Pokémon. El proyecto está desarrollado con una arquitectura de software bien definida y cuenta con funcionalidades como autenticación, logs de auditoría, CORS, pruebas unitarias, y documentación mediante Swagger. También incluye un **Frontend** en Angular y un **Backend** en ASP.NET Core.

## Tabla de contenidos

1. [Descripción General](#descripción-general)
2. [Diagrama ER](#diagrama-er)
3. [Diagrama de Arquitectura](#diagrama-de-arquitectura)
4. [Características](#características)
5. [Tecnologías Utilizadas](#tecnologías-utilizadas)
6. [Configuración del Proyecto](#configuración-del-proyecto)
7. [Pruebas Unitarias](#pruebas-unitarias)
8. [Instrucciones de Uso](#instrucciones-de-uso)
9. [Logs de Auditoría](#logs-de-auditoría)

## Descripción General

El **Pokedex Project** es una aplicación web que permite a los usuarios buscar información sobre Pokémon, incluyendo:
- Nombre
- Número (ID)
- Imagen frontal
- Tipo(s) de Pokémon
- Altura
- Peso
- Descripción
- Cadena de evolución

La aplicación cuenta con autenticación mediante JWT, sistema de auditoría de acciones y backend con ASP.NET Core como API REST. El frontend está desarrollado en Angular.

## Diagrama ER

A continuación se muestra el diagrama entidad-relación (ER) del proyecto, que define las relaciones entre usuarios, logs de auditoría y datos obtenidos de la PokeAPI.

![Diagrama ER](/DiagramaERPokedex.png)

## Diagrama de Arquitectura

El siguiente es el diagrama de la arquitectura del proyecto, mostrando cómo están estructuradas las capas de presentación, lógica de negocio y acceso a datos, así como la integración con la API externa PokeAPI.

![Diagrama de Arquitectura](/DiagramaDeArquitectura_Pokedex.png)

## Características

### Backend
- **Autenticación JWT**: Autenticación segura con JSON Web Tokens.
- **Logs de Auditoría**: Todas las acciones realizadas por los usuarios se registran en la base de datos.
- **CORS**: Configurado para permitir el acceso desde diferentes orígenes.
- **Acceso a datos con Entity Framework Core**: ORM para consultas eficientes a la base de datos.
- **Sistema de logs**: Implementado con **log4net** para registrar eventos importantes del sistema.
- **Pruebas Unitarias con xUnit**: Pruebas robustas para garantizar la calidad del código.
- **Documentación con Swagger**: La API está documentada automáticamente usando Swagger.

### Frontend
- **Búsqueda de Pokémon por ID o Nombre**: Los usuarios pueden buscar Pokémon fácilmente.
- **Infinite Scroll**: El listado de Pokémon se carga automáticamente conforme el usuario se desplaza.
- **Interfaz amigable**: Diseñado en Angular con una experiencia de usuario optimizada.
- **Tailwind CSS**: Se utiliza para estilos personalizados en el frontend.

## Tecnologías Utilizadas

### Backend
- **ASP.NET Core**: Framework para la creación de APIs REST.
- **Entity Framework Core**: ORM para el acceso a la base de datos.
- **JWT**: Autenticación de usuarios.
- **log4net**: Sistema de logging.
- **xUnit**: Pruebas unitarias.
- **Swagger**: Documentación de la API.

### Frontend
- **Angular**: Framework frontend para la creación de interfaces de usuario.
- **TypeScript**: Lenguaje de programación para el frontend.
- **HTML5/CSS3**: Estructura y estilos de la interfaz de usuario.
- **Tailwind CSS**: Para el diseño responsivo del frontend.

### Base de Datos
- **SQL Server**: Base de datos utilizada para almacenar usuarios y logs de auditoría.

### API Externa
- **PokeAPI**: API pública utilizada para obtener la información de los Pokémon.

## Configuración del Proyecto

### Requisitos Previos

- **Node.js** y **npm** instalados para el desarrollo del frontend.
- **.NET Core SDK** para el desarrollo del backend.
- **SQL Server** para la base de datos.

### Paquetes NuGet a instalar (Backend)

1. **Microsoft.EntityFrameworkCore** (versión 8.0.8)
2. **Microsoft.EntityFrameworkCore.SqlServer** (versión 8.0.8)
3. **Microsoft.EntityFrameworkCore.Tools** (versión 8.0.8)
4. **Microsoft.AspNetCore.Authentication.JwtBearer** (versión 8.0.8)
5. **Newtonsoft.Json** (versión 13.0.3)
6. **log4net** (versión 3.0.0)
7. **Swashbuckle.AspNetCore** (versión 6.8.0)
8. **Moq** (versión 4.20.72)
9. **coverlet.collector** (versión 6.0.0)
10. **xUnit** (versión 2.9.1)
11. **xUnit.runner.visualstudio** (versión 2.8.2)

### Configuración del appsettings.json

Debes actualizar la cadena de conexión en el archivo `appsettings.json` para reflejar tu configuración local:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=PokedexDB;Trusted_Connection=True;"
  },
  "Jwt": {
    "Key": "TuClaveSecretaAqui",
    "Issuer": "TuDominio.com",
    "Audience": "TuDominio.com"
  }
}
```
---

## Pasos de Instalación

### Clonar el Repositorio

``` bash
git clone https://github.com/Xalot/PokedexProject.git
cd PokedexProject/backend
```
### Restaurar Paquetes
``` bash
dotnet restore
```
### Configurar la Base de Datos

- Asegúrate de tener SQL Server configurado localmente.
- Actualiza la cadena de conexión en _appsettings.json_ si es necesario.

### Aplicar Migraciones (si es necesario)
``` bash
dotnet ef database update
```

### Ejecutar el Servidor
``` bash
dotnet run
```

### Acceder a Swagger
Dirígete a _https://localhost:{puerto}/swagger_ para ver la documentación generada por Swagger.

---

## Pruebas Unitarias

Para ejecutar las pruebas unitarias:

### Navegar al Proyecto de Pruebas
``` bash
cd PokedexAPI.Tests
```
### Ejecutar las Pruebas
``` bash
dotnet test
```

---

## Instrucciones de Uso

### Autenticación
- Usa el endpoint _/api/auth/login_ para iniciar sesión y obtener un token JWT.
- Incluye el token en el encabezado de autorización para las solicitudes protegidas:
``` http
Authorization: Bearer {token}
```

### Consultar Pokémon
- Buscar un Pokémon por ID: _/api/pokemon/{id}_
- Buscar un Pokémon por Nombre: _/api/pokemon/name/{name}_

---

## Logs de Auditoría

Los logs de auditoría se almacenan en la base de datos y registran todas las acciones de los usuarios, como consultas y modificaciones en los datos de los Pokémon o usuarios.

También se generan logs en archivos de texto que se pueden encontrar en la carpeta _logs/_ dentro del proyecto, configurados a través de log4net.
