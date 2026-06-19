namespace Ejemplo1_CalculoSueldos.Dominio;

// Discrimina el tipo de empleado para decidir cómo se calcula su sueldo.
public enum TipoEmpleado
{
    Asalariado,
    PorHora,
    Comision
}
