namespace Ejemplo3_PedidoCafe;

// Clase que representa el producto final
public class Cafe
{
    public string Tipo { get; set; }

    public Cafe(string tipo)
    {
        Tipo = tipo;
    }

    public override string ToString()
    {
        return $"un delicioso {Tipo}";
    }
}