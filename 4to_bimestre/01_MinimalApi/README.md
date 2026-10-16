# 01 - Minimal API con ASP.NET Core

Ejemplo de una **Web API** usando **Minimal APIs** de .NET 10, con documentación OpenAPI y **Swagger UI** para probar los endpoints desde el navegador.

## Requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download) instalado.
- Para verificar la versión instalada:

```bash
dotnet --version
```

---

## 1. Crear el proyecto

Desde la carpeta donde queremos crear el proyecto:

```bash
dotnet new webapi -n 01_MinimalApi
cd 01_MinimalApi
```

- `webapi` es la plantilla de Web API. Por defecto (desde .NET 8) genera una **Minimal API**, es decir, sin controladores: los endpoints se definen directamente en `Program.cs`.
- `-n` indica el nombre del proyecto (y de la carpeta).

> Si quisiéramos usar controladores, se agrega la opción `--use-controllers`. En este ejemplo **no** la usamos.

Estructura generada:

```
01_MinimalApi/
├── 01_MinimalApi.csproj        → configuración del proyecto y paquetes NuGet
├── 01_MinimalApi.http          → archivo para probar requests desde VS Code / Visual Studio
├── Program.cs                  → punto de entrada: configuración y endpoints
├── appsettings.json            → configuración de la aplicación
├── appsettings.Development.json
└── Properties/
    └── launchSettings.json     → puertos y perfiles de ejecución
```

---

## 2. Ejecutar el proyecto

```bash
dotnet run
```

En la consola se muestra la URL donde quedó escuchando la API, por ejemplo:

```
Now listening on: http://localhost:5264
```

Para usar el perfil **https** (definido en `Properties/launchSettings.json`):

```bash
dotnet run --launch-profile https
```

| Perfil  | URL                                                  |
|---------|------------------------------------------------------|
| `http`  | http://localhost:5264                                |
| `https` | https://localhost:7191 y http://localhost:5264       |

> Si el navegador marca el certificado HTTPS como no confiable, ejecutar una sola vez: `dotnet dev-certs https --trust`

Para detener la aplicación: `Ctrl + C`.

**Tip:** con `dotnet watch` la aplicación se reinicia sola cada vez que guardamos un cambio:

```bash
dotnet watch
```

---

## 3. Instalar y configurar Swagger

Desde .NET 9, la plantilla ya **no incluye Swagger UI**. Trae el paquete `Microsoft.AspNetCore.OpenApi`, que **genera el documento OpenAPI** (un JSON que describe la API), pero no una interfaz visual para probarla. Para eso agregamos Swagger UI.

### 3.1 Instalar el paquete

```bash
dotnet add package Swashbuckle.AspNetCore.SwaggerUI
```

Después de instalarlo, el `.csproj` queda así:

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="10.0.0" />
  <PackageReference Include="Swashbuckle.AspNetCore.SwaggerUI" Version="10.3.0" />
</ItemGroup>
```

### 3.2 Configurarlo en `Program.cs`

```csharp
var builder = WebApplication.CreateBuilder(args);

// Registra el servicio que genera el documento OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Expone el documento OpenAPI en /openapi/v1.json
    app.MapOpenApi();

    // Habilita la interfaz de Swagger UI usando ese documento
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("/openapi/v1.json", "v1"));
}
```

- `AddOpenApi()` → agrega el servicio que arma la documentación de la API.
- `MapOpenApi()` → publica el JSON en `/openapi/v1.json`.
- `UseSwaggerUI(...)` → publica la página de Swagger y le indica de dónde leer el JSON.
- Todo está dentro de `IsDevelopment()` para que Swagger **solo** esté disponible en desarrollo y no en producción.

### 3.3 Abrir Swagger

Con la aplicación corriendo, entrar a:

```
http://localhost:5264/swagger
```

Ahí aparecen todos los endpoints. Para probar uno: desplegarlo → **Try it out** → **Execute**.

> Opcional: para que el navegador abra Swagger automáticamente al ejecutar, en `launchSettings.json` poner `"launchBrowser": true` y agregar `"launchUrl": "swagger"` en el perfil.

---

## 4. Endpoint GET de ejemplo

### 4.1 El modelo

`Alumno.cs`:

```csharp
namespace _01_MinimalApi
{
    public class Alumno
    {
        public string Nombre { get; set; }

        public int Edad { get; set; }
    }
}
```

### 4.2 El endpoint

En `Program.cs`, después de `builder.Build()` y antes de `app.Run()`:

```csharp
List<Alumno> alumnos = new List<Alumno>();

