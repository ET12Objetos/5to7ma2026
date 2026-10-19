using Persistencia.Entidades;
using Persistencia.Contratos;

namespace Aplicacion.Servicios;

public interface IRolPermisoServicio
{
    List<Permiso> ObtenerPermisosPorRol(int rolId);
}

public class RolPermisoServicio : IRolPermisoServicio
{
    private readonly IRolPermisoRepositorio rolPermisoRepositorio;

    public RolPermisoServicio(IRolPermisoRepositorio _rolPermisoRepositorio)
    {
        rolPermisoRepositorio = _rolPermisoRepositorio;
    }

    public List<Permiso> ObtenerPermisosPorRol(int rolId)
    {
        return rolPermisoRepositorio.ObtenerPermisosPorRol(rolId).ToList();
    }
}