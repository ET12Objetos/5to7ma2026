namespace Ejemplo2;

public class Alumno
{
    public string nombre;

    public int nota;

    public Alumno(string nombre, int nota)
    {
        this.nombre = nombre;
        this.nota = nota;
    }

    bool EstaAprobado(Alumno alumno)
    {
        if (alumno.nota >= 6)
            return true;
        else
            return false;
    }
}