using Persistencia.Entidades;
using Microsoft.VisualBasic;

namespace Persistencia.Contratos;

public interface IRolRepositorio
{
    IReadOnlyList<Rol> ObtenerTodos();

    Rol? ObtenerPorId(int id);

    int Crear(Rol rol);

    Rol Actualizar(int IdRol, Rol rol);
}
