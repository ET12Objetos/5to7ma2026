namespace Persistencia.Entidades;

public class Usuario
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nombre { get; set; } = default!;
    public string Email { get; set; } = default!;
}