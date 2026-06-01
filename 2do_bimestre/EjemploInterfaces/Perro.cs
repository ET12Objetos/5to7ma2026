namespace EjemploInterfaces;

public class Perro : IAnimal
{
    public string Nombre { get; set; } = default!;

    public string HacerSonido()
    {
        return "Guau";
    }
}