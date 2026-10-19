using Persistencia.Entidades;

namespace Persistencia.Contratos;

public interface IUsuarioRepositorio
{
    IReadOnlyList<Usuario> GetUsuarios();
}