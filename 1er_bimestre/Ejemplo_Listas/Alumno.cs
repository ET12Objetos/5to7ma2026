namespace Ejemplo_Listas;

public class Alumno
{
    public int Dni { get; set; }
    public string Nombre { get; set; }

    public Alumno(int Dni, string Nombre)
    {
        this.Dni = Dni;
        this.Nombre = Nombre;
    }
}