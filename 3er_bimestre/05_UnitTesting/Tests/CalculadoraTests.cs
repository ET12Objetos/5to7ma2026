using Biblioteca;

namespace Tests;

public class CalculadoraTests
{
    Calculadora calculadora;

    public CalculadoraTests()
    {
        //Inicializar mi prueba - Arrange
        calculadora = new Calculadora();
    }

    [Fact]
    //Prueba Unitaria o Unit Test
    public void Calculadora_Sumar_DebeRetornarUnValorCorrecto()
    {
        //Hacer la prueba - Act
        int resultado = calculadora.Sumar(6, 7);

        //Verificar el exito de la prueba - Assert
        Assert.Equal(13, resultado);
    }

    [Fact]
    //Prueba Unitaria o Unit Test
    public void Calculadora_Sumar_DebeRetornarUnValorIncorrecto()
    {
        //Hacer la prueba - Act
        int resultado = calculadora.Sumar(6, 7);

        //Verificar el exito de la prueba - Assert
        Assert.NotEqual(2, resultado);
    }

    [Fact]
    public void Calculadora_Dividir_DebeRetornarResultadoCorrecto()
    {
        int resultado = calculadora.Dividir(15, 3);

        Assert.Equal(5, resultado);
    }

    [Fact]
    public void Calculadora_Dividir_DebeRetornarResultadoIncorrecto()
    {
        Assert.Throws<DivideByZeroException>(() => calculadora.Dividir(3, 0));
    }

    // [Fact]
    // public void Test1()
    // {
    //     int resultado = calculadora.Multiplicar(5, 3);

    //     Assert.Equal(15, resultado);
    // }

    // [Fact]
    // public void Test2()
    // {
    //     int resultado = calculadora.Multiplicar(5, 0);

    //     Assert.Equal(0, resultado);
    // }

    // [Fact]
    // public void Test3()
    // {
    //     int resultado = calculadora.Multiplicar(5, -2);

    //     Assert.Equal(-10, resultado);
    // }

    [Theory]
    [InlineData(2, 5, 10)]
    [InlineData(2, -5, -10)]
    [InlineData(2, 0, 0)]
    [InlineData(0, 2, 0)]
    public void Calculadora_Multiplicar_CasoCorrecto(int a, int b, int resultadoEsperado)
    {
        int resultado = calculadora.Multiplicar(a, b);

        Assert.Equal(resultadoEsperado, resultado);
    }
}
