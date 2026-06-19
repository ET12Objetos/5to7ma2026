namespace Ejemplo1_CalculoSueldosPolimorfismo.Dominio;

// Sueldo fijo mensual.
public class Asalariado : Empleado
{
    public decimal SueldoMensual { get; }

    public Asalariado(string nombre, decimal sueldoMensual) : base(nombre)
    {
        SueldoMensual = sueldoMensual;
    }

    public override decimal CalcularSueldo() => SueldoMensual;
}
