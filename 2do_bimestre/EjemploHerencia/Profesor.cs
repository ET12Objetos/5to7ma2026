using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploHerencia
{
    //Clase "sealed" no permite que otra clase herede de la
    //clase Profesor
    public sealed class Profesor : Persona
    {
        public int Dni { get; set; }

        public Profesor(string Nombre, string Email, int Dni)
            : base(Nombre, Email)
        {
            this.Dni = Dni;
        }

        public override void ImprimirNombre()
        {
            Console.WriteLine($"Prof.: {Nombre}");
        }

        public override void ImprimirEmail()
        {
            Console.WriteLine($"Email del profesor {Nombre}: {Email}");
        }
    }
}