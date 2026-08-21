using Aplicacion.Entidades;
using Aplicacion.Repositorios;

namespace Aplicacion.Servicios;

public interface IRolServicio
{
    int Crear(Rol rol);
    List<Rol> ObtenerTodos();
    Rol Actualizar(int IdRol, Rol rol);
}

public class RolServicio : IRolServicio
{
    private readonly IRolRepositorio rolRepositorio;

    public RolServicio(IRolRepositorio rolRepositorio)
    {
        this.rolRepositorio = rolRepositorio;
    }

    public Rol Actualizar(int IdRol, Rol rol)
    {
        if (IdRol <= 0)
        {
            throw new ArgumentException("El Id del rol debe ser mayor a cero.");
        }

        if (string.IsNullOrWhiteSpace(rol.Nombre))
        {
            throw new ArgumentException("El nombre del rol es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(rol.Descripcion))
        {
            throw new ArgumentException("La descripcion del rol es obligatoria.");
        }

        return rolRepositorio.Actualizar(IdRol, rol);
    }

    public int Crear(Rol rol)
    {
        //realizar validaciones varias

        return rolRepositorio.Crear(rol);
    }

    public List<Rol> ObtenerTodos()
    {
        return rolRepositorio.ObtenerTodos().ToList();
    }
}
