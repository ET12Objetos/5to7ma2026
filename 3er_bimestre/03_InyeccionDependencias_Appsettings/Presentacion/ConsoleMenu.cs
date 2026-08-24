using System.Globalization;
using Aplicacion.Entidades;
using Aplicacion.Servicios;

namespace Presentacion;

internal sealed class ConsoleMenu
{
    private readonly IUsuarioServicio _usuarioServicio;
    private readonly IRolServicio _rolServicio;
    private readonly IPermisoServicio _permisoServicio;
    private readonly IRolPermisoServicio _rolPermisoServicio;

    public ConsoleMenu(IUsuarioServicio usuarioServicio,
        IRolServicio rolServicio,
        IPermisoServicio permisoServicio,
        IRolPermisoServicio rolPermisoServicio)
    {
        _usuarioServicio = usuarioServicio;
        _rolServicio = rolServicio;
        _permisoServicio = permisoServicio;
        _rolPermisoServicio = rolPermisoServicio;
    }

    public void Ejecutar()
    {
        var salir = false;

        while (!salir)
        {
            MostrarOpciones();
            var opcion = Console.ReadLine()?.Trim();

            try
            {
                switch (opcion)
                {
                    case "1":
                        ListarUsuarios();
                        break;
                    case "2":
                        RegistrarUsuario();
                        break;
                    case "3":
                        CambiarRolUsuario();
                        break;
                    case "4":
                        CambiarEstadoUsuario();
                        break;
                    case "5":
                        ListarRoles();
                        break;
                    case "6":
                        RegistrarRol();
                        break;
                    case "7":
                        ListarPermisos();
                        break;
                    case "8":
                        RegistrarPermiso();
                        break;
                    case "9":
                        AsignarPermisoARol();
                        break;
                    case "10":
                        QuitarPermisoARol();
                        break;
                    case "11":
                        ListarPermisosPorRol();
                        break;
                    case "12":
                        ActualizarRol();
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opcion invalida.");
                        break;
                }
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo completar la operacion: {ex.Message}");
            }

            if (!salir)
            {
                Console.WriteLine();
                Console.Write("Presione Enter para continuar...");
                Console.ReadLine();
            }
        }
    }

    private void ActualizarRol()
    {
        ListarRoles();
        var rolId = LeerEntero("Rol Id: ");

        var rol = new Rol
        {
            Nombre = LeerTexto("Nuevo nombre: "),
            Descripcion = LeerTexto("Nueva descripcion: ")
        };

        var rolActualizado = _rolServicio.Actualizar(rolId, rol);
        Console.WriteLine($"Rol actualizado: {rolActualizado.Id}. {rolActualizado.Nombre} - {rolActualizado.Descripcion}");
    }


    private static void MostrarOpciones()
    {
        Console.Clear();
        Console.WriteLine("Gestion de usuarios");
        Console.WriteLine("-------------------");
        Console.WriteLine("1. Listar usuarios");
        Console.WriteLine("2. Registrar usuario");
        Console.WriteLine("3. Cambiar rol de usuario");
        Console.WriteLine("4. Activar o desactivar usuario");
        Console.WriteLine("5. Listar roles");
        Console.WriteLine("6. Registrar rol");
        Console.WriteLine("7. Listar permisos");
        Console.WriteLine("8. Registrar permiso");
        Console.WriteLine("9. Asignar permiso a rol");
        Console.WriteLine("10. Quitar permiso a rol");
        Console.WriteLine("11. Listar permisos por rol");
        Console.WriteLine("12. Actualizar rol");
        Console.WriteLine("0. Salir");
        Console.Write("Seleccione una opcion: ");
    }

    private void ListarUsuarios()
    {
        var usuarios = _usuarioServicio.ObtenerTodos();

        Console.WriteLine();
        Console.WriteLine("Usuarios");
        foreach (var usuario in usuarios)
        {
            var estado = usuario.Activo ? "Activo" : "Inactivo";
            Console.WriteLine(
                $"{usuario.Id}. {usuario.Apellido}, {usuario.Nombre} | @{usuario.NombreUsuario} | {usuario.Email} | Rol {usuario.RolId} | {estado}");
        }

        MostrarSinDatos(usuarios.Count);
    }

    private void RegistrarUsuario()
    {
        ListarRoles();

        var usuario = new Usuario
        {
            Nombre = LeerTexto("Nombre: "),
            Apellido = LeerTexto("Apellido: "),
            NombreUsuario = LeerTexto("Nombre de usuario: "),
            Email = LeerTexto("Email: "),
            RolId = LeerEntero("Rol Id: "),
            Activo = true
        };

        var id = _usuarioServicio.Crear(usuario);
        Console.WriteLine($"Usuario registrado con Id {id}.");
    }

