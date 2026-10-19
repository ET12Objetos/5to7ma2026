using Persistencia.Entidades;

namespace Persistencia.Contratos;

public interface IPermisoRepositorio
{
    IReadOnlyList<Permiso> ObtenerTodos();

    Permiso? ObtenerPorId(int id);

    int Crear(Permiso permiso);
}
