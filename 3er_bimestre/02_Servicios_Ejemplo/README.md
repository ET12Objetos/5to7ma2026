# Usuarios App

Aplicacion de consola en C# para gestionar usuarios, roles y permisos por rol con una arquitectura por capas.

## Arquitectura

La solucion esta organizada en tres proyectos:

- `Aplicacion`: contiene el modelo de dominio, los contratos de repositorio y los servicios de aplicacion.
- `Persistencia`: implementa los repositorios con Dapper y MySQL.
- `Presentacion`: contiene la consola interactiva y arma las dependencias.

## Proyecto Aplicacion

Contiene las entidades, las interfaces que definen las operaciones de persistencia y los servicios que implementan los casos de uso.

Estructura principal:

- `Aplicacion.Entidades`
  - `Entidad`: clase base con `Id`.
  - `Persona`: clase base para entidades con `Nombre`.
  - `Usuario`: hereda de `Persona` y representa una cuenta del sistema.
  - `Rol`: hereda de `Entidad` y agrupa permisos.
  - `Permiso`: hereda de `Entidad` y representa una accion habilitada.
  - `RolPermiso`: representa la relacion entre roles y permisos.
- `Aplicacion.Repositorios`
  - `IUsuarioRepositorio`
  - `IRolRepositorio`
  - `IPermisoRepositorio`
  - `IRolPermisoRepositorio`
- `Aplicacion.Servicios`
  - `IRolServicio` y `RolServicio`
  - `IPermisoServicio` y `PermisoServicio`
  - `IRolPermisoServicio` y `RolPermisoServicio`

Los servicios contienen las validaciones y los casos de uso de la aplicacion. Para acceder a los datos dependen de las interfaces de repositorio, no de las implementaciones de `Persistencia`.

## Proyecto Persistencia

Implementa las interfaces de `Aplicacion.Repositorios`.

Estructura principal:

- `Persistencia`
  - `IDbConnectionFactory`
  - `MySqlConnectionFactory`
- `Persistencia.Repositorios`
  - `UsuarioRepositorio`
  - `RolRepositorio`
  - `PermisoRepositorio`
  - `RolPermisoRepositorio`

Los repositorios usan Dapper de forma sincronica, sin `async`/`await`.

## Proyecto Presentacion

Contiene la entrada de la aplicacion y el menu de consola:

- `Program.cs`: lee la cadena de conexion, crea `MySqlConnectionFactory`, instancia los repositorios, construye los servicios y finalmente crea `ConsoleMenu`.
- `ConsoleMenu.cs`: muestra las opciones y ejecuta las operaciones por medio de los servicios. Durante la migracion, las operaciones de usuario todavia usan `IUsuarioRepositorio` directamente.

## Funcionalidades

- Listar y registrar usuarios.
- Cambiar el rol asignado a un usuario.
- Activar o desactivar usuarios.
- Listar y registrar roles.
- Listar y registrar permisos.
- Asignar permisos a roles.
- Quitar permisos a roles.
- Consultar los permisos de un rol.

## Dependencias

```text
Presentacion -> Persistencia -> Aplicacion
Presentacion -> Aplicacion
```

`Aplicacion` no depende de ningun otro proyecto. `Persistencia` depende de `Aplicacion` para implementar los contratos. `Presentacion` depende de ambos para crear los repositorios, inyectarlos en los servicios y usar esos servicios desde la consola.

## Base de datos

Crear la base y las tablas en MySQL:

```bash
mysql -u root -p < database.sql
```

Editar la cadena de conexion en `Presentacion/appsettings.json` si tu usuario, clave o host son distintos.

## Ejecutar

```bash
dotnet run --project Presentacion/Presentacion.csproj
```

## Compilar

```bash
dotnet build UsuariosApp.slnx
```

## Cambios necesarios para crear los servicios

Un servicio actua como intermediario entre `Presentacion` y los repositorios. Su responsabilidad es representar un caso de uso, validar los datos recibidos y delegar la persistencia en una interfaz de repositorio.

Para incorporar un servicio se deben realizar los siguientes cambios:

