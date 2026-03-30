namespace CargaBateriaCelular;

public class Celular
{
    //escenario 1
    // private int porcentajeActual;

    //escenario 2
    //el atributo tiene un getter publico y un setter privado
    public int porcentajeActual { get; private set; }

    public Celular(int porcentajeBateria)
    {
        //carga inicial al momento de instanciar el objeto
        // if (porcentajeBateria >= 0 && porcentajeBateria <= 100)
        //     this.porcentajeBateria = porcentajeBateria;
        // else
        //     Console.WriteLine("Porcentaje de bateria incorrecto");

        //programacion defensiva
        if (porcentajeBateria < 0 || porcentajeBateria > 100)
            Console.WriteLine("Porcentaje de bateria incorrecto");
        else
            this.porcentajeActual = porcentajeBateria;
    }

    public void CargarBateria(int porcentaje)
    {
        if (porcentaje < 0 || porcentaje > 100)
            Console.WriteLine("Porcentaje de bateria incorrecto");
        else
            if (porcentaje > 100 - porcentajeActual)
                Console.WriteLine("No es posible realizar la carga");
            else
                porcentajeActual = 100 - porcentaje;
    }

    public void ConsumirBateria(int porcentaje)
    {
        if (porcentaje < 0 || porcentaje > 100)
            Console.WriteLine("Porcentaje de bateria incorrecto");
        else
            if (porcentaje > porcentajeActual)
                Console.WriteLine("No es posible realizar la accion");
            else
                porcentajeActual = porcentajeActual - porcentaje;
    }

    //escenario 1
    public void InforarPorcentajeBateria()
    {
        Console.WriteLine($"El porcentaje de la bateria es: {porcentajeActual}%");
    }
}