namespace Ejercicio1_TallerMecanico;

public class Mecanico
{
    public void RealizarReparacion(OrdenDeTrabajo orden, Recepcionista recepcionista)
    {
        Console.WriteLine($"[Mecánico]: Recibí la orden #{orden.Id} para el {orden.Vehiculo}. Manos a la obra.");
        Console.WriteLine("[Mecánico]: (Simulando reparación...)");

        orden.Completada = true;
        Console.WriteLine($"[Mecánico]: ¡Listo! El trabajo para la orden #{orden.Id} está terminado.");

        recepcionista.NotificarTrabajoTerminado(orden);
    }
}
