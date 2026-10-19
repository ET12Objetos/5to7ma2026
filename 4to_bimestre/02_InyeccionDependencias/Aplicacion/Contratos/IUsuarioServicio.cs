using Persistencia.Entidades;

namespace Aplicacion.Contratos;

public interface IUsuarioServicio
{
    IReadOnlyList<Usuario> LeerUsuarios();
}