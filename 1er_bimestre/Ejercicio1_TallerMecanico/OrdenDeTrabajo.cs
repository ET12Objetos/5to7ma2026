namespace Ejercicio1_TallerMecanico
{
    public class OrdenDeTrabajo
    {
        private static int contadorId = 0;
        public int Id { get; set; }
        public Cliente Propietario { get; set; }
        public Auto Vehiculo { get; set; }
        public bool Completada { get; set; }

        public OrdenDeTrabajo(Cliente propietario)
        {
            Id = ++contadorId;
            Propietario = propietario;
            Vehiculo = propietario.Vehiculo;
            Completada = false;
        }
    }
}
