using CargaBateriaCelular;

Celular celular = new Celular(35);

Cargador cargador = new Cargador();

celular.InforarPorcentajeBateria();
cargador.CargarUn(celular, 120);
celular.InforarPorcentajeBateria();
cargador.CargarUn(celular, 24);
celular.InforarPorcentajeBateria();
celular.ConsumirBateria(47);

//escenario 1
celular.InforarPorcentajeBateria();

//escenario 2
//accede al valor atributo porcentajeActual desde fuera del objeto celular
Console.WriteLine($"El porcentaje de la bateria es: {celular.porcentajeActual}%");
