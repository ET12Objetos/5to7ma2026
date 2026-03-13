# Ejemplo1 — Introducción a Clases y Objetos en C#

Proyecto de consola en **C# / .NET 10** que ilustra los conceptos básicos de la **Programación Orientada a Objetos (POO)**: definición de una clase, atributos, constructor, métodos y creación de objetos.

---

## Estructura del proyecto

```
Ejemplo1/
├── Ejemplo1.csproj   # Configuración del proyecto .NET
├── Lata.cs           # Definición de la clase Lata
└── Program.cs        # Punto de entrada, lógica principal
```

---

## Descripción de los archivos

### `Lata.cs`

Define la clase `Lata` dentro del namespace `Ejemplo1`.

| Elemento | Tipo | Descripción |
|---|---|---|
| `Color` | `string` | Color de la lata |
| `Cantidad` | `int` | Volumen/cantidad de contenido |
| `Sabor` | `string` | Sabor del producto |
| `Lata(Color, Cantidad, Sabor)` | Constructor | Inicializa los tres atributos |
| `InformarColor()` | Método | Imprime el color de la lata en consola |

```csharp
public class Lata
{
    public string Color;
    public int Cantidad;
    public string Sabor;

    public Lata(string Color, int Cantidad, string Sabor)
    {
        this.Color = Color;
        this.Cantidad = Cantidad;
        this.Sabor = Sabor;
    }

    public void InformarColor()
    {
        Console.WriteLine(Color);
    }
}
```

### `Program.cs`

Es el punto de entrada del programa. Realiza los siguientes pasos:

1. **Pide datos al usuario** por consola: color, volumen y sabor de la lata.
2. **Declara** una variable de tipo `Lata`.
3. **Instancia** un objeto `Lata` usando el constructor con los datos ingresados.
4. **Envía el mensaje** `InformarColor()` al objeto, que imprime el color en pantalla.

```csharp
Console.Write("Ingrese el color de la lata:");
string color = Console.ReadLine();

Console.Write("Ingrese el volumen de la lata:");
int volumen = int.Parse(Console.ReadLine());

Console.Write("Ingrese el sabor de la lata:");
string sabor = Console.ReadLine();

Lata unaLata;
unaLata = new Lata(Color: color, Cantidad: volumen, Sabor: sabor);

unaLata.InformarColor();
```

---

## Conceptos POO aplicados

- **Clase**: `Lata` es el molde o plantilla que define la estructura.
- **Atributos**: `Color`, `Cantidad` y `Sabor` son las propiedades del objeto.
- **Constructor**: método especial que se ejecuta al crear el objeto con `new`.
- **Objeto**: `unaLata` es la instancia concreta de la clase `Lata`.
- **Mensaje / Método**: `InformarColor()` es el comportamiento que el objeto puede ejecutar.

---

## Cómo ejecutar

```bash
cd Ejemplo1
dotnet run
```

**Ejemplo de ejecución:**

```
Ingrese el color de la lata: Rojo
Ingrese el volumen de la lata: 350
Ingrese el sabor de la lata: Cola
Rojo
```

---

## Requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
