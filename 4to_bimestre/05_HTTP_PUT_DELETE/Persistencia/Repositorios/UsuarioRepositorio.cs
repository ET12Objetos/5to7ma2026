using System.Data;
using Dapper;
using Persistencia.Contratos;
using Persistencia.Entidades;

namespace Persistencia.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly IDbConnection conexion;

    public UsuarioRepositorio(IDbConnection conexion)
    {
        this.conexion = conexion;
    }

    public void CreateUsuario(Usuario usuario)
    {
        const string sql = """
            INSERT INTO Usuarios (Id, Nombre, Email)
            VALUES (@Id, @Nombre, @Email);
            """;

        conexion.Execute(sql, usuario);
    }

    public IReadOnlyList<Usuario> GetUsuarios()
    {
        const string sql = "SELECT Id, Nombre, Email FROM Usuarios ORDER BY Nombre, Id;";

        return conexion.Query<Usuario>(sql).AsList();
    }

    public bool UpdateUsuario(Usuario usuario)
    {
        const string sql = """
            UPDATE Usuarios
            SET Nombre = @Nombre, Email = @Email
            WHERE Id = @Id;
            """;

        return conexion.Execute(sql, usuario) > 0;
    }

    public bool DeleteUsuario(Guid id)
    {
        const string sql = "DELETE FROM Usuarios WHERE Id = @Id;";

        return conexion.Execute(sql, new { Id = id }) > 0;
    }
}
