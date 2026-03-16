using Ejemplo3_PedidoCafe;

// --- ORQUESTACIÓN (Punto de inicio) ---
Console.WriteLine("--- Inicio de la Simulación de la Cafetería ---");
Console.WriteLine();

// 1. Creamos las instancias de los objetos necesarios
Cafetera miCafetera = new Cafetera();
Camarero miCamarero = new Camarero(miCafetera); // El mozo sabe qué cafetera usar
Cliente miCliente = new Cliente("Ana");

// 2. Iniciamos la interacción: El Cliente envía el primer mensaje
miCliente.HacerPedido(miCamarero, "Latte");

Console.WriteLine();
Console.WriteLine("--- Fin de la Simulación ---");
Console.ReadKey();