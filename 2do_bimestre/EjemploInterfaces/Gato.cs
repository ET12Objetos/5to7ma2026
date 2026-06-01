namespace EjemploInterfaces;

public class Gato : IVolador, IAnimal
{
    public string Nombre
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }
    public string HacerSonido()
    {
        return "Miau";
    }

    public string Volar()
    {
        return "Wiiii...";
    }
}