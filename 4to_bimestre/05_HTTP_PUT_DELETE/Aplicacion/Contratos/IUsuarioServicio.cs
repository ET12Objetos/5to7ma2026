using Aplicacion.DTOs;

namespace Aplicacion.Contratos;

public interface IUsuarioServicio
{
    UsuarioResponseDto CrearUsuario(UsuarioRequestDto usuarioDto);
    IReadOnlyList<UsuarioResponseDto> LeerUsuarios();
    UsuarioResponseDto? ActualizarUsuario(Guid id, UsuarioRequestDto usuarioDto);
    bool BorrarUsuario(Guid id);
}
