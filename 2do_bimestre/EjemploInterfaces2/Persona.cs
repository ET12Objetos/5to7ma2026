using System.Collections;

namespace EjemploInterfaces2;

public class Persona : IEnumerable<Persona>
{
    public int Dni { get; set; }
    public string Nombre { get; set; } = default!;

    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator<Persona> IEnumerable<Persona>.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}