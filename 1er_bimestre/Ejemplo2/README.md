# Proyecto Ejemplo2

Este proyecto es una simple aplicación de consola en C# que gestiona una lista de alumnos y sus notas para un curso.

## Clases

### Alumno

La clase `Alumno` representa a un estudiante con las siguientes propiedades:

- `nombre`: El nombre del alumno (string).
- `nota`: La nota del alumno (int).

### Curso

La clase `Curso` representa a un curso que contiene un máximo de 5 alumnos. Tiene los siguientes métodos:

- `AgregarUn(Alumno alumno)`: Agrega un alumno al curso.
- `MejorAlumno()`: Devuelve el alumno con la nota más alta del curso.

## Funcionamiento

El programa principal (`Program.cs`) realiza las siguientes acciones:

1.  Crea una instancia de la clase `Curso`.
2.  Pide al usuario que ingrese el nombre y la nota de 5 alumnos.
3.  Agrega cada alumno al curso.
4.  Encuentra al alumno con la mejor nota utilizando el método `MejorAlumno()`.
5.  Imprime el nombre del mejor alumno en la consola.

## Cómo ejecutar el programa

1.  Clona o descarga este repositorio.
2.  Abre una terminal en el directorio `Ejemplo2`.
3.  Ejecuta el comando `dotnet run`.

## Ejemplo de salida

```
Nombre: Juan
Nota: 8
Nombre: Maria
Nota: 9
Nombre: Pedro
Nota: 7
Nombre: Ana
Nota: 10
Nombre: Luis
Nota: 6
Mejor alumno: Ana
```