    private void CambiarRolUsuario()
    {
        ListarUsuarios();
        var usuarioId = LeerEntero("Usuario Id: ");

        ListarRoles();
        var rolId = LeerEntero("Nuevo rol Id: ");

        var actualizado = _usuarioServicio.ActualizarRol(usuarioId, rolId);
        Console.WriteLine(actualizado ? "Rol actualizado." : "No existe un usuario con ese Id.");
    }

    private void CambiarEstadoUsuario()
    {
        ListarUsuarios();
        var usuarioId = LeerEntero("Usuario Id: ");
        var activo = LeerBooleano("Activo (s/n): ");

        var actualizado = _usuarioServicio.CambiarEstado(usuarioId, activo);
        Console.WriteLine(actualizado ? "Estado actualizado." : "No existe un usuario con ese Id.");
    }

    private void ListarRoles()
    {
        var roles = _rolServicio.ObtenerTodos();

        Console.WriteLine();
        Console.WriteLine("Roles");
        foreach (var rol in roles)
        {
            Console.WriteLine($"{rol.Id}. {rol.Nombre} - {rol.Descripcion}");
        }

        MostrarSinDatos(roles.Count);
    }

    private void RegistrarRol()
    {
        var rol = new Rol
        {
            Nombre = LeerTexto("Nombre: "),
            Descripcion = LeerTexto("Descripcion: ")
        };

        //var id = _rolRepositorio.Crear(rol);
        var id = _rolServicio.Crear(rol);
        Console.WriteLine($"Rol registrado con Id {id}.");
    }

    private void ListarPermisos()
    {
        var permisos = _permisoServicio.ObtenerTodos();

        Console.WriteLine();
        Console.WriteLine("Permisos");
        foreach (var permiso in permisos)
        {
            Console.WriteLine($"{permiso.Id}. {permiso.Codigo} - {permiso.Descripcion}");
        }

        MostrarSinDatos(permisos.Count);
    }

    private void RegistrarPermiso()
    {
        // var permiso = new Permiso
        // {
        //     Codigo = LeerTexto("Codigo: "),
        //     Descripcion = LeerTexto("Descripcion: ")
        // };

        // var id = _permisoRepositorio.Crear(permiso);
        //Console.WriteLine($"Permiso registrado con Id {id}.");
    }

    private void AsignarPermisoARol()
    {
        // ListarRoles();
        // var rolId = LeerEntero("Rol Id: ");

        // ListarPermisos();
        // var permisoId = LeerEntero("Permiso Id: ");

        // var asignado = _rolPermisoRepositorio.AsignarPermiso(rolId, permisoId);
        // Console.WriteLine(asignado ? "Permiso asignado." : "Ese rol ya tiene el permiso indicado.");
    }

    private void QuitarPermisoARol()
    {
        // ListarRoles();
        // var rolId = LeerEntero("Rol Id: ");

        // ListarPermisosPorRol(rolId);
        // var permisoId = LeerEntero("Permiso Id: ");

        // var quitado = _rolPermisoRepositorio.QuitarPermiso(rolId, permisoId);
        // Console.WriteLine(quitado ? "Permiso quitado." : "No existe esa asignacion.");
    }

    private void ListarPermisosPorRol()
    {
        ListarRoles();
        var rolId = LeerEntero("Rol Id: ");
        ListarPermisosPorRol(rolId);
    }

    private void ListarPermisosPorRol(int rolId)
    {
        var permisos = _rolPermisoServicio.ObtenerPermisosPorRol(rolId);

        Console.WriteLine();
        Console.WriteLine($"Permisos del rol {rolId}");
        foreach (var permiso in permisos)
        {
            Console.WriteLine($"{permiso.Id}. {permiso.Codigo} - {permiso.Descripcion}");
        }

        MostrarSinDatos(permisos.Count);
    }

    private static string LeerTexto(string mensaje)
    {
        Console.Write(mensaje);
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    private static int LeerEntero(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            if (int.TryParse(Console.ReadLine(), NumberStyles.Integer, CultureInfo.CurrentCulture, out var valor))
            {
                return valor;
            }

            Console.WriteLine("Ingrese un numero entero valido.");
        }
    }

    private static bool LeerBooleano(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            var entrada = Console.ReadLine()?.Trim().ToLowerInvariant();

            if (entrada is "s" or "si")
            {
                return true;
            }

            if (entrada is "n" or "no")
            {
                return false;
            }

            Console.WriteLine("Ingrese s o n.");
        }
    }

    private static void MostrarSinDatos(int cantidad)
    {
        if (cantidad == 0)
        {
            Console.WriteLine("No hay datos cargados.");
        }
    }
}
