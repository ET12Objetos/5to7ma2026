# Proyecto EjemploHerencia

Este proyecto es una aplicacion de consola en C# que muestra conceptos basicos de programacion orientada a objetos, especialmente herencia, clases abstractas, metodos virtuales, sobrescritura (`override`) y clases `sealed`.

## Objetivo

El proyecto busca mostrar como varias clases pueden compartir comportamiento comun a partir de una clase base (`Persona`) y como las clases derivadas (`Alumno` y `Profesor`) pueden especializar ese comportamiento.

## Estructura del proyecto

### `Persona`

Es la clase base abstracta del modelo.

- Tiene las propiedades `Nombre` y `Email`.
- No se puede instanciar directamente porque es `abstract`.
- Incluye el metodo concreto `InformarNombre()`, que imprime el nombre.
- Define el metodo abstracto `ImprimirNombre()`, que obliga a las clases hijas a implementarlo.
- Define el metodo virtual `ImprimirEmail()`, que puede ser reutilizado o redefinido por las clases derivadas.

### `Alumno`

Hereda de `Persona`.

- Agrega la propiedad `FechaNacimiento`.
- Implementa `ImprimirNombre()` con un mensaje especifico para alumnos.
- Incluye el metodo `Edad()`, que calcula la edad a partir de la fecha de nacimiento.

### `Profesor`

Hereda de `Persona` y esta declarada como `sealed`.

- Agrega la propiedad `Dni`.
- Implementa `ImprimirNombre()` con un formato propio para profesores.
- Sobrescribe `ImprimirEmail()` para cambiar completamente el comportamiento heredado.
- Al ser `sealed`, no permite que otra clase herede de `Profesor`.

### `Batman`

Esta clase sirve como ejemplo de esa restriccion.

- El archivo deja comentado que `Batman` no puede heredar de `Profesor` porque `Profesor` es `sealed`.

### `Curso`

Representa un curso compuesto por:

- Una lista de alumnos (`List<Alumno>`).
- Un profesor asociado.

Incluye el metodo `InformarAlumnos()`, que recorre la lista e imprime el nombre y la edad de cada alumno.

### `Program`

El programa principal:

1. Crea un `Profesor`.
2. Crea un `Alumno`.
3. Ejecuta `InformarNombre()` en ambos objetos.
4. Ejecuta `ImprimirEmail()` en ambos objetos.
5. Deja comentado un ejemplo incorrecto de instanciacion de `Persona`, ya que al ser abstracta no puede crearse directamente.

## Conceptos de herencia que muestra

- Clase abstracta: `Persona`.
- Herencia: `Alumno : Persona` y `Profesor : Persona`.
- Metodo abstracto: `ImprimirNombre()`.
- Metodo virtual y sobrescritura: `ImprimirEmail()`.
- Clase sellada: `Profesor`.

## Ejecucion actual

Al ejecutar `dotnet run`, la salida observada es:

```text
Juan
Jose
juan@mail.com
Quiero vale cuatro
```

Esto muestra que:

- `InformarNombre()` usa la implementacion comun heredada desde `Persona`.
- `Alumno` usa la implementacion heredada de `ImprimirEmail()`.
- `Profesor` redefine `ImprimirEmail()` con su propia salida.

## Como ejecutar el proyecto

1. Abrir una terminal en `2do_bimestre/EjemploHerencia`.
2. Ejecutar:

```bash
dotnet run
```

## Nota

Durante la compilacion aparece una advertencia en `Curso.cs` porque la propiedad `Profesor` no se inicializa en un constructor. Esa advertencia no impide ejecutar el ejemplo actual.
