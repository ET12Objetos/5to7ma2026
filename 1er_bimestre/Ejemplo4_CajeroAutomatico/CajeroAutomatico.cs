using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ejemplo4_CajeroAutomatico
{
    public class CajeroAutomatico
    {
        // El Cajero TIENE una referencia a una Cuenta (Composición/Asociación)
        private CuentaBancaria _cuentaVinculada;

        public CajeroAutomatico(CuentaBancaria cuenta)
        {
            _cuentaVinculada = cuenta; // Conectamos el cajero a la cuenta
        }

        // Responsabilidad: Gestionar la interacción y el hardware
        public void ProcesarPeticion(decimal monto)
        {
            Console.WriteLine($"[Cajero] Consultando autorización por ${monto}...");

            // DELEGACIÓN: El cajero le pasa la pelota a la cuenta
            bool esValido = _cuentaVinculada.AutorizarRetiro(monto);

            if (esValido)
            {
                this.EntregarBilletes(monto);
            }
            else
            {
                Console.WriteLine("[Cajero] ERROR: Fondos insuficientes.");
            }
        }

        private void EntregarBilletes(decimal monto)
        {
            Console.WriteLine($"[Cajero] Despachando ${monto} en efectivo. ¡Retire su dinero!");
        }
    }
}