using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploHerencia
{
    public abstract class Persona
    {
        public string Nombre { get; protected set; }
        public string Email { get; protected set; }

        public Persona(string Nombre, string Email)
        {
            this.Nombre = Nombre;
            this.Email = Email;
        }

        public void InformarNombre() => Console.WriteLine(Nombre);
    }
}