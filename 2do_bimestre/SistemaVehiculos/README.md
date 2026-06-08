# SistemaVehiculos

Proyecto de consola en C#/.NET que modela un sistema simple de gestion de vehiculos. La solucion esta separada en dos proyectos: uno contiene la logica de aplicacion y las entidades del dominio, y el otro contiene el punto de entrada que ejecuta el programa.

## Requisitos

- .NET SDK compatible con `net10.0`
- Visual Studio 2026, Visual Studio Code o una terminal con el comando `dotnet`

## Como ejecutar el proyecto

Desde la carpeta raiz del proyecto:

```bash
dotnet restore
dotnet build
dotnet run --project Presentacion
```

Tambien se puede abrir el archivo `SistemaVehiculos.sln` desde Visual Studio y ejecutar el proyecto `Presentacion`.

## Estructura del proyecto

```text
SistemaVehiculos/
├── SistemaVehiculos.sln
├── Aplicacion/
│   ├── Aplicacion.csproj
│   ├── Entidades/
│   │   ├── Vehiculo.cs
│   │   ├── Automovil.cs
│   │   ├── Moto.cs
│   │   └── Camion.cs
│   └── Intefaces/
│       ├── ITransportePasajeros.cs
│       ├── ITransporteCarga.cs
│       └── ITransporteFueraRuta.cs
└── Presentacion/
    ├── Presentacion.csproj
    └── Program.cs
```

## Construccion de la solucion

La solucion esta formada por dos proyectos:

- `Aplicacion`: proyecto de biblioteca donde se define el modelo principal del sistema.
- `Presentacion`: proyecto de consola que referencia a `Aplicacion` y crea ejemplos de vehiculos para mostrar su comportamiento.

El proyecto `Presentacion` tiene una referencia directa a `Aplicacion`:

```xml
<ProjectReference Include="..\Aplicacion\Aplicacion.csproj" />
```

Gracias a esta referencia, `Program.cs` puede usar las clases `Automovil`, `Moto` y `Camion` definidas en la capa de aplicacion.

## Entidades principales

### Vehiculo

`Vehiculo` es una clase abstracta que representa las caracteristicas comunes de todos los vehiculos:

- `Marca`
- `Modelo`
- `Patente`

Tambien define el metodo abstracto `Consumo()`, que obliga a cada vehiculo concreto a indicar su consumo propio.

### Automovil

`Automovil` hereda de `Vehiculo` e implementa:

- `ITransportePasajeros`
- `ITransporteFueraRuta`

Define una cantidad maxima de pasajeros y puede transportar pasajeros. Su consumo actual es de `6` litros cada 100 km.

### Moto

`Moto` hereda de `Vehiculo` e implementa:

- `ITransportePasajeros`
- `ITransporteFueraRuta`

Define una cantidad maxima de pasajeros y puede transportar pasajeros. Su consumo actual es de `4` litros cada 100 km.

### Camion

`Camion` hereda de `Vehiculo` e implementa:

- `ITransporteCarga`

Define una capacidad maxima de carga y puede cargar mercancia. Su consumo actual es de `8` litros cada 100 km.

## Interfaces

Las interfaces representan capacidades que puede tener un vehiculo:

- `ITransportePasajeros`: indica que el vehiculo puede transportar pasajeros.
- `ITransporteCarga`: indica que el vehiculo puede transportar carga.
- `ITransporteFueraRuta`: indica que el vehiculo puede circular fuera de ruta.

Esto permite combinar comportamientos sin depender solamente de la herencia. Por ejemplo, un `Automovil` y una `Moto` pueden transportar pasajeros, mientras que un `Camion` puede transportar carga.

## Flujo de ejecucion

El archivo `Presentacion/Program.cs` funciona como punto de entrada del sistema. Actualmente realiza los siguientes pasos:

1. Muestra un mensaje de bienvenida.
2. Crea un `Automovil`, una `Moto` y un `Camion`.
3. Ejecuta metodos especificos de cada tipo de vehiculo:
   - `TransportarPasajeros()` en el automovil.
   - `TransportarPasajeros()` en la moto.
   - `Cargar()` en el camion.

## Objetivo del diseño

El proyecto muestra conceptos basicos de programacion orientada a objetos:

- Herencia mediante la clase base `Vehiculo`.
- Polimorfismo mediante el metodo abstracto `Consumo()`.
- Interfaces para representar capacidades especificas.
- Separacion entre capa de aplicacion y capa de presentacion.

