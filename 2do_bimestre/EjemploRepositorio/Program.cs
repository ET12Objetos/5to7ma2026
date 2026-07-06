using EjemploRepositorio;

List<Persona> personas = new List<Persona>();

string connectionString = "Server=localhost;Port=3306;User=root;Password=pass123;Database=ejemplo";

var personaRepository = new PersonaRepository(connectionString);

personas = personaRepository.GetPesonas();

foreach (var p in personas)
{
    Console.WriteLine($"Id: {p.Id}, Nombre: {p.Nombre}, DNI: {p.Dni}");
}