using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploHerencia
{
    public class Alumno : Persona
    {
        public DateOnly FechaNacimiento { get; set; }

        public Alumno(string Nombre, string Email, DateOnly FechaNacimiento)
            : base(Nombre, Email)
        {
            this.FechaNacimiento = FechaNacimiento;
        }

        public int Edad()
        {
            var hoy = DateOnly.FromDateTime(DateTime.Today);
            var edad = hoy.Year - FechaNacimiento.Year;

            if (hoy < FechaNacimiento.AddYears(edad))
            {
                edad--;
            }

            return edad;
        }

        public override void ImprimirNombre()
        {
            Console.WriteLine($"El nombre del alumno es: {Nombre}");
        }
    }
}