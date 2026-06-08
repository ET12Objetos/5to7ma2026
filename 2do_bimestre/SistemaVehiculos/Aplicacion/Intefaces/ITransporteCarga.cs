namespace Aplicacion.Intefaces;

public interface ITransporteCarga
{
    int MaximoCarga { get; }
    void Cargar();
}