1. Crear el contrato y la implementacion en `Aplicacion/Servicios`. Por convencion, se pueden ubicar ambos en un archivo con el nombre del servicio, por ejemplo `UsuarioServicio.cs`.
2. Declarar en la interfaz del servicio solo las operaciones que necesita la presentacion.
3. Inyectar en el constructor del servicio las interfaces de repositorio necesarias.
4. Agregar en cada metodo las validaciones y reglas de negocio antes de llamar al repositorio.
5. En `Presentacion/Program.cs`, construir primero el repositorio y luego el servicio que depende de el.
6. Cambiar el constructor y los campos de `ConsoleMenu` para recibir interfaces de servicio en lugar de interfaces de repositorio.
7. Reemplazar dentro del menu las llamadas directas al repositorio por llamadas al servicio.

Ejemplo de la estructura que falta para usuarios:

```csharp
using Aplicacion.Entidades;
using Aplicacion.Repositorios;

namespace Aplicacion.Servicios;

public interface IUsuarioServicio
{
    List<Usuario> ObtenerTodos();
    int Crear(Usuario usuario);
    bool ActualizarRol(int usuarioId, int rolId);
    bool CambiarEstado(int usuarioId, bool activo);
}

public class UsuarioServicio : IUsuarioServicio
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;

    public UsuarioServicio(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
    }

    public List<Usuario> ObtenerTodos()
    {
        return _usuarioRepositorio.ObtenerTodos().ToList();
    }

    public int Crear(Usuario usuario)
    {
        ArgumentNullException.ThrowIfNull(usuario);

        if (string.IsNullOrWhiteSpace(usuario.NombreUsuario))
        {
            throw new ArgumentException("El nombre de usuario es obligatorio.");
        }

        return _usuarioRepositorio.Crear(usuario);
    }

    public bool ActualizarRol(int usuarioId, int rolId)
    {
        if (usuarioId <= 0 || rolId <= 0)
        {
            throw new ArgumentException("Los Id deben ser mayores a cero.");
        }

        return _usuarioRepositorio.ActualizarRol(usuarioId, rolId);
    }

    public bool CambiarEstado(int usuarioId, bool activo)
    {
        if (usuarioId <= 0)
        {
            throw new ArgumentException("El Id del usuario debe ser mayor a cero.");
        }

        return _usuarioRepositorio.CambiarEstado(usuarioId, activo);
    }
}
```

El armado manual de esa dependencia en `Program.cs` queda de esta forma:

```csharp
var usuarioRepositorio = new UsuarioRepositorio(connectionFactory);
var usuarioServicio = new UsuarioServicio(usuarioRepositorio);

var menu = new ConsoleMenu(
    usuarioServicio,
    rolServicio,
    permisoServicio,
    rolPermisoServicio);
```

En `ConsoleMenu` tambien se reemplaza el campo y el parametro correspondientes:

```csharp
private readonly IUsuarioServicio _usuarioServicio;

public ConsoleMenu(
    IUsuarioServicio usuarioServicio,
    IRolServicio rolServicio,
    IPermisoServicio permisoServicio,
    IRolPermisoServicio rolPermisoServicio)
{
    _usuarioServicio = usuarioServicio;
    // Asignar los demas servicios...
}
```

Despues de este cambio, metodos como `ListarUsuarios`, `RegistrarUsuario`, `CambiarRolUsuario` y `CambiarEstadoUsuario` deben usar `_usuarioServicio`.

### Servicios que todavia se deben completar

- Crear `IUsuarioServicio` y `UsuarioServicio`, registrarlos en `Program.cs` y dejar de inyectar `IUsuarioRepositorio` en `ConsoleMenu`.
- Agregar `Crear(Permiso permiso)` a `IPermisoServicio` y `PermisoServicio` para implementar nuevamente `RegistrarPermiso`.
- Agregar `AsignarPermiso(int rolId, int permisoId)` y `QuitarPermiso(int rolId, int permisoId)` a `IRolPermisoServicio` y `RolPermisoServicio` para implementar nuevamente las opciones correspondientes del menu.
- Completar las validaciones de `RolServicio.Crear` y agregar las validaciones necesarias a los otros servicios.

No es necesario modificar `Aplicacion.csproj` al agregar archivos `.cs`: el SDK de .NET los incluye automaticamente. Tampoco se debe mover SQL a los servicios; las consultas y Dapper siguen perteneciendo a `Persistencia`.
