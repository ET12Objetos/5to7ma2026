namespace Ejemplo1_CalculoSueldos.Dominio;

// Una única clase guarda los datos de TODOS los tipos de empleado.
// El tipo se indica con la propiedad Tipo y, según su valor, se usan
// unas propiedades u otras al calcular el sueldo (ver Program.cs).
public class Empleado
{
    public string Nombre { get; set; } = string.Empty;
    public TipoEmpleado Tipo { get; set; }

    // Asalariado
    public decimal SueldoMensual { get; set; }

    // Por hora
    public int HorasTrabajadas { get; set; }
    public decimal TarifaHora { get; set; }

    // Por comisión
    public decimal SueldoBase { get; set; }
    public decimal Ventas { get; set; }
    public decimal PorcentajeComision { get; set; }
}
