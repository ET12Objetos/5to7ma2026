using Dapper;
using Aplicacion.Entidades;
using Aplicacion.Repositorios;

namespace Persistencia.Repositorios;

public sealed class PermisoRepositorio(IDbConnectionFactory connectionFactory) : IPermisoRepositorio
{
    private readonly IDbConnectionFactory _connectionFactory
        = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

    public IReadOnlyList<Permiso> ObtenerTodos()
    {
        const string sql = """
            SELECT id AS Id, codigo AS Codigo, descripcion AS Descripcion
            FROM permisos
            ORDER BY codigo;
            """;

        using var connection = _connectionFactory.CrearConexion();
        var permisos = connection.Query<Permiso>(sql);
        return permisos.AsList();
    }

    public Permiso? ObtenerPorId(int id)
    {
        const string sql = """
            SELECT id AS Id, codigo AS Codigo, descripcion AS Descripcion
            FROM permisos
            WHERE id = @Id;
            """;

        using var connection = _connectionFactory.CrearConexion();
        return connection.QuerySingleOrDefault<Permiso>(sql, new { Id = id });
    }

    public int Crear(Permiso permiso)
    {
        const string sql = """
            INSERT INTO permisos (codigo, descripcion)
            VALUES (@Codigo, @Descripcion);
            SELECT LAST_INSERT_ID();
            """;

        using var connection = _connectionFactory.CrearConexion();
        return connection.ExecuteScalar<int>(sql, permiso);
    }
}
