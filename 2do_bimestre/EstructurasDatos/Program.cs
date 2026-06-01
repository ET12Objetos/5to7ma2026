using EstructurasDatos;

// Listas
List<Alumno> alumnos = new List<Alumno>();

alumnos.Add(new Alumno { Nombre = "Juan", Dni = 123, Email = "juan@example.com", CuadroFutbol = "Boca" });
alumnos.Add(new Alumno { Nombre = "Jose", Dni = 234, Email = "jose@example.com", CuadroFutbol = "River" });
alumnos.Add(new Alumno { Nombre = "Pedro", Dni = 345, Email = "pedro@example.com", CuadroFutbol = "Independiente" });
alumnos.Add(new Alumno { Nombre = "Maria", Dni = 456, Email = "maria@example.com", CuadroFutbol = "San Lorenzo" });

// foreach (Alumno alumno in alumnos)
//     Console.WriteLine($"Alumno: {alumno.Nombre} - Dni: {alumno.Dni}");// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

//Diccionarios
Dictionary<string, Alumno> valores = new Dictionary<string, Alumno>();

valores.Add("boca", new Alumno { Nombre = "Esteban", Dni = 1000, Email = "juan@example.com", CuadroFutbol = "Boca" });
valores.Add("river", new Alumno { Nombre = "Javier", Dni = 2000, Email = "jose@example.com", CuadroFutbol = "River" });
valores.Add("independiente", new Alumno { Nombre = "Paula", Dni = 3000, Email = "pedro@example.com", CuadroFutbol = "Independiente" });

//Console.WriteLine(valores["river"].Nombre);

Dictionary<int, Alumno> alumnos2 = new Dictionary<int, Alumno>();

alumnos2 = alumnos.ToDictionary(alumno => alumno.Dni, alumno => alumno);

//Console.WriteLine(alumnos2[345].Nombre);

//Colas

Queue<Alumno> cola = new Queue<Alumno>();

cola.Enqueue(new Alumno { Nombre = "Esteban", Dni = 1000, Email = "esteban@example.com", CuadroFutbol = "Boca" });
cola.Enqueue(new Alumno { Nombre = "Javier", Dni = 2000, Email = "javier@example.com", CuadroFutbol = "River" });

cola.Dequeue(); // Elimina el primer elemento agregado (Esteban)

Console.WriteLine(cola.Peek().Nombre); // Muestra el nombre del siguiente elemento en la cola (Javier)

//Pilas

Stack<Alumno> pila = new Stack<Alumno>();

pila.Push(new Alumno { Nombre = "Esteban", Dni = 1000, Email = "esteban@example.com", CuadroFutbol = "Boca" });
pila.Push(new Alumno { Nombre = "Javier", Dni = 2000, Email = "javier@example.com", CuadroFutbol = "River" });

pila.Pop(); // Elimina el último elemento agregado (Javier)

Console.WriteLine(pila.Peek().Nombre); // Muestra el nombre del siguiente elemento en la pila (Esteban)
