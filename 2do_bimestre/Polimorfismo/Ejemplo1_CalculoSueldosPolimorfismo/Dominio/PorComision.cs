namespace Ejemplo1_CalculoSueldosPolimorfismo.Dominio;

// Sueldo base + porcentaje de las ventas.
public class PorComision : Empleado
{
    public decimal SueldoBase { get; }
    public decimal Ventas { get; }
    public decimal PorcentajeComision { get; }

    public PorComision(string nombre, decimal sueldoBase, decimal ventas, decimal porcentajeComision)
        : base(nombre)
    {
        SueldoBase = sueldoBase;
        Ventas = ventas;
        PorcentajeComision = porcentajeComision;
    }

    public override decimal CalcularSueldo() => SueldoBase + Ventas * PorcentajeComision;
}
