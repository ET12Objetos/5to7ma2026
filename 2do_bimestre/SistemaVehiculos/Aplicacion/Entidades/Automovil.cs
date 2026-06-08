using Aplicacion.Intefaces;

namespace Aplicacion.Entidades;

public class Automovil : Vehiculo, ITransportePasajeros, ITransporteFueraRuta
{
    public Automovil(string marca, string modelo, string patente)
        : base(marca, modelo, patente)
    {
    }

    public int MaximoPasajeros { get; } = 3;

    public override int Consumo()
    {
        //6 litros/100km es un consumo promedio para un automóvil
        return 6;
    }

    public bool TransportarFueraRuta()
    {
        return true;
    }

    public void TransportarPasajeros()
    {
        Console.WriteLine("El automóvil está transportando pasajeros.");
    }
}