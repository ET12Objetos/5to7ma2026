namespace Biblioteca;

/// <summary>
/// Operaciones simples con números y cadenas de texto.
/// </summary>
public class Operaciones
{
    public bool EsPar(int numero)
    {
        return numero % 2 == 0;
    }

    public double CalcularPromedio(double primerNumero, double segundoNumero)
    {
        return (primerNumero + segundoNumero) / 2;
    }

    public double CalcularPorcentaje(double numero, double porcentaje)
    {
        return numero * porcentaje / 100;
    }

    public string Concatenar(string primerTexto, string segundoTexto)
    {
        ArgumentNullException.ThrowIfNull(primerTexto);
        ArgumentNullException.ThrowIfNull(segundoTexto);

        return primerTexto + segundoTexto;
    }

    public int ContarCaracteres(string texto)
    {
        ArgumentNullException.ThrowIfNull(texto);

        return texto.Length;
    }
}
