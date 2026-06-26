class NotificadorPush : INotificador
{
    public void Enviar(string usuario, string mensaje)
    {
        Console.WriteLine($"PUSH para {usuario}: {mensaje}");
        Console.WriteLine("Se envia como notificacion de la app.");
    }
}
