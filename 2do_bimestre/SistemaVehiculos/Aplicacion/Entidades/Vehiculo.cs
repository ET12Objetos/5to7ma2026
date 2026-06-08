namespace Aplicacion.Entidades;

public abstract class Vehiculo
{
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public string Patente { get; set; }

    public Vehiculo(string marca, string modelo, string patente)
    {
        Marca = marca;
        Modelo = modelo;
        Patente = patente;
    }

    //Consumo (6 a 10) litros/100km
    public abstract int Consumo();
}