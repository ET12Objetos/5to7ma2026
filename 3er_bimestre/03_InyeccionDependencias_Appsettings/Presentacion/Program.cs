using Aplicacion.Servicios;
using Microsoft.Extensions.Configuration;
using Persistencia;
using Persistencia.Repositorios;
using Presentacion;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var connectionString = configuration.GetConnectionString("Default")
    ?? throw new InvalidOperationException(
        "No se encontro la cadena de conexion 'Default' en appsettings.json.");

var connectionFactory = new MySqlConnectionFactory(connectionString);

var usuarioRepositorio = new UsuarioRepositorio(connectionFactory);
var rolRepositorio = new RolRepositorio(connectionFactory);
var permisoRepositorio = new PermisoRepositorio(connectionFactory);
var rolPermisoRepositorio = new RolPermisoRepositorio(connectionFactory);

var usuarioServicio = new UsuarioServicio(usuarioRepositorio);
var rolServicio = new RolServicio(rolRepositorio);
var permisoServicio = new PermisoServicio(permisoRepositorio);
var rolPermisoServicio = new RolPermisoServicio(rolPermisoRepositorio);

var menu = new ConsoleMenu(
    usuarioServicio,
    rolServicio,
    permisoServicio,
    rolPermisoServicio);

menu.Ejecutar();
