// See https://aka.ms/new-console-template for more information
using Ejemplo1;

Console.Write("Ingrese el color de la lata:");
string color = Console.ReadLine();
Console.Write("Ingrese el volumen de la lata:");
int volumen = int.Parse(Console.ReadLine());
Console.Write("Ingrese el sabor de la lata:");
string sabor = Console.ReadLine();


//define un objeto
Lata unaLata;

//instancia un objeto de la clase Lata
unaLata = new Lata(Color: color, Cantidad: volumen, Sabor: sabor);

//en envia un mensaje "InformarColor" al objeto unaLata
unaLata.InformarColor();