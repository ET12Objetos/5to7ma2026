using Aplicacion.Contratos;
using Aplicacion.DTOs;
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

    public UsuarioResponseDto CrearUsuario(UsuarioRequestDto usuarioDto)
    {
        //conversion de Usuario DTO => Entidad Usuario
        Usuario usuario = new Usuario
        {
            Nombre = usuarioDto.Nombre,
            Email = usuarioDto.Email
        };

        usuarioRepositorio.CreateUsuario(usuario);

        return new UsuarioResponseDto
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Email = usuario.Email
        };
    }

    public IReadOnlyList<UsuarioResponseDto> LeerUsuarios()
    {
        List<UsuarioResponseDto> usuarios = new List<UsuarioResponseDto>();

        //conversion de Entidad Usuario => Usuario DTO
        usuarios = usuarioRepositorio.GetUsuarios()
                    .Select(x => new UsuarioResponseDto
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        Email = x.Email
                    }).ToList();

        return usuarios;
    }
}