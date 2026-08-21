# Lectura de configuración en una aplicación de consola .NET

Este proyecto muestra cómo cargar un archivo `appsettings.json` desde una
aplicación de consola y obtener una cadena de conexión mediante las extensiones
de configuración de .NET.

El ejemplo usa `ConfigurationBuilder` de forma manual: registra el archivo JSON
como fuente de configuración, construye un objeto `IConfiguration` y lee la
entrada `ConnectionStrings:DefaultConnection`.

## Requisitos

- SDK de .NET 10.
- Paquete NuGet `Microsoft.Extensions.Hosting` 10.0.0.

La referencia utilizada por el proyecto es:

```xml
<PackageReference Include="Microsoft.Extensions.Hosting" Version="10.0.0" />
```

## Estructura relevante

```text
.
|-- Program.cs
|-- appsettings.json
`-- InyeccionDepencias.csproj
```

## Configuración

El archivo `appsettings.json` contiene una sección `ConnectionStrings` y una
cadena llamada `DefaultConnection`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MiBaseDeDatos;User Id=usuario;Password=clave;TrustServerCertificate=True;"
  }
}
```

`GetConnectionString("DefaultConnection")` es una forma abreviada de consultar
la clave `ConnectionStrings:DefaultConnection`.

Para que el archivo JSON esté disponible al ejecutar la aplicación, el
`.csproj` lo copia al directorio de salida:

```xml
<ItemGroup>
  <None Update="appsettings.json">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

Con `PreserveNewest`, el archivo se vuelve a copiar solamente cuando el original
es más reciente que la copia que ya existe en el directorio de salida.

## Funcionamiento de Program.cs

El programa realiza los siguientes pasos:

1. Crea un `ConfigurationBuilder`.
2. Usa `AppContext.BaseDirectory` como ruta base. De este modo, busca la
   configuración junto a los archivos compilados y no en el directorio desde el
   que se invoque el comando.
3. Agrega `appsettings.json` como fuente obligatoria.
4. Construye la configuración.
5. Obtiene y muestra la cadena `DefaultConnection`.

El código fuente completo es:

```csharp
using Microsoft.Extensions.Configuration;

ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

configurationBuilder.SetBasePath(AppContext.BaseDirectory);

configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration configuration = configurationBuilder.Build();

string? connectionString = configuration.GetConnectionString("DefaultConnection");

Console.WriteLine(connectionString);
```

### Versión reducida con ConfigurationBuilder

El mismo flujo puede escribirse de forma más compacta encadenando los métodos
del `ConfigurationBuilder`. Así no es necesario guardar el constructor en una
variable intermedia:

```csharp
using Microsoft.Extensions.Configuration;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

string? connectionString = configuration.GetConnectionString("DefaultConnection");

Console.WriteLine(connectionString);
```

Esta versión produce el mismo resultado que el código anterior. La diferencia
es únicamente la sintaxis: cada método devuelve el objeto necesario para poder
continuar la configuración en la siguiente línea.

Los argumentos de `AddJsonFile` indican que:

- `optional: false`: la aplicación produce un error si no encuentra el archivo.
- `reloadOnChange: true`: el proveedor puede recargar la configuración cuando
  cambia el JSON mientras el proceso permanece en ejecución. En este ejemplo el
  programa termina inmediatamente después de imprimir el valor.

El resultado se guarda en un `string?` porque `GetConnectionString` devuelve
`null` cuando no existe la sección o la entrada solicitada.

## Ejecución

Desde la carpeta que contiene `InyeccionDepencias.csproj`:

```powershell
dotnet restore
dotnet run
```

La consola mostrará la cadena configurada en `DefaultConnection`:

```text
Server=localhost;Database=MiBaseDeDatos;User Id=usuario;Password=clave;TrustServerCertificate=True;
```

## Problemas frecuentes

- Si no se encuentra `appsettings.json`, comprobar que existe en la raíz del
  proyecto y que el `.csproj` incluye `CopyToOutputDirectory`.
- Si no se imprime ningún valor, comprobar que `DefaultConnection` se encuentra
  dentro de `ConnectionStrings` y que el nombre coincide exactamente con el
  usado en `Program.cs`.

## Seguridad

La cadena incluida en el repositorio contiene valores de ejemplo. Las
credenciales reales no deben almacenarse en `appsettings.json` ni confirmarse
en el control de versiones. Para datos sensibles conviene usar variables de
entorno, Secret Manager durante el desarrollo o el servicio de secretos de la
plataforma de despliegue.
