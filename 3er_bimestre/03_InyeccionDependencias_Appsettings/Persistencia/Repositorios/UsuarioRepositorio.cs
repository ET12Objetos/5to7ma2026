using Dapper;
using Aplicacion.Entidades;
using Aplicacion.Repositorios;

namespace Persistencia.Repositorios;

public sealed class UsuarioRepositorio(IDbConnectionFactory connectionFactory) : IUsuarioRepositorio
{
    private readonly IDbConnectionFactory _connectionFactory
        = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

    public IReadOnlyList<Usuario> ObtenerTodos()
    {
        const string sql = """
            SELECT id AS Id,
                   nombre AS Nombre,
                   apellido AS Apellido,
                   nombre_usuario AS NombreUsuario,
                   email AS Email,
                   rol_id AS RolId,
                   activo AS Activo
            FROM usuarios
            ORDER BY apellido, nombre;
            """;

        using var connection = _connectionFactory.CrearConexion();
        var usuarios = connection.Query<Usuario>(sql);
        return usuarios.AsList();
    }

    public Usuario? ObtenerPorId(int id)
    {
        const string sql = """
            SELECT id AS Id,
                   nombre AS Nombre,
                   apellido AS Apellido,
                   nombre_usuario AS NombreUsuario,
                   email AS Email,
                   rol_id AS RolId,
                   activo AS Activo
            FROM usuarios
            WHERE id = @Id;
            """;

        using var connection = _connectionFactory.CrearConexion();
        return connection.QuerySingleOrDefault<Usuario>(sql, new { Id = id });
    }

    public int Crear(Usuario usuario)
    {
        const string sql = """
            INSERT INTO usuarios (nombre, apellido, nombre_usuario, email, rol_id, activo)
            VALUES (@Nombre, @Apellido, @NombreUsuario, @Email, @RolId, @Activo);
            SELECT LAST_INSERT_ID();
            """;

        using var connection = _connectionFactory.CrearConexion();
        return connection.ExecuteScalar<int>(sql, usuario);
    }

    public bool ActualizarRol(int id, int rolId)
    {
        const string sql = """
            UPDATE usuarios
            SET rol_id = @RolId
            WHERE id = @Id;
            """;

        using var connection = _connectionFactory.CrearConexion();
        var filas = connection.Execute(sql, new { Id = id, RolId = rolId });
        return filas > 0;
    }

    public bool CambiarEstado(int id, bool activo)
    {
        const string sql = """
            UPDATE usuarios
            SET activo = @Activo
            WHERE id = @Id;
            """;

        using var connection = _connectionFactory.CrearConexion();
        var filas = connection.Execute(sql, new { Id = id, Activo = activo });
        return filas > 0;
    }
}
