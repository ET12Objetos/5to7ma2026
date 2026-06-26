# Ejemplo 2: Notificaciones con POO y polimorfismo

Este proyecto muestra el mismo sistema de notificaciones, pero usando Programacion Orientada a Objetos y polimorfismo.

El sistema avisa a un usuario por distintos canales:

- Email
- SMS
- Push

El mensaje es el mismo en todos los casos. Lo que cambia es como se envia.

## Como funciona

En este ejemplo existe una interfaz llamada `INotificador`.

```csharp
interface INotificador
{
    void Enviar(string usuario, string mensaje);
}
```

La interfaz define un comportamiento comun: todos los notificadores deben saber enviar un mensaje.

Cada canal tiene su propia clase:

- `NotificadorEmail`
- `NotificadorSms`
- `NotificadorPush`

Todas esas clases implementan `INotificador`, pero cada una tiene su propia version del metodo `Enviar`.

## Donde aparece el polimorfismo

En `Program.cs`, se crea una lista de `INotificador`:

```csharp
List<INotificador> notificadores =
[
    new NotificadorEmail(),
    new NotificadorSms(),
    new NotificadorPush()
];
```

Aunque todos se guardan como `INotificador`, cada objeto ejecuta su propio metodo `Enviar`.

```csharp
foreach (INotificador notificador in notificadores)
{
    notificador.Enviar(usuario, mensaje);
}
```

Ese es el punto central del polimorfismo: se usa el mismo metodo, pero el comportamiento cambia segun el objeto real.

## Archivos del proyecto

- `Program.cs`: crea el mensaje, arma la lista de notificadores y los recorre.
- `INotificador.cs`: define la interfaz comun.
- `NotificadorEmail.cs`: envia el mensaje como email.
- `NotificadorSms.cs`: envia el mensaje como SMS.
- `NotificadorPush.cs`: envia el mensaje como notificacion push.

## Ventaja

Si aparece un nuevo canal, por ejemplo WhatsApp, se puede crear una nueva clase `NotificadorWhatsapp` que implemente `INotificador`.

No hace falta modificar un `switch` central. Solo se agrega el nuevo objeto a la lista.

## Como ejecutar

Desde la carpeta principal del repositorio:

```powershell
dotnet run --project .\Ejemplo2_NotificacionesPolimorfismo\Ejemplo2_NotificacionesPolimorfismo.csproj
```
