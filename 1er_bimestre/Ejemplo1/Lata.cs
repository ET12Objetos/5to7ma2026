using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ejemplo1
{
    public class Lata
    {
        //atributos
        public string Color;
        public int Cantidad;
        public string Sabor;

        public Lata(string Color, int Cantidad, string Sabor)
        {
            this.Color = Color;
            this.Cantidad = Cantidad;
            this.Sabor = Sabor;
        }

        //comportamiento
        public void InformarColor()
        {
            Console.WriteLine(Color);
        }
    }
}