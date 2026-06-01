# EjemploInterfaces

Proyecto de consola en C# creado como ejemplo didactico para practicar el uso de interfaces, clases e implementacion de contratos.

## Naturaleza del proyecto

Este proyecto muestra como una interfaz permite definir un comportamiento comun que distintas clases deben cumplir. En lugar de depender directamente de una clase concreta, el codigo puede trabajar con el contrato definido por la interfaz.

El ejemplo usa animales para representar este concepto de manera simple:

- `IAnimal` define las caracteristicas basicas de un animal.
- `IVolador` define el comportamiento de un objeto que puede volar.
- `Perro` implementa la interfaz `IAnimal`.
- `Gato` implementa las interfaces `IAnimal` e `IVolador`.

## Estructura del proyecto

```text
EjemploInterfaces/
+-- EjemploInterfaces.csproj
+-- Program.cs
+-- IAnimal.cs
+-- IVolador.cs
+-- Perro.cs
+-- Gato.cs
```

## Archivos principales

### `IAnimal.cs`

Define la interfaz `IAnimal`, que funciona como contrato para cualquier clase que represente un animal.

Incluye:

- La propiedad `Nombre`.
- El metodo `HacerSonido()`, que debe devolver el sonido caracteristico del animal.

### `IVolador.cs`

Define la interfaz `IVolador`, pensada para clases que tengan la capacidad de volar.

Incluye:

- El metodo `Volar()`.

### `Perro.cs`

Define la clase `Perro`, que implementa `IAnimal`.

Incluye la propiedad `Nombre` implementada como propiedad publica de lectura y escritura.

Su metodo `HacerSonido()` devuelve:

```csharp
"Guau"
```

### `Gato.cs`

Define la clase `Gato`, que implementa dos interfaces:

- `IAnimal`
- `IVolador`

Su metodo `HacerSonido()` devuelve:

```csharp
"Miau"
```

Su metodo `Volar()` devuelve:

```csharp
"Wiiii..."
```

Al igual que en `Perro`, la propiedad `Nombre` esta declarada pero no implementada.

### `Program.cs`

Es el punto de entrada de la aplicacion. Actualmente contiene ejemplos de uso de las clases e interfaces del proyecto.

El archivo muestra distintas formas de crear objetos de la clase `Perro`:

```csharp
Perro perro1 = new Perro();
Perro perro2 = new();
var perro3 = new Perro();
```

Tambien muestra como una variable puede declararse usando una interfaz y recibir una instancia de una clase que la implemente:

```csharp
IAnimal perro4 = new Perro();
IAnimal gato4 = new Gato();
```

Esto demuestra polimorfismo: tanto `Perro` como `Gato` pueden tratarse como `IAnimal` porque ambos implementan esa interfaz.

El programa tambien incluye ejemplos comentados que explican restricciones importantes:

- No se puede llamar a `Volar()` desde una variable de tipo `IAnimal`, aunque el objeto real sea un `Gato`, porque `Volar()` no pertenece al contrato de `IAnimal`.
- No se puede asignar un `Perro` a una variable de tipo `IVolador`, porque `Perro` no implementa esa interfaz.

Finalmente, se crea una referencia de tipo `IVolador` apuntando a un `Gato`:

```csharp
IVolador gato5 = new Gato();
gato5.Volar();
```

Esta llamada es valida porque `Gato` implementa `IVolador`.

## Conceptos que se practican

- Creacion de interfaces.
- Implementacion de interfaces en clases.
- Uso de contratos para definir comportamientos comunes.
- Implementacion multiple de interfaces.
- Separacion entre definicion de comportamiento e implementacion concreta.
- Polimorfismo mediante variables declaradas con tipos de interfaz.
- Diferencia entre el tipo declarado de una variable y el tipo real del objeto.

## Como ejecutar el proyecto

Desde la carpeta del proyecto, ejecutar:

```bash
dotnet run
```

La salida actual sera:

```text
No se muestra texto en consola.
```

Aunque se llama al metodo `Volar()`, su valor de retorno no se imprime con `Console.WriteLine`.

## Posibles mejoras

- Implementar correctamente la propiedad `Nombre` en `Gato`.
- Mostrar en consola el nombre y el sonido de cada animal.
- Usar variables de tipo `IAnimal` para demostrar polimorfismo.
- Imprimir el resultado de `gato5.Volar()` usando `Console.WriteLine`.
- Crear mas clases que implementen `IAnimal` o `IVolador`.

## Objetivo educativo

El objetivo principal del proyecto es comprender como las interfaces ayudan a organizar el codigo, definir obligaciones para las clases y permitir que diferentes objetos compartan comportamientos sin depender de una misma clase base.
