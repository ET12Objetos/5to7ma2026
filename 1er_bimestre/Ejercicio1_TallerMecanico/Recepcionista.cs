namespace Ejercicio1_TallerMecanico
{
    public class Recepcionista
    {
        private Mecanico mecanico;

        public Recepcionista(Mecanico mecanico)
        {
            this.mecanico = mecanico;
        }

        public void RecibirVehiculo(Cliente cliente)
        {
            Console.WriteLine($"[Recepcionista]: Buen día, {cliente.Nombre}. Recibimos su {cliente.Vehiculo}.");
            Console.WriteLine("[Recepcionista]: Generando orden de trabajo...");
            
            var orden = new OrdenDeTrabajo(cliente);
            
            Console.WriteLine($"[Recepcionista]: Orden #{orden.Id} creada. Se la entrego al mecánico.");
            mecanico.RealizarReparacion(orden, this);
        }

        public void NotificarTrabajoTerminado(OrdenDeTrabajo orden)
        {
            Console.WriteLine($"[Recepcionista]: El mecánico terminó el trabajo #{orden.Id}. Notificando al cliente.");
            orden.Propietario.Notificar($"Su {orden.Vehiculo.Modelo} está listo para ser retirado.");
        }
    }
}