alumnos.Add(new Alumno { Nombre = "Jose", Edad = 20 });
alumnos.Add(new Alumno { Nombre = "Juan", Edad = 30 });

app.MapGet("/alumno", () =>
{
    return Results.Ok(alumnos);
});
```

- `app.MapGet(ruta, función)` → asocia una petición **GET** a la ruta `/alumno` con la función que se ejecuta.
- `Results.Ok(alumnos)` → devuelve código **200 OK** y la lista serializada automáticamente a **JSON**.
- La lista está en memoria: los datos se pierden al reiniciar la aplicación.

### 4.3 Cómo se compone un endpoint

Un endpoint en Minimal API tiene siempre la misma forma:

```
app.MapGet( "/alumno" , () => { return Results.Ok(alumnos); } );
│   │       │           └──────────────── handler ────────────┘
│   │       └── ruta
│   └── método HTTP
└── la aplicación
```

| Parte | En el ejemplo | Qué es |
|-------|---------------|--------|
| **Aplicación** | `app` | El objeto `WebApplication` que se obtiene con `builder.Build()`. Sobre él se registran todos los endpoints. |
| **Método HTTP** | `MapGet` | Indica a qué verbo HTTP responde el endpoint. |
| **Ruta** | `"/alumno"` | La URL a la que hay que hacer la petición: `http://localhost:5264/alumno`. |
| **Handler** | `() => { ... }` | La función (lambda) que se ejecuta cuando llega la petición. |
| **Respuesta** | `Results.Ok(alumnos)` | Lo que devuelve el handler: el código de estado HTTP y el contenido. |

#### Método HTTP

Cada verbo tiene su propio método `Map...`:

| Método | Verbo HTTP | Uso habitual |
|--------|------------|--------------|
| `app.MapGet`    | GET    | Obtener datos |
| `app.MapPost`   | POST   | Crear un dato nuevo |
| `app.MapPut`    | PUT    | Modificar un dato existente |
| `app.MapDelete` | DELETE | Eliminar un dato |

Una misma ruta puede tener varios endpoints si usan verbos distintos (por ejemplo `GET /alumno` y `POST /alumno`).

#### Handler

Es una función común de C#, escrita como **lambda**: `(parámetros) => { cuerpo }`.

- Los **paréntesis** reciben los parámetros. En el ejemplo están vacíos porque no se necesita ningún dato de la petición.
- Las **llaves** contienen el código que se ejecuta.
- Puede usar variables declaradas fuera de ella, como la lista `alumnos`.

Si el cuerpo es una sola instrucción, se puede escribir en una línea sin llaves ni `return`:

```csharp
app.MapGet("/alumno", () => Results.Ok(alumnos));
```

#### Respuesta

La clase `Results` tiene métodos para devolver cada código de estado HTTP:

| Método | Código | Cuándo se usa |
|--------|--------|---------------|
| `Results.Ok(dato)`         | 200 OK          | La petición salió bien (se puede enviar un dato) |
| `Results.Created(ruta, dato)` | 201 Created  | Se creó un dato nuevo |
| `Results.NoContent()`      | 204 No Content  | Salió bien pero no hay nada para devolver |
| `Results.BadRequest()`     | 400 Bad Request | Los datos enviados son inválidos |
| `Results.NotFound()`       | 404 Not Found   | No se encontró lo pedido |

El dato que se pasa (en este caso la lista) se convierte automáticamente a **JSON**.

### 4.4 Probarlo

- **Swagger:** `http://localhost:5264/swagger` → `GET /alumno` → Try it out → Execute.
- **Navegador:** `http://localhost:5264/alumno`
- **Archivo `.http`** (VS Code con la extensión REST Client, o Visual Studio):

```http
GET http://localhost:5264/alumno
Accept: application/json
```

Respuesta esperada:

```json
[
  { "nombre": "Jose", "edad": 20 },
  { "nombre": "Juan", "edad": 30 }
]
```

> Las propiedades se devuelven en *camelCase* (`nombre`, `edad`) porque es la convención por defecto de ASP.NET Core para JSON.

---

## `Program.cs` completo

```csharp
using _01_MinimalApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
     options.SwaggerEndpoint("/openapi/v1.json", "v1"));
}

List<Alumno> alumnos = new List<Alumno>();

alumnos.Add(new Alumno { Nombre = "Jose", Edad = 20 });
alumnos.Add(new Alumno { Nombre = "Juan", Edad = 30 });

app.MapGet("/alumno", () =>
{
    return Results.Ok(alumnos);
});

app.UseHttpsRedirection();

app.Run();
```
