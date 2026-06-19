# Ejemplo 1 — Cálculo de sueldos

## Enunciado

Una empresa tiene distintos tipos de empleados:

- **Asalariados**: cobran un sueldo fijo mensual.
- **Por hora**: cobran según las horas trabajadas × la tarifa por hora.
- **Por comisión**: cobran un sueldo base + un porcentaje de las ventas realizadas.

Hay que calcular el sueldo de cada uno y mostrar la nómina completa de la
empresa con el total a pagar.

## Objetivo

Resolver el problema **sin polimorfismo**, usando una única clase `Empleado`
que guarda los datos de todos los tipos y una propiedad `Tipo` (enum
`TipoEmpleado`). El cálculo del sueldo se decide con un `switch` sobre ese
tipo:

```csharp
decimal CalcularSueldo(Empleado e)
{
    switch (e.Tipo)
    {
        case TipoEmpleado.Asalariado: return e.SueldoMensual;
        case TipoEmpleado.PorHora:    return e.HorasTrabajadas * e.TarifaHora;
        case TipoEmpleado.Comision:   return e.SueldoBase + e.Ventas * e.PorcentajeComision;
        default: throw new ArgumentException("Tipo desconocido");
    }
}
```

> Nota: esta es la versión "antes" del polimorfismo. Sirve para ver el
> problema que aparece al usar un `switch` por tipo: si mañana se agrega un
> nuevo tipo de empleado, hay que tocar este método (y todos los `switch`
> parecidos). El polimorfismo resuelve esto delegando el cálculo a cada clase.

## Requisitos

1. Definir un enum `TipoEmpleado` con los valores `Asalariado`, `PorHora` y
   `Comision`.
2. Definir una única clase `Empleado` con la propiedad `Nombre`, la propiedad
   `Tipo` y las propiedades de datos de los tres casos (sueldo mensual, horas
   y tarifa, base/ventas/comisión).
3. Cada clase/tipo debe estar en su propio archivo dentro de la carpeta
   `Dominio`.
4. En el `Program.cs`, guardar varios empleados en una lista de `Empleado`,
   recorrerla calculando el sueldo con la función `CalcularSueldo` (el `switch`)
   e ir acumulando el total de la nómina.

## Salida esperada (ejemplo)

```
=== Nómina de la empresa ===

Ana López       (Asalariado ):  $ 2.500,00
Bruno Díaz      (PorHora    ):  $ 2.000,00
Carla Ruiz      (Comision   ):  $ 1.550,00
---------------------------------------------
TOTAL NÓMINA                :   $6,050.00
```
