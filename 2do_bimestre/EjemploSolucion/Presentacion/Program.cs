using Aplicacion;

List<Alumno> alumnos = new List<Alumno>
{
    new Alumno { Nombre = "Juan", Apellido = "Pérez", Edad = 20 },
    new Alumno { Nombre = "María", Apellido = "Gómez", Edad = 22 },
    new Alumno { Nombre = "Carlos", Apellido = "López", Edad = 19 }
};

foreach (var alumno in alumnos)
{
    Console.WriteLine($"Nombre: {alumno.Nombre} {alumno.Apellido}, Edad: {alumno.Edad}");
}