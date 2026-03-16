using Ejemplo2;

string nombre;
int nota;
Curso curso = new Curso();

for (int i = 0; i < 5; i++)
{
    Console.Write("Nombre: ");
    nombre = Console.ReadLine();
    Console.Write("Nota: ");
    nota = int.Parse(Console.ReadLine());

    // alumno = new Alumno(nombre, nota);
    // curso.AgregarUn(alumno);

    curso.AgregarUn(new Alumno(nombre, nota));
}

Console.WriteLine($"Mejor alumno: {curso.MejorAlumno().nombre}");