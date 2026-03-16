namespace Ejemplo3_PedidoCafe;

// Entidad: Cafetera
public class Cafetera
{
    // Mensaje que recibe la Cafetera: "preparar"
    public Cafe Preparar(string tipoDeCafe)
    {
        Console.WriteLine($"[Cafetera] Iniciando la preparación de un {tipoDeCafe}...");
        // Simulamos un tiempo de preparación
        System.Threading.Thread.Sleep(1500);
        Console.WriteLine($"[Cafetera] ¡{tipoDeCafe} listo!");

        // La Cafetera devuelve un objeto Cafe como respuesta
        return new Cafe(tipoDeCafe);
    }
}