using Dapper;
using Aplicacion.Entidades;
using Aplicacion.Repositorios;

namespace Persistencia.Repositorios;

public sealed class RolRepositorio(IDbConnectionFactory connectionFactory) : IRolRepositorio
{
    private readonly IDbConnectionFactory _connectionFactory
        = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

    public IReadOnlyList<Rol> ObtenerTodos()
    {
        const string sql = """
            SELECT id AS Id, nombre AS Nombre, descripcion AS Descripcion
            FROM roles
            ORDER BY nombre;
            """;

        using var connection = _connectionFactory.CrearConexion();
        var roles = connection.Query<Rol>(sql);
        return roles.AsList();
    }

    public Rol? ObtenerPorId(int id)
    {
        const string sql = """
            SELECT id AS Id, nombre AS Nombre, descripcion AS Descripcion
            FROM roles
            WHERE id = @Id;
            """;

        using var connection = _connectionFactory.CrearConexion();
        return connection.QuerySingleOrDefault<Rol>(sql, new { Id = id });
    }

    public int Crear(Rol rol)
    {
        const string sql = """
            INSERT INTO roles (nombre, descripcion)
            VALUES (@Nombre, @Descripcion);
            SELECT LAST_INSERT_ID();
            """;

        using var connection = _connectionFactory.CrearConexion();
        return connection.ExecuteScalar<int>(sql, rol);
    }

    public Rol Actualizar(int IdRol, Rol rol)
    {
        const string sql = """
            UPDATE roles
            SET nombre = @Nombre, descripcion = @Descripcion
            WHERE id = @Id;
            """;

        const string sqlObtener = """
            SELECT id AS Id, nombre AS Nombre, descripcion AS Descripcion
            FROM roles
            WHERE id = @Id;
            """;

        using var connection = _connectionFactory.CrearConexion();
        var filas = connection.Execute(sql, new
        {
            Id = IdRol,
            rol.Nombre,
            rol.Descripcion
        });

        if (filas == 0)
        {
            throw new InvalidOperationException("No existe un rol con ese Id.");
        }

        return connection.QuerySingle<Rol>(sqlObtener, new { Id = IdRol });
    }
}
