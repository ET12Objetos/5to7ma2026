namespace Ejercicio1_TallerMecanico
{
    public class Auto
    {
        public string Modelo { get; set; }
        public string Patente { get; set; }
        public string Problema { get; set; }

        public Auto(string modelo, string patente, string problema)
        {
            Modelo = modelo;
            Patente = patente;
            Problema = problema;
        }

        public override string ToString()
        {
            return $"{Modelo} (Patente: {Patente}) con problema: '{Problema}'";
        }
    }
}
