using Aplicacion.Intefaces;

namespace Aplicacion.Entidades;

public class Moto : Vehiculo, ITransportePasajeros, ITransporteFueraRuta
{
    public Moto(string marca, string modelo, string patente)
        : base(marca, modelo, patente)
    {
    }

    public int MaximoPasajeros { get; } = 1;

    public override int Consumo()
    {
        //4 litros/100km es un consumo promedio para una moto
        return 4;
    }

    public bool TransportarFueraRuta()
    {
        return true;
    }

    public void TransportarPasajeros()
    {
        Console.WriteLine("La moto está transportando pasajeros.");
    }
}