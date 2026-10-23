using Aplicacion.DTOs;

namespace Aplicacion.Contratos;

public interface IUsuarioServicio
{
    UsuarioResponseDto CrearUsuario(UsuarioRequestDto usuarioDto);
    IReadOnlyList<UsuarioResponseDto> LeerUsuarios();
}