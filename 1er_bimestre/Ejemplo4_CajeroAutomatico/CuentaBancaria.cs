using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ejemplo4_CajeroAutomatico
{
    // --- OBJETO 1: LA ENTIDAD DE DATOS (CUENTA) ---
    public class CuentaBancaria
    {
        private decimal _saldo;

        public CuentaBancaria(decimal saldoInicial)
        {
            _saldo = saldoInicial;
        }

        // Responsabilidad: Validar y descontar dinero
        public bool AutorizarRetiro(decimal monto)
        {
            if (monto <= _saldo)
            {
                _saldo -= monto; // Actualiza su estado interno
                return true;     // Respuesta al mensaje: ÉXITO
            }
            return false;        // Respuesta al mensaje: FALLO
        }

        public decimal ConsultarSaldo() => _saldo;
    }
}