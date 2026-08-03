# Turnos App

Aplicacion de consola en C# para solicitar turnos usando capas:

- `Aplicacion`: reglas de aplicacion e interfaces de repositorio. Incluye la carpeta `Entidades`, donde `Cliente` y `Profesional` heredan de `Persona`, y `Persona`, `Servicio` y `Turno` heredan de `Entidad`.
- `Persistencia`: repositorios Dapper contra MySQL.
- `Presentacion`: consola interactiva.

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
