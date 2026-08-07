# Usuarios App

Aplicacion de consola en C# para gestionar usuarios, roles y permisos por rol con una arquitectura por capas.

## Arquitectura

La solucion esta organizada en tres proyectos:

- `Aplicacion`: contiene el modelo de dominio y los contratos de repositorio.
- `Persistencia`: implementa los repositorios con Dapper y MySQL.
- `Presentacion`: contiene la consola interactiva y arma las dependencias.

## Proyecto Aplicacion

Contiene las entidades y las interfaces que definen las operaciones de persistencia.

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

- `Program.cs`: lee la cadena de conexion, crea `MySqlConnectionFactory`, instancia los repositorios y construye `ConsoleMenu`.
- `ConsoleMenu.cs`: muestra las opciones y ejecuta las operaciones usando directamente las interfaces de repositorio.

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

`Aplicacion` no depende de ningun otro proyecto. `Persistencia` depende de `Aplicacion` para implementar los contratos. `Presentacion` depende de ambos para crear los repositorios y usarlos desde la consola.

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
