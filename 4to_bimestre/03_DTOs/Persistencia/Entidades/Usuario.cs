namespace Persistencia.Entidades;

public class Usuario
{
    //public int Id { get; set; }
    public Guid Id { get; set; } = new Guid();
    public string Nombre { get; set; } = default!;
    public string Email { get; set; } = default!;
}