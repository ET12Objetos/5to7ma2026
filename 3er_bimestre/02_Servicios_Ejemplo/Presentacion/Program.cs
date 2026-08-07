using System.Text.Json;
using Aplicacion.Servicios;
using Persistencia;
using Persistencia.Repositorios;
using Presentacion;

var connectionString = ObtenerCadenaConexion();
var connectionFactory = new MySqlConnectionFactory(connectionString);

var usuarioRepositorio = new UsuarioRepositorio(connectionFactory);

var rolRepositorio = new RolRepositorio(connectionFactory);
var rolServicio = new RolServicio(rolRepositorio);

var permisoRepositorio = new PermisoRepositorio(connectionFactory);
var permisoServicio = new PermisoServicio(permisoRepositorio);

var rolPermisoRepositorio = new RolPermisoRepositorio(connectionFactory);
var rolPermisoServicio = new RolPermisoServicio(rolPermisoRepositorio);

var menu = new ConsoleMenu(usuarioRepositorio,
    rolServicio,
    permisoServicio,
    rolPermisoServicio
    );

menu.Ejecutar();

static string ObtenerCadenaConexion()
{
    const string fallback = "Server=localhost;Port=3306;Database=usuarios_app;User ID=root;Password=;";
    var ruta = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

    if (!File.Exists(ruta))
    {
        return fallback;
    }

    using var stream = File.OpenRead(ruta);
    using var document = JsonDocument.Parse(stream);

    if (!document.RootElement.TryGetProperty("ConnectionStrings", out var connectionStrings))
    {
        return fallback;
    }

    if (!connectionStrings.TryGetProperty("Default", out var defaultConnection))
    {
        return fallback;
    }

    var value = defaultConnection.GetString();
    return string.IsNullOrWhiteSpace(value) ? fallback : value;
}
