# CAEF
Sistema de Control de Actas de Evaluación Final desarrollado por alumnos de Ingeniería en Computación en la Universidad Autónoma de Baja California

## Tabla de contenido

- [Construido Con](#construido-con)
    - [Tecnologías](#tecnologías)
    - [Extensiones y Herramientas](#extensiones-y-herramientas)
- [Documentación](#documentación)
    - [Desarrollo](#desarrollo)
        - [Requerimientos](#requerimientos)
        - [Configuración](#configuración)
        - [Proveedor DB](#proveedor-de-base-de-datos)
        - [Creación DB](#creación-de-base-de-datos)
- [Por Hacer](#por-hacer)
- [Licencia](#licencia)
- [Enlaces](#enlaces)

## Construido con
### Tecnologías
<p align="justify">El sistema fue dessarrollado en Visual Studio 2015, utilizando tecnologías tales como:</p>

1. [**ASP.NET Core**](https://www.asp.net/core): <div align="justify">Framework para el desarrollo de aplicaciones web con funcionalidades transversales como infraestructura, cacheado, _logging_, autenticación, configuración, globalización...</div>
2. [**ASP.NET Identity**](https://www.asp.net/identity): <div align="justify">Framework de autorización e identidades con soporte de perfiles, control de persistencia, integración con OAuth y OWIN, entre otros...</div>
3. [**Entity Framework Core**](https://docs.microsoft.com/en-us/ef/core/): <div align="justify">Framework de mapeo objeto-relacional (ORM) que permite trabajar con una base de datos utilizando objetos .NET.</div>
4. [**Bootstrap**](http://getbootstrap.com/): <div align="justify">Framework basado en HTML, CSS y JS para el desarrollo de sitios web adaptables.</div>
5. [**AngularJS**](https://angularjs.org/): <div align="justify">Framework JavaScript de desarrollo de aplicaciones web en el lado cliente que utiliza el patrón MVC principalmente.</div>
6. [**iTextSharp**](https://www.nuget.org/packages/iTextSharp/): <div align="justify">Librería que permite la generación y mantenimiento de documentos en formato Portable Document Format (PDF) con .NET.</div>
### Extensiones y herramientas
Con las siguientes extensiones:
- [**Web Essentials**](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.WebEssentials20135): <div align="justify">Agrega muchas características útiles a Visual Studio para el desarrollo web.</div>
- [**Web Compiler**](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.WebCompiler): <div align="justify">Permite compilar archivos LESS, Scss, Stylus, JSX y CoffeeScript directamente desde Visual Studio.</div>
- [**Add New File**](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.AddNewFile): <div align="justify">Manera rápida y fácil de crear archivos dentro del proyecto.</div>
- [**Open Command Line**](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.OpenCommandLine): <div align="justify">Abre línea de comandos en la raíz del proyecto.</div>
- [**TypeScript**](https://marketplace.visualstudio.com/items?itemName=TypeScriptTeam.TypeScript22forVisualStudio2015): <div align="justify">Súper conjunto de Javascript que aporta herramientas avanzadas e implementa muchos de los mecanismos más habituales de la programación orientada a objetos.</div>

## Documentación
### Desarrollo
#### Requerimientos
- [Visual Studio 2015 w/ Update 3](https://my.visualstudio.com/Downloads?pid=2086)
- [.NET Core SDK (1.0/1.1)](https://go.microsoft.com/fwlink/?linkid=843448)
- [.NET Core tools Preview 2 for Visual Studio 2015](https://go.microsoft.com/fwlink/?LinkID=827546)

#### Configuración
Toda la configuración de componentes e inyección de dependencias se realizan en la clase [Startup](https://github.com/RamonLM/CAEF/blob/master/src/CAEF/Startup.cs) ([más información](http://www.variablenotfound.com/2015/02/la-clase-startup-en-aspnet-5.html)).

#### Proveedor de base de datos
El proyecto fue creado utilizando Microsoft SQL Server pero funciona con **cualquier proveedor**. Las líneas de conexión se encuentran dentro del archivo [config.json](https://github.com/RamonLM/CAEF/blob/master/src/CAEF/config.json) y pueden ser cambiadas como se requiera.

#### Creación de base de datos
Se implementó Entity Framework Core, el cual ofrece tres diferentes enfoques para la creación de modelos de entidades:
- Code First
- Database First
- Model First

En este sistema se utilizó el enfoque de _Code First_ para hacer más fáciles las mgiraciones de base de datos entre sistemas.

Para la creación de base de datos (tablas, columnas, etc.) Entity Framework interpreta contextos de base de datos ya definidos para generar las base de datos en base a los [modelos](https://github.com/RamonLM/CAEF/tree/master/src/CAEF/Models/Entities) declarados. Estos contextos se encuentran en la carpeta [Contexts](https://github.com/RamonLM/CAEF/tree/master/src/CAEF/Models/Contexts) ([más sobre DbContext](http://www.w3ii.com/es/entity_framework/entity_framework_dbcontext.html)).

En el proyecto ya hay migraciones definidas y, por lo tanto, sólo es necesario aplicarlas a la base de datos. Esto se logra abriendo una línea de comandos (cmd) dentro de la raíz del proyecto y ejecutar los siguientes comandos:

```
dotnet ef database update -c CAEF.Models.Contexts.CAEFContext
dotnet ef database update -c CAEF.Models.Contexts.UsuarioFIADContext
dotnet ef database update -c CAEF.Models.Contexts.UsuarioUABCContext
```

Donde ``-c`` denota el contexto a cual aplicarle la actualización, en el caso de este sistema hay tres contextos diferentes para cada base de datos.

## Por hacer
- [X] ~~SQL schemas~~
- [X] ~~EF7 Database First Migration~~
- [X] ~~Controladores~~
- [X] ~~Vistas~~
- [X] Operaciones CRUD
- [X] ~~Autenticación~~
- [X] Servicios

## Licencia
No disponible por el momento.

## Enlaces
* [Sitio oficial](https://www.facebook.com/LeyvusSoftwareDevelopment/)
* Documentation (En progreso)
* [Issues](https://github.com/RamonLM/CAEF/issues)
* [Código fuente](https://github.com/RamonLM/CAEF)
