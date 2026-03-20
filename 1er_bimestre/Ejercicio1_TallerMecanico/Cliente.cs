namespace Ejercicio1_TallerMecanico
{
    public class Cliente
    {
        public string Nombre { get; set; }
        public Auto Vehiculo { get; set; }

        public Cliente(string nombre, Auto vehiculo)
        {
            Nombre = nombre;
            Vehiculo = vehiculo;
            Console.WriteLine($"Hola, soy {Nombre}, cliente y dueño del auto {Vehiculo.Patente}.");
        }

        public void EntregarVehiculo(Recepcionista recepcionista)
        {
            Console.WriteLine($"[{Nombre}]: Vengo a dejar mi {Vehiculo.Modelo} para que lo revisen.");
            recepcionista.RecibirVehiculo(this);
        }

        public void Notificar(string mensaje)
        {
            Console.WriteLine($"[{Nombre}]: Recibí una notificación: '{mensaje}'");
        }
    }
}
