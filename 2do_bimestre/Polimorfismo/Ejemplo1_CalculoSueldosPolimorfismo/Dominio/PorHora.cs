namespace Ejemplo1_CalculoSueldosPolimorfismo.Dominio;

// Horas trabajadas × tarifa por hora.
public class PorHora : Empleado
{
    public int HorasTrabajadas { get; }
    public decimal TarifaHora { get; }

    public PorHora(string nombre, int horasTrabajadas, decimal tarifaHora) : base(nombre)
    {
        HorasTrabajadas = horasTrabajadas;
        TarifaHora = tarifaHora;
    }

    public override decimal CalcularSueldo() => HorasTrabajadas * TarifaHora;
}
