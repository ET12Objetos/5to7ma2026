using Aplicacion.Entidades;

namespace Aplicacion.Repositorios;

public interface IPermisoRepositorio
{
    IReadOnlyList<Permiso> ObtenerTodos();

    Permiso? ObtenerPorId(int id);

    int Crear(Permiso permiso);
}
