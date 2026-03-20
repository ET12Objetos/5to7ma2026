// 1. Instanciamos la Cuenta (El "Experto en Datos")
using Ejemplo4_CajeroAutomatico;

CuentaBancaria miCuenta = new CuentaBancaria(500);

// 2. Instanciamos el Cajero y le "inyectamos" la cuenta (Composición)
CajeroAutomatico elCajero = new CajeroAutomatico(miCuenta);

// 3. El Usuario interactúa con el Cajero enviando un mensaje
Console.WriteLine("--- BIENVENIDO AL BANCO ---");
elCajero.ProcesarPeticion(200); // Intento 1: Exitoso

Console.WriteLine("\n--- SEGUNDA OPERACIÓN ---");
elCajero.ProcesarPeticion(400); // Intento 2: Fallido (solo quedaban 300)

Console.WriteLine("\nPresione cualquier tecla para salir...");
Console.ReadKey();