using EjemploInterfaces;

Perro perro1 = new Perro();

Perro perro2 = new();

var perro3 = new Perro();

//---------------------------------

IAnimal perro4 = new Perro();

IAnimal gato4 = new Gato();

//No funciona pues el metodo Volar() no se encuentra
//definido en la interface IAnimal
//gato4.Volar();

//Es incorrecto debibo a que la clase Perro no implementa
//la interfaz IVolador
//IVolador perro = new Perro();

IVolador gato5 = new Gato();

gato5.Volar();