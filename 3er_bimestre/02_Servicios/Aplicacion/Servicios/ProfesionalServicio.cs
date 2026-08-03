using Aplicacion.Entidades;
using Aplicacion.Repositorios;

namespace Aplicacion.Servicios;

public sealed class ProfesionalServicio(IProfesionalRepositorio profesionalRepositorio)
{
    private readonly IProfesionalRepositorio _profesionalRepositorio = profesionalRepositorio ?? throw new ArgumentNullException(nameof(profesionalRepositorio));

    public IReadOnlyList<Profesional> ObtenerTodos()
    {
        return _profesionalRepositorio.ObtenerTodos();
    }

    public int Crear(Profesional profesional)
    {
        ArgumentNullException.ThrowIfNull(profesional);

        if (string.IsNullOrWhiteSpace(profesional.Nombre))
        {
            throw new ArgumentException("El nombre del profesional es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(profesional.Especialidad))
        {
            throw new ArgumentException("La especialidad del profesional es obligatoria.");
        }

        return _profesionalRepositorio.Crear(profesional);
    }
}
