string usuario = "Ana";
string mensaje = "Tu pedido ya fue enviado.";

List<INotificador> notificadores =
[
    new NotificadorEmail(),
    new NotificadorSms(),
    new NotificadorPush()
];

foreach (INotificador notificador in notificadores)
{
    notificador.Enviar(usuario, mensaje);
}
