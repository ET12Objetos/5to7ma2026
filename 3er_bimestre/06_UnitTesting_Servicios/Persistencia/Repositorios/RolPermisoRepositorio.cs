using Dapper;
using Aplicacion.Entidades;
using Aplicacion.Repositorios;

namespace Persistencia.Repositorios;

public sealed class RolPermisoRepositorio(IDbConnectionFactory connectionFactory) : IRolPermisoRepositorio
{
    private readonly IDbConnectionFactory _connectionFactory
        = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

    public IReadOnlyList<Permiso> ObtenerPermisosPorRol(int rolId)
    {
        const string sql = """
            SELECT p.id AS Id,
                   p.codigo AS Codigo,
                   p.descripcion AS Descripcion
            FROM roles_permisos rp
            INNER JOIN permisos p ON p.id = rp.permiso_id
            WHERE rp.rol_id = @RolId
            ORDER BY p.codigo;
            """;

        using var connection = _connectionFactory.CrearConexion();
        var permisos = connection.Query<Permiso>(sql, new { RolId = rolId });
        return permisos.AsList();
    }

    public bool ExisteAsignacion(int rolId, int permisoId)
    {
        const string sql = """
            SELECT COUNT(1)
            FROM roles_permisos
            WHERE rol_id = @RolId
              AND permiso_id = @PermisoId;
            """;

        using var connection = _connectionFactory.CrearConexion();
        var cantidad = connection.ExecuteScalar<int>(sql, new { RolId = rolId, PermisoId = permisoId });
        return cantidad > 0;
    }

    public bool AsignarPermiso(int rolId, int permisoId)
    {
        if (ExisteAsignacion(rolId, permisoId))
        {
            return false;
        }

        const string sql = """
            INSERT INTO roles_permisos (rol_id, permiso_id)
            VALUES (@RolId, @PermisoId);
            """;

        using var connection = _connectionFactory.CrearConexion();
        var filas = connection.Execute(sql, new { RolId = rolId, PermisoId = permisoId });
        return filas > 0;
    }

    public bool QuitarPermiso(int rolId, int permisoId)
    {
        const string sql = """
            DELETE FROM roles_permisos
            WHERE rol_id = @RolId
              AND permiso_id = @PermisoId;
            """;

        using var connection = _connectionFactory.CrearConexion();
        var filas = connection.Execute(sql, new { RolId = rolId, PermisoId = permisoId });
        return filas > 0;
    }
}
