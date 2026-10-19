namespace Presentacion;

public class Usuario
{
    public Guid Id { get; set; } = new Guid();
    public string Nombre { get; set; } = default!;
    public string Email { get; set; } = default!;
}