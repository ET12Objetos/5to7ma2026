# Ejemplo 2: Notificaciones sin polimorfismo

Este proyecto muestra un sistema que avisa a un usuario por distintos canales:

- Email
- SMS
- Push

El mensaje es el mismo en todos los casos. Lo que cambia es la forma en que se envia.

## Como funciona

En este ejemplo no se usa polimorfismo. El programa decide que funcion ejecutar usando un `switch`.

El metodo `EnviarNotificacion` recibe:

- `usuario`: la persona que recibe la notificacion.
- `mensaje`: el texto que se quiere enviar.
- `canal`: el tipo de envio que se va a usar.

Segun el valor de `canal`, el programa llama a una funcion distinta:

- `EnviarPorEmail`
- `EnviarPorSms`
- `EnviarPorPush`

## Idea principal

El programa pregunta explicitamente que canal se quiere usar.

```csharp
EnviarNotificacion(usuario, mensaje, "email");
EnviarNotificacion(usuario, mensaje, "sms");
EnviarNotificacion(usuario, mensaje, "push");
```

Dentro de `EnviarNotificacion`, el `switch` decide que hacer.

## Ventaja

Es facil de entender para un ejemplo pequeno.

## Desventaja

Si aparece un nuevo canal, por ejemplo WhatsApp, hay que modificar el `switch` y agregar una nueva funcion.

Esto hace que el codigo crezca dentro del mismo archivo y sea menos flexible.

## Como ejecutar

Desde la carpeta principal del repositorio:

```powershell
dotnet run --project .\Ejemplo2_Notificaciones\Ejemplo2_Notificaciones.csproj
```
