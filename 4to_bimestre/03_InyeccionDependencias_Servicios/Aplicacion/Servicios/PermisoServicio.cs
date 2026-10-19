using Persistencia.Entidades;
using Persistencia.Contratos;

namespace Aplicacion.Servicios;

public interface IPermisoServicio
{
    List<Permiso> ObtenerTodos();
}

public class PermisoServicio : IPermisoServicio
{
    private readonly IPermisoRepositorio permisoRepositorio;

    public PermisoServicio(IPermisoRepositorio permisoRepositorio)
    {
        this.permisoRepositorio = permisoRepositorio;
    }

    public List<Permiso> ObtenerTodos()
    {
        return permisoRepositorio.ObtenerTodos().ToList();
    }
}
