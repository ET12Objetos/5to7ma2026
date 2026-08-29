using Aplicacion.Repositorios;
using Aplicacion.Servicios;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

var services = new ServiceCollection();

services.AddSingleton<IDbConnectionFactory>(
    _ => new MySqlConnectionFactory(connectionString));

services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
services.AddTransient<IRolRepositorio, RolRepositorio>();
services.AddTransient<IPermisoRepositorio, PermisoRepositorio>();
services.AddTransient<IRolPermisoRepositorio, RolPermisoRepositorio>();

services.AddTransient<IUsuarioServicio, UsuarioServicio>();
services.AddTransient<IRolServicio, RolServicio>();
services.AddTransient<IPermisoServicio, PermisoServicio>();
services.AddTransient<IRolPermisoServicio, RolPermisoServicio>();

services.AddTransient<ConsoleMenu>();

using var serviceProvider = services.BuildServiceProvider();
var menu = serviceProvider.GetRequiredService<ConsoleMenu>();

menu.Ejecutar();
