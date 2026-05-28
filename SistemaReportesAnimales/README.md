Implementación de Login y Autenticación

Para esta entrega, pasamos el proyecto de una aplicación de consola a un entorno web usando **ASP.NET Core Razor Pages. Implementamos un sistema de autenticación basado en **Cookies**, que es liviano y no requiere toda la complejidad de Identity.

## Cambios realizados

### 1. Configuración del Proyecto (.csproj y Program.cs)
- Se cambió el SDK del proyecto a `Microsoft.NET.Sdk.Web`.
- Se agregaron los paquetes de **Entity Framework Core** para manejar la base de datos de forma más moderna.
- En `Program.cs` se configuraron los servicios de autenticación y se definió que la página de login por defecto es `/Account/Login`.

### 2. Base de Datos y Modelos
- Se actualizaron las clases de `Usuario` para incluir los campos `Email` y `Password`.
- Se creó el `ApplicationDbContext` para mapear nuestras entidades a las tablas de SQL Server.
- El script SQL (`script_bd.sql`) fue actualizado para incluir estas nuevas columnas y algunos usuarios de prueba.

### 3. Sistema de Login
- **Página de Login:** Ubicada en `Pages/Account/Login.cshtml`. El Code-Behind (`OnPostAsync`) busca al usuario en la base de datos. Si las credenciales coinciden, genera una "identidad" (Claims) y crea la cookie de sesión.
- **Logout:** Implementado en `Logout.cshtml.cs`, simplemente destruye la cookie y redirige al inicio.

### 4. Protección de Rutas (Seguridad)
Para proteger cualquier página y que solo puedan entrar usuarios logueados, usamos el decorador `[Authorize]` arriba de la clase en el archivo `.cshtml.cs`. 
Ejemplo (ver `Pages/Reportes/Listado.cshtml.cs`):

```csharp
[Authorize]
public class ListadoModel : PageModel { 
    // ... logic ...
}
```

Si un usuario intenta entrar a esa página sin estar logueado, el sistema lo redirigirá automáticamente al Login.

## Usuarios de prueba
- **Email:** `admin@animales.com` | **Password:** `admin123`
- **Email:** `juan@gmail.com` | **Password:** `juan123`

## Instrucciones
1. Asegurate de ejecutar el script `script_bd.sql` actualizado en tu servidor SQL.
2. Ejecutá el proyecto. Se abrirá el navegador en el Inicio.
3. Intentá entrar a "Ver Reportes (Protegido)". Verás que te manda al Login.
4. Ingresá con las credenciales de prueba.
5. Una vez logueado, ya podrás ver el listado y aparecerá tu nombre en la barra de navegación.
