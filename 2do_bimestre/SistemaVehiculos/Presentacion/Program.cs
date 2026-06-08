using Aplicacion.Entidades;

Console.WriteLine("Bienvenido al sistema de gestión de vehículos");
Console.WriteLine("Creando vehículos...");
var automovil = new Automovil("Toyota", "Corolla", "ABC123");
var moto = new Moto("Honda", "CB500", "XYZ789");
var camion = new Camion("Volvo", "FH16", "CAM456");
automovil.TransportarPasajeros();
moto.TransportarPasajeros();
camion.Cargar();