using Persistencia.Contratos;
using Persistencia.Entidades;

namespace Persistencia.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    List<Usuario> usuarios = new List<Usuario>()
        {
            new Usuario {Id = Guid.NewGuid(), Nombre = "Juan", Email = "juan@mail.com"},
            new Usuario {Id = Guid.NewGuid(), Nombre = "Martin", Email = "martin@mail.com"},
            new Usuario {Id = Guid.NewGuid(), Nombre = "Leonardo", Email = "leonardo@mail.com"},
        };

    public void CreateUsuario(Usuario usuario)
    {
        usuarios.Add(usuario);
    }

    public IReadOnlyList<Usuario> GetUsuarios()
    {
        return usuarios;
    }
}