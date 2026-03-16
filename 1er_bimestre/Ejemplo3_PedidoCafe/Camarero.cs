namespace Ejemplo3_PedidoCafe;

//Entidad: Camarero
public class Camarero
{
    private Cafetera _cafetera;

    // El Mozo necesita conocer la Cafetera que usará
    public Camarero(Cafetera cafetera)
    {
        _cafetera = cafetera;
    }

    // Mensaje que recibe el Mozo del Cliente: "tomar pedido"
    public void TomarPedido(Cliente cliente, string pedido)
    {
        Console.WriteLine($"[Camarero] Entendido, un {pedido} para el cliente. Pasando pedido a la cocina.");

        // El Mozo colabora enviando un mensaje a la Cafetera
        Cafe cafePreparado = _cafetera.Preparar(pedido);

        Console.WriteLine($"[Camarero] Recibido el café de la cocina. Sirviendo al cliente.");

        // El Mozo devuelve el mensaje al Cliente: "entregar cafe"
        cliente.RecibirCafe(cafePreparado);
    }
}