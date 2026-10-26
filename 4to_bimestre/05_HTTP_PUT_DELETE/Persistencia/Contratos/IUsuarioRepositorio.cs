using Persistencia.Entidades;

namespace Persistencia.Contratos;

public interface IUsuarioRepositorio
{
    IReadOnlyList<Usuario> GetUsuarios();

    void CreateUsuario(Usuario usuario);
    bool UpdateUsuario(Usuario usuario);
    bool DeleteUsuario(Guid id);
}
