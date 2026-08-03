using Aplicacion.Entidades;
using Aplicacion.Repositorios;

namespace Aplicacion.Servicios;

public sealed class TurnoServicio(
    ITurnoRepositorio turnoRepositorio,
    IClienteRepositorio clienteRepositorio,
    IProfesionalRepositorio profesionalRepositorio,
    IServicioRepositorio servicioRepositorio)
{
    private readonly ITurnoRepositorio _turnoRepositorio = turnoRepositorio ?? throw new ArgumentNullException(nameof(turnoRepositorio));
    private readonly IClienteRepositorio _clienteRepositorio = clienteRepositorio ?? throw new ArgumentNullException(nameof(clienteRepositorio));
    private readonly IProfesionalRepositorio _profesionalRepositorio = profesionalRepositorio ?? throw new ArgumentNullException(nameof(profesionalRepositorio));
    private readonly IServicioRepositorio _servicioRepositorio = servicioRepositorio ?? throw new ArgumentNullException(nameof(servicioRepositorio));

    public IReadOnlyList<Turno> ObtenerTodos()
    {
        return _turnoRepositorio.ObtenerTodos();
    }

    public int Solicitar(Turno turno)
    {
        ArgumentNullException.ThrowIfNull(turno);

        if (turno.ClienteId <= 0)
        {
            throw new ArgumentException("Debe seleccionar un cliente valido.");
        }

        if (turno.ProfesionalId <= 0)
        {
            throw new ArgumentException("Debe seleccionar un profesional valido.");
        }

        if (turno.ServicioId <= 0)
        {
            throw new ArgumentException("Debe seleccionar un servicio valido.");
        }

        if (turno.FechaHora <= DateTime.Now)
        {
            throw new ArgumentException("La fecha y hora del turno debe ser futura.");
        }

        if (_clienteRepositorio.ObtenerPorId(turno.ClienteId) is null)
        {
            throw new InvalidOperationException("El cliente seleccionado no existe.");
        }

        if (_profesionalRepositorio.ObtenerPorId(turno.ProfesionalId) is null)
        {
            throw new InvalidOperationException("El profesional seleccionado no existe.");
        }

        if (_servicioRepositorio.ObtenerPorId(turno.ServicioId) is null)
        {
            throw new InvalidOperationException("El servicio seleccionado no existe.");
        }

        if (_turnoRepositorio.ExisteTurno(turno.ProfesionalId, turno.FechaHora))
        {
            throw new InvalidOperationException("El profesional ya tiene un turno en ese horario.");
        }

        turno.Estado = string.IsNullOrWhiteSpace(turno.Estado) ? "Solicitado" : turno.Estado.Trim();
        return _turnoRepositorio.Crear(turno);
    }

    public bool ActualizarEstado(int id, string estado)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Debe indicar un turno valido.");
        }

        if (string.IsNullOrWhiteSpace(estado))
        {
            throw new ArgumentException("El estado es obligatorio.");
        }

        return _turnoRepositorio.ActualizarEstado(id, estado.Trim());
    }
}
