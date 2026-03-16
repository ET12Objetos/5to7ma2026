namespace Ejemplo2;

public class Curso
{
    public Alumno[] alumnos;
    public int totalAlumnos;

    public Curso()
    {
        alumnos = new Alumno[5];
        totalAlumnos = 0;
    }

    public void AgregarUn(Alumno alumno)
    {
        if (totalAlumnos < alumnos.Length)
        {
            alumnos[totalAlumnos] = alumno;
            totalAlumnos++;
        }
    }

    public Alumno MejorAlumno()
    {
        Alumno mejorAlumno = alumnos[0];

        for (int i = 1; i < totalAlumnos; i++)
        {
            if (alumnos[i].nota > mejorAlumno.nota)
                mejorAlumno = alumnos[i];
        }

        return mejorAlumno;
    }
}