namespace Aplicacion.Entidades;

public sealed class Usuario : Persona
{
    public string Apellido { get; set; } = string.Empty;

    public string NombreUsuario { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int RolId { get; set; }

    public bool Activo { get; set; } = true;
}
