using Biblioteca;

namespace Tests;

public class CalculadoraTests
{
    [Fact]
    public void Test1()
    {
        //Arrange
        Calculadora calculadora = new Calculadora();

        //Act
        int resultado = calculadora.Sumar(4, 5);

        //Assert
        Assert.Equal(9, resultado);
    }

    [Fact]
    public void Test2()
    {
        //Arrange
        Calculadora calculadora = new Calculadora();

        //Act
        int resultado = calculadora.Sumar(4, 5);

        //Assert
        Assert.NotEqual(10, resultado);
    }

    [Fact]
    public void Test3()
    {
        Calculadora calculadora = new Calculadora();

        int resultado = calculadora.Dividir(6, 3);

        Assert.Equal(2, resultado);
    }

    [Fact]
    public void Test4()
    {
        Calculadora calculadora = new Calculadora();

        Assert.Throws<DivideByZeroException>(() => calculadora.Dividir(6, 0));
    }
}
