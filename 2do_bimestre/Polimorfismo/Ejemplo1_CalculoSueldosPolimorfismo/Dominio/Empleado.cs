namespace Ejemplo1_CalculoSueldosPolimorfismo.Dominio;

// Clase base abstracta: define QUÉ saben hacer todos los empleados
// (calcular su sueldo), pero no CÓMO. Cada subclase lo decide en su override.
public abstract class Empleado
{
    public string Nombre { get; }

    protected Empleado(string nombre)
    {
        Nombre = nombre;
    }

    // No hay implementación aquí: cada tipo de empleado la aporta.
    public abstract decimal CalcularSueldo();
}
