using Aplicacion.Entidades;
using Aplicacion.Repositorios;

namespace Aplicacion.Servicios;

public interface IUsuarioServicio
{
    List<Usuario> ObtenerTodos();

    int Crear(Usuario usuario);

    bool ActualizarRol(int usuarioId, int rolId);

    bool CambiarEstado(int usuarioId, bool activo);
}

public class UsuarioServicio
    : IUsuarioServicio
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;

    public UsuarioServicio(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio
            ?? throw new ArgumentNullException(nameof(usuarioRepositorio));
    }

    public List<Usuario> ObtenerTodos()
    {
        return _usuarioRepositorio.ObtenerTodos().ToList();
    }

    public int Crear(Usuario usuario)
    {
        ArgumentNullException.ThrowIfNull(usuario);

        if (string.IsNullOrWhiteSpace(usuario.NombreUsuario))
        {
            throw new ArgumentException("El nombre de usuario es obligatorio.");
        }

        return _usuarioRepositorio.Crear(usuario);
    }

    public bool ActualizarRol(int usuarioId, int rolId)
    {
        if (usuarioId <= 0 || rolId <= 0)
        {
            throw new ArgumentException("Los Id deben ser mayores a cero.");
        }

        return _usuarioRepositorio.ActualizarRol(usuarioId, rolId);
    }

    public bool CambiarEstado(int usuarioId, bool activo)
    {
        if (usuarioId <= 0)
        {
            throw new ArgumentException("El Id del usuario debe ser mayor a cero.");
        }

        return _usuarioRepositorio.CambiarEstado(usuarioId, activo);
    }
}
