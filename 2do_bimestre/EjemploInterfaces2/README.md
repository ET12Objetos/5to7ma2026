# EjemploInterfaces2

Proyecto de consola en C# creado para practicar el uso de clases, listas e interfaces de enumeracion.

## Descripcion

El programa crea una lista de alumnos, agrega varios objetos `Alumno` y luego recorre la coleccion con un `foreach` para mostrar los datos por consola.

La idea principal del ejemplo es observar como una coleccion como `List<T>` implementa interfaces que permiten agregar elementos, recorrerlos y trabajar con ellos de forma tipada.

## Estructura del proyecto

- `Program.cs`: contiene el codigo principal del programa. Crea una lista de alumnos, carga datos y los imprime.
- `Alumno.cs`: define la clase `Alumno`, con las propiedades `Nombre` y `Dni`.
- `Persona.cs`: define la clase `Persona`, tambien con `Nombre` y `Dni`, e implementa `IEnumerable<Persona>`. Sus metodos de enumeracion estan declarados, pero todavia no tienen implementacion.
- `EjemploInterfaces2.csproj`: archivo de configuracion del proyecto .NET.

## Codigo principal

En `Program.cs` se declara una lista:

```csharp
List<Alumno> alumnos = new List<Alumno>();
```

Luego se agregan alumnos:

```csharp
alumnos.Add(new Alumno { Nombre = "Juan", Dni = 123 });
alumnos.Add(new Alumno { Nombre = "Jose", Dni = 234 });
alumnos.Add(new Alumno { Nombre = "Pedro", Dni = 345 });
alumnos.Add(new Alumno { Nombre = "Maria", Dni = 456 });
```

Finalmente, la lista se recorre con `foreach`:

```csharp
foreach (Alumno alumno in alumnos)
    Console.WriteLine($"Alumno: {alumno.Nombre} - Dni: {alumno.Dni}");
```

## Como ejecutar

Desde la carpeta del proyecto, ejecutar:

```bash
dotnet run
```

## Salida esperada

```text
Alumno: Juan - Dni: 123
Alumno: Jose - Dni: 234
Alumno: Pedro - Dni: 345
Alumno: Maria - Dni: 456
```

## Conceptos trabajados

- Creacion de clases en C#.
- Propiedades automaticas.
- Uso de `List<T>`.
- Inicializacion de objetos.
- Recorrido de colecciones con `foreach`.
- Introduccion a interfaces como `IEnumerable<T>`, `IList<T>` e `IReadOnlyList<T>`.

## Nota

La clase `Persona` implementa `IEnumerable<Persona>`, pero sus metodos `GetEnumerator()` todavia lanzan `NotImplementedException`. Esto significa que, si se intenta recorrer directamente un objeto `Persona`, el programa fallara hasta que se complete esa implementacion.
