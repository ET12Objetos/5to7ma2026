using Aplicacion.Entidades;

namespace Aplicacion.Repositorios;

public interface IUsuarioRepositorio
{
    IReadOnlyList<Usuario> ObtenerTodos();

    Usuario? ObtenerPorId(int id);

    int Crear(Usuario usuario);

    bool ActualizarRol(int id, int rolId);

    bool CambiarEstado(int id, bool activo);
}
