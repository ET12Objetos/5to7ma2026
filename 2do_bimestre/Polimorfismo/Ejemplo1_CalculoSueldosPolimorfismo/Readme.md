# Ejemplo 1 — Cálculo de sueldos (con polimorfismo)

## Enunciado

Una empresa tiene distintos tipos de empleados:

- **Asalariados**: cobran un sueldo fijo mensual.
- **Por hora**: cobran según las horas trabajadas × la tarifa por hora.
- **Por comisión**: cobran un sueldo base + un porcentaje de las ventas realizadas.

Hay que calcular el sueldo de cada uno y mostrar la nómina completa de la
empresa con el total a pagar.

## Objetivo

Es el mismo problema que [`Ejemplo1_CalculoSueldos`](../Ejemplo1_CalculoSueldos),
pero resuelto **con polimorfismo**. En lugar de una sola clase con un `switch`
sobre el tipo, se define una clase base abstracta `Empleado` con el método
`CalcularSueldo()` y cada tipo de empleado lo **sobrescribe** (`override`) con
su propia fórmula.

El programa principal recorre una lista de `Empleado` y llama siempre al mismo
método; en tiempo de ejecución se ejecuta la versión correcta según el tipo
real de cada objeto (despacho dinámico).

### Ventaja frente al `switch`

Para agregar un nuevo tipo de empleado solo hay que crear una nueva clase que
herede de `Empleado` y sobrescriba `CalcularSueldo()`. **No** se toca el
`Program.cs` ni ningún `switch` existente.

## Estructura

```
Ejemplo1_CalculoSueldosPolimorfismo/
├── Dominio/
│   ├── Empleado.cs       ← clase base abstracta + CalcularSueldo() abstracto
│   ├── Asalariado.cs     ← override: sueldo fijo
│   ├── PorHora.cs        ← override: horas × tarifa
│   └── PorComision.cs    ← override: base + % ventas
└── Program.cs
```

## Salida esperada (ejemplo)

```
=== Nómina de la empresa ===

Ana López       (Asalariado ):  $ 2.500,00
Bruno Díaz      (PorHora    ):  $ 2.000,00
Carla Ruiz      (PorComision):  $ 1.550,00
---------------------------------------------
TOTAL NÓMINA                :  $ 6.050,00
```
