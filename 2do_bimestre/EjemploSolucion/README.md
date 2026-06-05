# EjemploSolucion

Este repositorio contiene un ejemplo simple de una solucion .NET organizada en dos proyectos:

- `Aplicacion`: biblioteca de clases donde se define la logica o los tipos del dominio. En este ejemplo contiene la clase `Alumno`.
- `Presentacion`: aplicacion de consola que actua como punto de entrada del programa. Este proyecto referencia a `Aplicacion` para poder crear y mostrar una lista de alumnos.

La idea principal del proyecto es separar responsabilidades: la capa de presentacion ejecuta el programa y muestra informacion por consola, mientras que la capa de aplicacion contiene las clases reutilizables.

## Estructura

```text
EjemploSolucion/
+-- EjemploSolucion.slnx
+-- Aplicacion/
|   +-- Aplicacion.csproj
|   +-- Alumno.cs
+-- Presentacion/
    +-- Presentacion.csproj
    +-- Program.cs
```

## Tipos de proyectos

El proyecto `Aplicacion` fue creado como una biblioteca de clases:

```bash
dotnet new classlib -n Aplicacion
```

Este tipo de proyecto no se ejecuta directamente. Sirve para definir clases, servicios, modelos o reglas que pueden ser usadas por otros proyectos.

El proyecto `Presentacion` fue creado como una aplicacion de consola:

```bash
dotnet new console -n Presentacion
```

Este tipo de proyecto genera un ejecutable. En este caso, contiene el archivo `Program.cs`, que es el punto de entrada de la aplicacion.

## Creacion del archivo de solucion

Para agrupar los proyectos bajo una misma solucion se creo un archivo `.slnx`, que es el nuevo formato XML de archivos de solucion:

```bash
dotnet new sln -n EjemploSolucion --format slnx
```

El archivo `EjemploSolucion.slnx` permite administrar varios proyectos juntos desde Visual Studio, Visual Studio Code o la linea de comandos.

A diferencia del formato clasico `.sln`, el formato `.slnx` es XML y resulta mas simple de leer. En este proyecto tiene una estructura similar a esta:

```xml
<Solution>
  <Configurations>
    <Platform Name="Any CPU" />
    <Platform Name="x64" />
    <Platform Name="x86" />
  </Configurations>
  <Project Path="Aplicacion/Aplicacion.csproj" />
  <Project Path="Presentacion/Presentacion.csproj" />
</Solution>
```

Si ya existia un archivo `.sln` clasico, se puede generar el nuevo formato con:

```bash
dotnet sln migrate EjemploSolucion.sln
```

## Agregar proyectos a la solucion

Una vez creados los proyectos, se agregaron al archivo de solucion con estos comandos:

```bash
dotnet sln EjemploSolucion.slnx add Aplicacion/Aplicacion.csproj
dotnet sln EjemploSolucion.slnx add Presentacion/Presentacion.csproj
```

Con esto, la solucion queda compuesta por los proyectos `Aplicacion` y `Presentacion`.

## Agregar dependencia entre proyectos

Como `Presentacion` necesita usar la clase `Alumno` definida en `Aplicacion`, se agrego una referencia desde el proyecto de consola hacia la biblioteca de clases:

```bash
dotnet add Presentacion/Presentacion.csproj reference Aplicacion/Aplicacion.csproj
```

Esto genera dentro de `Presentacion.csproj` una referencia como la siguiente:

```xml
<ItemGroup>
  <ProjectReference Include="..\Aplicacion\Aplicacion.csproj" />
</ItemGroup>
```

Gracias a esa referencia, el codigo de `Presentacion` puede importar el namespace de la biblioteca:

```csharp
using Aplicacion;
```

## Compilar y ejecutar

Para compilar toda la solucion:

```bash
dotnet build EjemploSolucion.slnx
```

Para ejecutar el proyecto de consola:

```bash
dotnet run --project Presentacion/Presentacion.csproj
```

El programa crea una lista de alumnos y muestra sus datos por consola.

## Comandos usados

Resumen de los comandos principales:

```bash
dotnet new sln -n EjemploSolucion --format slnx
dotnet new classlib -n Aplicacion
dotnet new console -n Presentacion
dotnet sln EjemploSolucion.slnx add Aplicacion/Aplicacion.csproj
dotnet sln EjemploSolucion.slnx add Presentacion/Presentacion.csproj
dotnet add Presentacion/Presentacion.csproj reference Aplicacion/Aplicacion.csproj
dotnet build EjemploSolucion.slnx
dotnet run --project Presentacion/Presentacion.csproj
```
