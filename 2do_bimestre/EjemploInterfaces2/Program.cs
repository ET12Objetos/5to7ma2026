using EjemploInterfaces2;

//implementacion mas restrictiva de métodos
//IList<Alumno> alumnos = new List<Alumno>();

//implementa interfaces IList y IReadOnly
List<Alumno> alumnos = new List<Alumno>();

alumnos.Add(new Alumno { Nombre = "Juan", Dni = 123 });
alumnos.Add(new Alumno { Nombre = "Jose", Dni = 234 });
alumnos.Add(new Alumno { Nombre = "Pedro", Dni = 345 });
alumnos.Add(new Alumno { Nombre = "Maria", Dni = 456 });

foreach (Alumno alumno in alumnos)
    Console.WriteLine($"Alumno: {alumno.Nombre} - Dni: {alumno.Dni}");