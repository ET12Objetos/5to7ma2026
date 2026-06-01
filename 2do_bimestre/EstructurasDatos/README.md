# EstructurasDatos

Proyecto de consola en C# para practicar el uso de estructuras de datos basicas con objetos de tipo `Alumno`.

## Contenido del proyecto

- `Program.cs`: contiene los ejemplos principales de uso de listas, diccionarios, colas y pilas.
- `Alumno.cs`: define la clase `Alumno`, con datos como DNI, nombre, email y cuadro de futbol.
- `EstructurasDatos.csproj`: archivo de configuracion del proyecto .NET.

## Clase `Alumno`

La clase `Alumno` representa a un estudiante y tiene las siguientes propiedades:

- `Dni`: numero de documento del alumno.
- `Nombre`: nombre del alumno.
- `Email`: correo electronico.
- `CuadroFutbol`: equipo de futbol del alumno.

## Estructuras de datos utilizadas

### Lista

Se usa `List<Alumno>` para guardar varios alumnos en orden. Permite agregar elementos y recorrerlos facilmente.

```csharp
List<Alumno> alumnos = new List<Alumno>();
```

### Diccionario

Se usan diccionarios para guardar alumnos asociados a una clave.

- `Dictionary<string, Alumno>`: usa como clave el nombre de un equipo.
- `Dictionary<int, Alumno>`: usa como clave el DNI del alumno.

Tambien se muestra como convertir una lista en diccionario usando `ToDictionary`.

```csharp
alumnos2 = alumnos.ToDictionary(alumno => alumno.Dni, alumno => alumno);
```

### Cola

Se usa `Queue<Alumno>` para trabajar con una estructura FIFO: el primer elemento en entrar es el primero en salir.

Metodos usados:

- `Enqueue`: agrega un alumno al final de la cola.
- `Dequeue`: elimina el primer alumno agregado.
- `Peek`: consulta el siguiente alumno sin eliminarlo.

### Pila

Se usa `Stack<Alumno>` para trabajar con una estructura LIFO: el ultimo elemento en entrar es el primero en salir.

Metodos usados:

- `Push`: agrega un alumno a la pila.
- `Pop`: elimina el ultimo alumno agregado.
- `Peek`: consulta el alumno que esta en la cima sin eliminarlo.

## Salida esperada

Al ejecutar el programa se muestran los nombres que quedan disponibles luego de operar con la cola y la pila:

```text
Javier
Esteban
```

## Como ejecutar

Desde la carpeta del proyecto, ejecutar:

```bash
dotnet run
```

## Objetivo

El objetivo del proyecto es comparar el comportamiento de distintas estructuras de datos en C# y entender cuando conviene usar cada una:

- Lista: coleccion ordenada de elementos.
- Diccionario: busqueda rapida por clave.
- Cola: atencion por orden de llegada.
- Pila: acceso al ultimo elemento agregado.
