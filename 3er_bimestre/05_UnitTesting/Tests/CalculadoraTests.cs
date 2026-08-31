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
}
