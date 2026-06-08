using Aplicacion.Intefaces;

namespace Aplicacion.Entidades;

public class Camion : Vehiculo, ITransporteCarga
{
    public Camion(string marca, string modelo, string patente)
        : base(marca, modelo, patente)
    {
    }

    // Ejemplo de capacidad máxima de carga en kg
    public int MaximoCarga { get; } = 10000;

    public void Cargar()
    {
        Console.WriteLine("El camión está cargando mercancía.");
    }

    public override int Consumo()
    {
        //8 litros/100km es un consumo promedio para un camión
        return 8;
    }
}