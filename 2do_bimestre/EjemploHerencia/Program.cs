using EjemploHerencia;

Profesor profesor = new Profesor("Jose", "jose@mail.com", 1234);     //bien

Alumno alumno = new Alumno("Juan", "juan@mail.com", new DateOnly(2023, 2, 4));   //bien

alumno.InformarNombre();

profesor.InformarNombre();

//Persona persona = new Persona();    //mal