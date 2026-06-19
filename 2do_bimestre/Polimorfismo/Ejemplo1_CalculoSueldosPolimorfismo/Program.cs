// Ejemplo 1 - Cálculo de sueldos (versión CON polimorfismo).
//
// Mismo problema que Ejemplo1_CalculoSueldos, pero sin switch sobre el tipo.
// Cada subclase de Empleado sobrescribe CalcularSueldo(). El bucle llama
// siempre al mismo método y, en tiempo de ejecución, se ejecuta la versión
// que corresponde al tipo real de cada objeto (despacho dinámico).

using Ejemplo1_CalculoSueldosPolimorfismo.Dominio;

List<Empleado> empleados = new()
{
    new Asalariado("Ana López", sueldoMensual: 2500m),
    new PorHora("Bruno Díaz", horasTrabajadas: 160, tarifaHora: 12.50m),
    new PorComision("Carla Ruiz", sueldoBase: 800m, ventas: 15000m, porcentajeComision: 0.05m)
};

Console.WriteLine("=== Nómina de la empresa ===\n");

foreach (Empleado e in empleados)
{
    decimal sueldo = e.CalcularSueldo();   // <- polimorfismo en acción
    Console.WriteLine($"{e.Nombre,-15} ({e.GetType().Name,-11}): {sueldo,10:C}");
}

