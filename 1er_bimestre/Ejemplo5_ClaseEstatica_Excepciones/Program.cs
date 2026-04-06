using Ejemplo5_ClaseEstatica_Excepciones;

try
{
    Persona persona = new Persona("jose", "asd", 2000);
}
catch (ArgumentException)
{
    Console.WriteLine($"Pasaron cosas.. ");
}

//La siguiente sentencia no se puede ejecutar puesto que 
//el objeto persona se encuentra dentro del alcance (scope) del try-catch
//persona.CambiarDireccion("algo");