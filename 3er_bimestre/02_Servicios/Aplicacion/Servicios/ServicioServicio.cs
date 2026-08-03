using Aplicacion.Entidades;
using Aplicacion.Repositorios;

namespace Aplicacion.Servicios;

public sealed class ServicioServicio(IServicioRepositorio servicioRepositorio)
{
    private readonly IServicioRepositorio _servicioRepositorio = servicioRepositorio ?? throw new ArgumentNullException(nameof(servicioRepositorio));

    public IReadOnlyList<Servicio> ObtenerTodos()
    {
        return _servicioRepositorio.ObtenerTodos();
    }

    public int Crear(Servicio servicio)
    {
        ArgumentNullException.ThrowIfNull(servicio);

        if (string.IsNullOrWhiteSpace(servicio.Nombre))
        {
            throw new ArgumentException("El nombre del servicio es obligatorio.");
        }

        if (servicio.DuracionMinutos <= 0)
        {
            throw new ArgumentException("La duracion del servicio debe ser mayor que cero.");
        }

        if (servicio.Precio < 0)
        {
            throw new ArgumentException("El precio del servicio no puede ser negativo.");
        }

        return _servicioRepositorio.Crear(servicio);
    }
}
