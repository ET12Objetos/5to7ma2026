namespace Ejercicio1_TallerMecanico;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- Inicio de la simulación del Taller Mecánico ---");
        Console.WriteLine();

        // 1. Se crean los actores y el objeto principal.
        var mecanico = new Mecanico();
        var recepcionista = new Recepcionista(mecanico);
        var auto = new Auto("Ford Fiesta", "AB123CD", "el motor hace un ruido extraño al arrancar");
        var cliente = new Cliente("Ana García", auto);

        Console.WriteLine();

        // 2. El cliente inicia la interacción entregando su vehículo.
        cliente.EntregarVehiculo(recepcionista);

        Console.WriteLine();
        Console.WriteLine("--- Fin de la simulación del Taller Mecánico ---");
        Console.WriteLine("El programa ha finalizado. El cliente ha sido notificado.");
    }
}
