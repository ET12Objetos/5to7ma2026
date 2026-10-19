using Persistencia.Contratos;
using Persistencia.Entidades;

namespace Persistencia.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    public IReadOnlyList<Usuario> GetUsuarios()
    {
        return new List<Usuario>()
        {
            new Usuario {Id = 1, Nombre = "Juan", Email = "juan@mail.com"},
            new Usuario {Id = 2, Nombre = "Martin", Email = "martin@mail.com"},
            new Usuario {Id = 3, Nombre = "Leonardo", Email = "leonardo@mail.com"},
        };
    }
}