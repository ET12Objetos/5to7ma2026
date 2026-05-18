//como crear una lista
//List<int> numeros = new List<int>();

//agrega un elemento a la lista
// numeros.Add(104);
// numeros.Add(102);
// numeros.Add(100);
// numeros.Add(105);


//numeros.RemoveRange(0, 2);
//leer la lista con indice
// for (int i = 0; i < numeros.Count; i++)
// {
//     Console.WriteLine(numeros[i]);
// }

//ordena los items de la lista
//numeros.Sort();

// foreach (int numero in numeros)
// {
//     Console.WriteLine(numero);
// }

// numeros.ForEach(numero =>
// {
//     Console.WriteLine(numero);
// });

// Console.WriteLine($"Max: {numeros.Max()}");
// Console.WriteLine($"Min: {numeros.Min()}");
// Console.WriteLine($"Average: {numeros.Average()}");

using Ejemplo_Listas;

List<Alumno> alumnos = new List<Alumno>();

alumnos.Add(new Alumno(1000, "Jose"));
alumnos.Add(new Alumno(2000, "Marcos"));
alumnos.Add(new Alumno(3000, "Juan"));
alumnos.Add(new Alumno(4000, "Matias"));

// foreach (Alumno alumno in alumnos)
// {
//     Console.WriteLine(alumno.Nombre);
// }

//var resultado = alumnos.Max(x => x.Dni);

Alumno alumnoDniAlto = alumnos.OrderBy(x => x.Dni).Last();

System.Console.WriteLine(alumnoDniAlto.Nombre);
