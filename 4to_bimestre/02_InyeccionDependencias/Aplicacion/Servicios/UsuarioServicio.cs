using Aplicacion.Contratos;
using Persistencia.Contratos;
using Persistencia.Entidades;

namespace Aplicacion.Servicios;

public class UsuarioServicio : IUsuarioServicio
{
    private readonly IUsuarioRepositorio usuarioRepositorio;

    public UsuarioServicio(IUsuarioRepositorio usuarioRepositorio)
    {
        this.usuarioRepositorio = usuarioRepositorio;
    }

    public IReadOnlyList<Usuario> LeerUsuarios()
    {
        return usuarioRepositorio.GetUsuarios();
    }
}