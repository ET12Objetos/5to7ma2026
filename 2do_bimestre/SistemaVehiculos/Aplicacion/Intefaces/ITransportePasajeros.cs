namespace Aplicacion.Intefaces;

public interface ITransportePasajeros
{
    int MaximoPasajeros { get; }
    void TransportarPasajeros();
}