// Ejemplo 1 - Cálculo de sueldos (versión SIN polimorfismo).
//
// Hay una sola clase Empleado y un enum TipoEmpleado. El cálculo del sueldo
// se resuelve con un switch sobre e.Tipo: según el tipo, se aplica una
// fórmula distinta usando las propiedades correspondientes.

using Ejemplo1_CalculoSueldos.Dominio;

List<Empleado> empleados = new()
{
    new Empleado { Nombre = "Ana López", Tipo = TipoEmpleado.Asalariado, SueldoMensual = 2500m },
    new Empleado { Nombre = "Bruno Díaz", Tipo = TipoEmpleado.PorHora, HorasTrabajadas = 160, TarifaHora = 12.50m },
    new Empleado { Nombre = "Carla Ruiz", Tipo = TipoEmpleado.Comision, SueldoBase = 800m, Ventas = 15000m, PorcentajeComision = 0.05m }
};

Console.WriteLine("=== Nómina de la empresa ===\n");

foreach (Empleado e in empleados)
{
    decimal sueldo = CalcularSueldo(e);
    Console.WriteLine($"{e.Nombre,-15} ({e.Tipo,-11}): {sueldo,10:C}");
}

decimal CalcularSueldo(Empleado e)
{
    switch (e.Tipo)
    {
        case TipoEmpleado.Asalariado: return e.SueldoMensual;
        case TipoEmpleado.PorHora: return e.HorasTrabajadas * e.TarifaHora;
        case TipoEmpleado.Comision: return e.SueldoBase + e.Ventas * e.PorcentajeComision;
        default: throw new ArgumentException("Tipo desconocido");
    }
}
