namespace Ejemplo3_PedidoCafe;

// Entidad: Cliente
public class Cliente
{
    private string _nombre;

    public Cliente(string nombre)
    {
        _nombre = nombre;
    }

    // Inicia la interacción enviando un mensaje al Mozo
    public void HacerPedido(Camarero camarero, string deseo)
    {
        Console.WriteLine($"[{_nombre}] Hola Camarero, quisiera pedir {deseo}, por favor.");
        camarero.TomarPedido(this, deseo); // Pasa 'this' para que el mozo sepa a quién responder
    }

    // Mensaje que recibe el Cliente cuando el pedido está listo
    public void RecibirCafe(Cafe cafe)
    {
        Console.WriteLine($"[{_nombre}] ¡Muchas gracias! Disfrutando de {cafe}.");
    }
}