class NotificadorSms : INotificador
{
    public void Enviar(string usuario, string mensaje)
    {
        Console.WriteLine($"SMS para {usuario}: {mensaje}");
        Console.WriteLine("Se envia usando la red telefonica.");
    }
}
