Implementación de Sistema de Reportes con ASP.NET Core MVC

Este proyecto ha sido migrado de Razor Pages a una arquitectura **Modelo-Vista-Controlador (MVC)** bien estructurada, mejorando la separación de responsabilidades y la mantenibilidad del código.

## Estructura del Proyecto

- **Controllers:** Manejan la lógica de las peticiones (Home, Account, Reportes).
- **Views:** Contienen la interfaz de usuario organizada por controlador.
- **Models:** Definen las entidades de la base de datos (Animal, Reporte, Usuario, etc.).
- **ViewModels:** Modelos específicos para las vistas (ej. LoginViewModel), asegurando que las vistas reciban solo los datos necesarios.
- **Repositories:** Implementan el acceso a datos (ADO.NET), permitiendo cambiar la fuente de datos fácilmente.
- **Data:** Contiene el `ApplicationDbContext` para Entity Framework Core.

## Características

### 1. Autenticación Basada en Cookies
Implementamos un sistema de autenticación liviano. La configuración se encuentra en `Program.cs` y el manejo en `AccountController`.

### 2. Acceso a Datos Mixto
- **Entity Framework Core:** Utilizado para la autenticación y gestión de usuarios.
- **ADO.NET:** Utilizado a través del repositorio de reportes para un control granular sobre las consultas.

### 3. Seguridad
Las rutas sensibles están protegidas mediante el atributo `[Authorize]` en los controladores correspondientes (ej. `ReportesController`).

## Usuarios de prueba
- **Email:** `admin@animales.com` | **Password:** `admin123`
- **Email:** `juan@gmail.com` | **Password:** `juan123`

## Instrucciones
1. Asegúrate de ejecutar el script `script_bd.sql` en tu servidor SQL para crear la base de datos `ControlAnimalesDB`.
2. Configura la cadena de conexión en `appsettings.json` o usa la por defecto en `Program.cs`.
3. Ejecuta el proyecto.
4. Navega por el menú. Las secciones de reportes requerirán que inicies sesión.

