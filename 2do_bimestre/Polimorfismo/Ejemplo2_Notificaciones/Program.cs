string usuario = "Ana";
string mensaje = "Tu pedido ya fue enviado.";

EnviarNotificacion(usuario, mensaje, "email");
EnviarNotificacion(usuario, mensaje, "sms");
EnviarNotificacion(usuario, mensaje, "push");

static void EnviarNotificacion(string usuario, string mensaje, string canal)
{
    switch (canal)
    {
        case "email":
            EnviarPorEmail(usuario, mensaje);
            break;

        case "sms":
            EnviarPorSms(usuario, mensaje);
            break;

        case "push":
            EnviarPorPush(usuario, mensaje);
            break;

        default:
            Console.WriteLine("Canal no disponible.");
            break;
    }
}

static void EnviarPorEmail(string usuario, string mensaje)
{
    Console.WriteLine($"EMAIL para {usuario}: {mensaje}");
    Console.WriteLine("Se envia usando un servidor de correo.");
}

static void EnviarPorSms(string usuario, string mensaje)
{
    Console.WriteLine($"SMS para {usuario}: {mensaje}");
    Console.WriteLine("Se envia usando la red telefonica.");
}

static void EnviarPorPush(string usuario, string mensaje)
{
    Console.WriteLine($"PUSH para {usuario}: {mensaje}");
    Console.WriteLine("Se envia como notificacion de la app.");
}
