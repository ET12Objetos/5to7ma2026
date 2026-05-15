using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploHerencia
{
    public class Alumno : Persona
    {
        public DateOnly FechaNaciemiento { get; set; }

        public int Edad()
        {
            var hoy = DateOnly.FromDateTime(DateTime.Today);
            var edad = hoy.Year - FechaNaciemiento.Year;

            if (hoy < FechaNaciemiento.AddYears(edad))
            {
                edad--;
            }

            return edad;
        }
    }
}