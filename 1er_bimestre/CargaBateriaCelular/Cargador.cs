namespace CargaBateriaCelular;

public class Cargador
{
    public void CargarUn(Celular celular, int porcentajeCarga)
    {
        celular.CargarBateria(porcentajeCarga);
    }
}