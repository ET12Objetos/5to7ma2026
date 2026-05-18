using EjemploHerencia;

Profesor profesor = new Profesor("Jose", "jose@mail.com", 1234);     //bien

Alumno alumno = new Alumno("Juan", "juan@mail.com", new DateOnly(2023, 2, 4));   //bien

Persona[] personas = { alumno, profesor };

Console.WriteLine("METODO CONCRETO:");
alumno.InformarNombre();
profesor.InformarNombre();

Console.WriteLine();
Console.WriteLine("METODO ABSTRACTO:");
foreach (Persona persona in personas)
{
	persona.ImprimirNombre();
}

Console.WriteLine();
Console.WriteLine("METODO VIRTUAL:");
foreach (Persona persona in personas)
{
	persona.ImprimirEmail();
}

//Persona persona = new Persona();    //mal