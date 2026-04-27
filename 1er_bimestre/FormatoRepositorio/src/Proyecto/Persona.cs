namespace Proyecto;

public class Persona
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int Dni { get; set; }

    public Persona(int id, string nombre, int dni)
    {
        Id = id;
        Nombre = nombre;
        Dni = dni;
    }

    public override string ToString() => $"Id: {Id} | Nombre: {Nombre} | DNI: {Dni}";
}