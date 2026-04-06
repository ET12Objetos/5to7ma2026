namespace Ejemplo5_ClaseEstatica_Excepciones;

public static class Validar
{
    public static void Cadena(string valor, string nombreCampo)
    {
        if (string.IsNullOrEmpty(valor))
            throw new ArgumentException($"El campo {nombreCampo} no puede estar vacío.");
    }

    public static void Edad(int valor)
    {
        if (valor <= 0 || valor >= 120)
            throw new ArgumentException("La edad no es valida");
    }
}