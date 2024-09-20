# Pokedex Project

Este es un proyecto de **Pokedex** que utiliza la [PokeAPI](https://pokeapi.co/) para obtener información de Pokémon. El proyecto está desarrollado con una arquitectura de software bien definida y cuenta con funcionalidades de autenticación, logs de auditoría, y pruebas unitarias. También cuenta con un **Frontend** en Angular y un **Backend** en ASP.NET Core.

## Tabla de contenidos

1. [Descripción General](#descripción-general)
2. [Diagrama ER](#diagrama-er)
3. [Diagrama de Arquitectura](#diagrama-de-arquitectura)
4. [Características](#características)
5. [Tecnologías Utilizadas](#tecnologías-utilizadas)
6. [Configuración del Proyecto](#configuración-del-proyecto)
7. [Instrucciones de Uso](#instrucciones-de-uso)


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

La aplicación cuenta con autenticación mediante JWT y un sistema de auditoría de acciones de los usuarios. El backend está implementado en ASP.NET Core como API REST y el frontend está desarrollado con Angular.

## Diagrama ER

A continuación se muestra el diagrama entidad-relación (ER) del proyecto, que define las relaciones entre usuarios, logs de auditoría y datos obtenidos de la PokeAPI.

![Diagrama ER](/DiagramaERPokedex.png)

## Diagrama de Arquitectura

El siguiente es el diagrama de la arquitectura del proyecto, mostrando cómo están estructuradas las capas de presentación, lógica de negocio y acceso a datos, así como la integración con la API externa PokeAPI.

![Diagrama de Arquitectura](/DiagramaDeArquitectura_Pokedex.png)

## Características

### Backend
- **Autenticación JWT**: El sistema utiliza JSON Web Tokens para la autenticación de usuarios.
- **Logs de Auditoría**: Todas las acciones realizadas por los usuarios se registran en la base de datos.
- **Acceso a datos con LINQ to Entities**: Las consultas a la base de datos se realizan utilizando LINQ.
- **Sistema de logs**: Implementado con **log4net** para registrar eventos importantes del sistema.
- **Documentación con Swagger**: La API está documentada automáticamente usando Swagger.

### Frontend
- **Infinite Scroll**: El listado de Pokémon se carga de manera infinita conforme el usuario se desplaza.
- **Búsqueda por ID**: Los usuarios pueden buscar Pokémon por su número.
- **Muestra Datos de Pokémon**: Se presentan los detalles completos de cada Pokémon.
- **Interfaz amigable**: Implementada en Angular.

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

### Base de Datos
- **SQL Server**: Base de datos utilizada para almacenar usuarios y logs de auditoría.

### API Externa
- **PokeAPI**: API pública utilizada para obtener la información de los Pokémon.

## Configuración del Proyecto

### Requisitos Previos

- **Node.js** y **npm** instalados para el desarrollo del frontend.
- **.NET Core SDK** para el desarrollo del backend.
- **SQL Server** para la base de datos.

