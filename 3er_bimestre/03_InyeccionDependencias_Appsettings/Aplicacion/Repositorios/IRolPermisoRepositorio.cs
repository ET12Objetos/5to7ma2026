using Aplicacion.Entidades;

namespace Aplicacion.Repositorios;

public interface IRolPermisoRepositorio
{
    IReadOnlyList<Permiso> ObtenerPermisosPorRol(int rolId);

    bool ExisteAsignacion(int rolId, int permisoId);

    bool AsignarPermiso(int rolId, int permisoId);

    bool QuitarPermiso(int rolId, int permisoId);
}
