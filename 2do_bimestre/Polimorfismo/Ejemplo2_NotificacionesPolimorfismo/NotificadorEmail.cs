class NotificadorEmail : INotificador
{
    public void Enviar(string usuario, string mensaje)
    {
        Console.WriteLine($"EMAIL para {usuario}: {mensaje}");
        Console.WriteLine("Se envia usando un servidor de correo.");
    }
}
