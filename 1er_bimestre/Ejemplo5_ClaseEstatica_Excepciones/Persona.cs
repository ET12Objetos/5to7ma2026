namespace Ejemplo5_ClaseEstatica_Excepciones;

public class Persona
{
    public string Nombre { get; set; }
    public string Direccion { get; private set; }
    public int Edad { get; set; }

    public Persona(string nombre, string direccion, int edad)
    {
        Validar.Cadena(nombre, "Nombre");
        this.Nombre = nombre;
        Validar.Cadena(direccion, "Direccion");
        this.Direccion = direccion;
        Validar.Edad(edad);
        this.Edad = edad;
    }

    public void CambiarDireccion(string direccion)
    {
        Validar.Cadena(direccion, "Direccion");
        this.Direccion = direccion;
    }
}