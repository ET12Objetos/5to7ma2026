using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploHerencia
{
    public class Curso
    {
        public List<Alumno> Alumnos = new List<Alumno>();
        public Profesor Profesor { get; set; }

        public void InformarAlumnos()
        {
            foreach (Alumno a in Alumnos)
            {
                Console.WriteLine($"Nombre: {a.Nombre}, Edad: {a.Edad()}");
            }
        }
    }
}