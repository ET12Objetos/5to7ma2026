# Ejemplo 3: Simulación de Pedido en una Cafetería

Este proyecto es una simple aplicación de consola en C# que demuestra los principios de la Programación Orientada a Objetos (POO), enfocándose en la **colaboración entre objetos**.

## Propósito del Proyecto

El objetivo principal es ilustrar cómo diferentes objetos interactúan entre sí enviando "mensajes" (llamadas a métodos) para cumplir una tarea en conjunto. Se simula un escenario cotidiano: un cliente que pide un café en una cafetería.

## Clases Involucradas

El proyecto está compuesto por las siguientes clases, cada una con una responsabilidad única:

-   **`Cliente`**: Representa a la persona que quiere tomar un café. Inicia la interacción.
    -   *Mensajes que envía*: `HacerPedido(camarero, tipoCafe)`
    -   *Mensajes que recibe*: `RecibirCafe(cafe)`
-   **`Camarero`**: Actúa como intermediario entre el cliente y la cocina (la cafetera).
    -   *Mensajes que recibe*: `TomarPedido(cliente, tipoCafe)`
    -   *Mensajes que envía*: `Preparar(tipoCafe)` a la `Cafetera` y `RecibirCafe(cafe)` al `Cliente`.
-   **`Cafetera`**: Representa la máquina o la "cocina" que prepara el café.
    -   *Mensajes que recibe*: `Preparar(tipoCafe)`
    -   *Devuelve*: Un objeto `Cafe`.
-   **`Cafe`**: Es una clase simple que representa el producto final entregado al cliente.

## Flujo de la Simulación

La interacción se desarrolla en el siguiente orden:

1.  Se crean las instancias de `Cafetera`, `Camarero` y `Cliente`. Es importante destacar que el `Camarero` "conoce" a la `Cafetera` con la que debe trabajar.
2.  El `Cliente` inicia el proceso llamando al método `HacerPedido` del `Camarero`, especificando qué café desea.
3.  El `Camarero` recibe el pedido y, a su vez, le pide a la `Cafetera` que prepare el café.
4.  La `Cafetera` simula un tiempo de preparación y devuelve un objeto `Cafe` ya listo.
5.  El `Camarero` recibe el café preparado y se lo entrega al `Cliente` correcto.
6.  El `Cliente` recibe su café y la simulación finaliza.

## Cómo Ejecutar el Proyecto

1.  Abre una terminal o consola.
2.  Navega hasta la carpeta del proyecto `Ejemplo3_PedidoCafe`.
3.  Ejecuta el siguiente comando de .NET:

    ```bash
    dotnet run
    ```

Esto compilará y ejecutará la aplicación, y verás el diálogo de la simulación impreso en la consola.
