using Aplicacion.Entidades;
using Microsoft.VisualBasic;

namespace Aplicacion.Repositorios;

public interface IRolRepositorio
{
    IReadOnlyList<Rol> ObtenerTodos();

    Rol? ObtenerPorId(int id);

    int Crear(Rol rol);

    Rol Actualizar(int IdRol, Rol rol);
}
