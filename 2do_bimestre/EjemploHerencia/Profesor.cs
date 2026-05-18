using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploHerencia
{
    public class Profesor : Persona
    {
        public int Dni { get; set; }

        public Profesor(string Nombre, string Email, int Dni)
            : base(Nombre, Email)
        {
            this.Dni = Dni;
        }
    }
}