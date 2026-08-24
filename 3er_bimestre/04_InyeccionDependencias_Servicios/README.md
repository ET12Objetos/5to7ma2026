# Cambios realizados en `Program.cs`

`Presentacion/Program.cs` dejó de crear manualmente los repositorios, servicios y el menú. Ahora utiliza un contenedor de inyección de dependencias para registrar las implementaciones y construir automáticamente sus dependencias.

## 1. Nuevos espacios de nombres

Se agregaron los espacios de nombres necesarios para acceder a las interfaces de los repositorios y al contenedor de inyección de dependencias:

```csharp
using Aplicacion.Repositorios;
using Microsoft.Extensions.DependencyInjection;
```

`Aplicacion.Repositorios` permite registrar cada repositorio mediante su interfaz. `Microsoft.Extensions.DependencyInjection` proporciona `ServiceCollection`, los métodos de registro y la resolución de servicios.

## 2. Creación del contenedor

Después de cargar la configuración y obtener la cadena de conexión, se crea una colección de servicios:

```csharp
var services = new ServiceCollection();
```

Esta colección contiene la información que el contenedor necesita para saber qué clase debe crear cuando se solicita una interfaz.

## 3. Registro de la fábrica de conexiones

La fábrica de conexiones se registra mediante su interfaz:

```csharp
services.AddSingleton<IDbConnectionFactory>(
    _ => new MySqlConnectionFactory(connectionString));
```

Se utiliza `AddSingleton` porque una única instancia de la fábrica puede compartirse durante toda la ejecución. La fábrica conserva la cadena de conexión y crea una conexión nueva cada vez que un repositorio la solicita.

## 4. Registro de los repositorios

Cada interfaz de repositorio se relaciona con su implementación concreta:

```csharp
services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
services.AddTransient<IRolRepositorio, RolRepositorio>();
services.AddTransient<IPermisoRepositorio, PermisoRepositorio>();
services.AddTransient<IRolPermisoRepositorio, RolPermisoRepositorio>();
```

Por ejemplo, cuando una clase solicita `IUsuarioRepositorio`, el contenedor crea un `UsuarioRepositorio` y le proporciona automáticamente el `IDbConnectionFactory` registrado.

Se utiliza `AddTransient` porque estos repositorios no conservan estado y pueden crearse cuando son requeridos.

## 5. Registro de los servicios

Los servicios de la capa de aplicación también se registran mediante sus interfaces:

```csharp
services.AddTransient<IUsuarioServicio, UsuarioServicio>();
services.AddTransient<IRolServicio, RolServicio>();
services.AddTransient<IPermisoServicio, PermisoServicio>();
services.AddTransient<IRolPermisoServicio, RolPermisoServicio>();
```

Al crear un servicio, el contenedor inspecciona su constructor y le inyecta el repositorio correspondiente. Por ejemplo, `UsuarioServicio` recibe automáticamente una implementación de `IUsuarioRepositorio`.

## 6. Registro y resolución del menú

El menú también forma parte del grafo de dependencias:

```csharp
services.AddTransient<ConsoleMenu>();
```

Después de completar los registros, se construye el proveedor de servicios y se solicita el menú:

```csharp
using var serviceProvider = services.BuildServiceProvider();
var menu = serviceProvider.GetRequiredService<ConsoleMenu>();

menu.Ejecutar();
```

`GetRequiredService<ConsoleMenu>()` crea el menú e inyecta sus cuatro servicios. Para construirlos, el contenedor crea a su vez los repositorios necesarios y les entrega la fábrica de conexiones.

La declaración `using` asegura que el proveedor y las dependencias administradas por él sean liberados al terminar la aplicación.

## 7. Código eliminado

Ya no es necesario crear manualmente cada nivel de dependencias:

```csharp
var connectionFactory = new MySqlConnectionFactory(connectionString);
var usuarioRepositorio = new UsuarioRepositorio(connectionFactory);
var usuarioServicio = new UsuarioServicio(usuarioRepositorio);
```

Tampoco se construye `ConsoleMenu` pasando cada servicio de forma manual. Esa responsabilidad ahora pertenece al contenedor.

## Flujo resultante

Cuando se solicita `ConsoleMenu`, el contenedor completa automáticamente el siguiente flujo:

```text
ConsoleMenu
    -> Servicios
        -> Repositorios
            -> IDbConnectionFactory
```

De esta manera, `Program.cs` funciona como raíz de composición: carga la configuración, registra las relaciones entre interfaces e implementaciones, construye el contenedor y resuelve el objeto principal de la aplicación.
