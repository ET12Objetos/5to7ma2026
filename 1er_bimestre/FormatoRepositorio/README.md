# FormatoRepositorio

Proyecto de ejemplo en **.NET 10 con C#** que demuestra cómo conectarse a una base de datos **MariaDB/MySQL** usando el paquete `MySql.Data`, ejecutar consultas directas y llamar stored procedures.

---

## Requisitos

| Herramienta | Version minima |
|---|---|
| .NET SDK | 10.0 |
| MariaDB / MySQL | 12.x / 8.x |
| MySql.Data (NuGet) | 9.7.0 |

---

## Estructura de carpetas

```
FormatoRepositorio/
├── .gitignore          # Reglas para ignorar artefactos de build, IDE y credenciales
├── README.md           # Este archivo
├── scripts/
│   └── script1.sql     # Script SQL para crear la BD, tabla y stored procedures
└── src/
    └── Proyecto/
        ├── Proyecto.csproj   # Definicion del proyecto .NET (dependencias, target framework)
        └── Program.cs        # Codigo fuente principal con los tres ejemplos
```

> Las carpetas `bin/` y `obj/` son generadas por el compilador y estan excluidas por el `.gitignore`.

---

## Configuracion de la base de datos

Ejecuta el script `scripts/script1.sql` en tu servidor MariaDB/MySQL. El script realiza lo siguiente:

1. **Crea la tabla** `persona` con columnas `Id`, `Nombre` y `Dni`.
2. **Inserta datos de prueba** (5 registros).
3. **Crea el stored procedure** `crear_persona(unNombre, unDni)` — inserta una nueva persona.
4. **Crea el stored procedure** `listar_personas()` — retorna todas las personas ordenadas por id.

```sql
-- Tabla
CREATE TABLE `persona` (
  `Id`     int(11)     NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) NOT NULL,
  `Dni`    int(11)     NOT NULL,
  PRIMARY KEY (`Id`)
);

-- Stored procedure: insertar
CREATE PROCEDURE `crear_persona`(in unNombre varchar(50), in unDni int)
BEGIN
  INSERT INTO persona (nombre, dni) VALUES (unNombre, unDni);
END;

-- Stored procedure: listar
CREATE PROCEDURE `listar_personas`()
BEGIN
  SELECT p.id, p.nombre, p.dni FROM persona p ORDER BY p.id;
END;
```

---

## Configuracion del proyecto (.csproj)

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="MySql.Data" Version="9.7.0" />
  </ItemGroup>
</Project>
```

---

## Cadena de conexion

En `Program.cs` la cadena de conexion esta definida al inicio:

```csharp
string connectionString = "server=localhost;database=ejemplo;uid=root;pwd=pass123;";
```

Modifica los valores de `server`, `database`, `uid` y `pwd` segun tu entorno antes de ejecutar.

---

## Ejemplos de codigo fuente

### Clase Persona

```csharp
class Persona
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int Dni { get; set; }

    public Persona(int id, string nombre, int dni)
    {
        Id = id;
        Nombre = nombre;
        Dni = dni;
    }

    public override string ToString() => $"Id: {Id} | Nombre: {Nombre} | DNI: {Dni}";
}
```

---

### Ejemplo 1 — Consulta directa a la tabla

Lee todos los registros de la tabla `persona` con un `SELECT` directo y los convierte en objetos `Persona`.

```csharp
using (MySqlConnection conn = new MySqlConnection(connectionString))
{
    conn.Open();

    string query = "SELECT id, nombre, dni FROM persona ORDER BY id";
    MySqlCommand cmd = new MySqlCommand(query, conn);

    using MySqlDataReader reader = cmd.ExecuteReader();
    while (reader.Read())
    {
        Persona p = new Persona(
            reader.GetInt32("id"),
            reader.GetString("nombre"),
            reader.GetInt32("dni")
        );
        Console.WriteLine(p);
    }
}
```

---

### Ejemplo 2 — Stored procedure `crear_persona`

Llama al SP `crear_persona` pasando parametros por nombre. Usa `ExecuteNonQuery` porque no retorna filas.

```csharp
using (MySqlConnection conn = new MySqlConnection(connectionString))
{
    conn.Open();

    MySqlCommand cmd = new MySqlCommand("crear_persona", conn);
    cmd.CommandType = System.Data.CommandType.StoredProcedure;

    cmd.Parameters.AddWithValue("unNombre", "carlos");
    cmd.Parameters.AddWithValue("unDni", 9999);

    int filasAfectadas = cmd.ExecuteNonQuery();
    Console.WriteLine($"Persona creada. Filas afectadas: {filasAfectadas}");
}
```

---

### Ejemplo 3 — Stored procedure `listar_personas`

Llama al SP `listar_personas`, recorre el reader y construye una `List<Persona>`.

```csharp
using (MySqlConnection conn = new MySqlConnection(connectionString))
{
    conn.Open();

    MySqlCommand cmd = new MySqlCommand("listar_personas", conn);
    cmd.CommandType = System.Data.CommandType.StoredProcedure;

    List<Persona> personas = new List<Persona>();

    using MySqlDataReader reader = cmd.ExecuteReader();
    while (reader.Read())
    {
        personas.Add(new Persona(
            reader.GetInt32("id"),
            reader.GetString("nombre"),
            reader.GetInt32("dni")
        ));
    }

    foreach (Persona p in personas)
        Console.WriteLine(p);
}
```

---

## Ejecucion

```bash
cd src/Proyecto
dotnet run
```

Salida esperada:

```
=== Ejemplo 1: SELECT directo a tabla persona ===
Id: 1 | Nombre: juan    | DNI: 20
Id: 2 | Nombre: jose    | DNI: 12
Id: 3 | Nombre: maria   | DNI: 1000
Id: 4 | Nombre: valeria | DNI: 2000
Id: 5 | Nombre: marta   | DNI: 3000

=== Ejemplo 2: SP crear_persona ===
Persona creada. Filas afectadas: 1

=== Ejemplo 3: SP listar_personas ===
Id: 1 | Nombre: juan    | DNI: 20
...
Id: 6 | Nombre: carlos  | DNI: 9999
```

---

## .gitignore

El `.gitignore` incluido esta basado en la plantilla oficial de Visual Studio para .NET e ignora entre otras cosas:

- Artefactos de compilacion: `bin/`, `obj/`
- Archivos de usuario de IDE: `*.suo`, `*.user`, `.vs/`
- Paquetes NuGet descargados: `packages/`
- Resultados de tests: `TestResults/`
- Variables de entorno sensibles: `*.env`
