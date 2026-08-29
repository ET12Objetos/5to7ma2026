using Aplicacion.Entidades;
using Aplicacion.Repositorios;
using Aplicacion.Servicios;

namespace Tests;

public class RolServicioTests
{
    [Fact]
    public void Crear_RolValido_DevuelveIdGeneradoPorRepositorio()
    {
        var repositorio = new RolRepositorioFalso
        {
            IdGenerado = 15
        };
        var servicio = new RolServicio(repositorio);
        var rol = CrearRolValido();

        var resultado = servicio.Crear(rol);

        Assert.Equal(15, resultado);
        Assert.Same(rol, repositorio.RolRecibidoAlCrear);
        Assert.Equal(1, repositorio.CantidadDeLlamadasACrear);
    }

    [Fact]
    public void ObtenerTodos_CuandoHayRoles_DevuelveLosRolesDelRepositorio()
    {
        var roles = new List<Rol>
        {
            new() { Id = 1, Nombre = "Administrador", Descripcion = "Acceso total" },
            new() { Id = 2, Nombre = "Operador", Descripcion = "Acceso operativo" }
        };
        var repositorio = new RolRepositorioFalso
        {
            Roles = roles
        };
        var servicio = new RolServicio(repositorio);

        var resultado = servicio.ObtenerTodos();

        Assert.Equal(roles, resultado);
        Assert.NotSame(roles, resultado);
        Assert.Equal(1, repositorio.CantidadDeLlamadasAObtenerTodos);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Actualizar_IdInvalido_LanzaArgumentExceptionYNoInvocaRepositorio(int idRol)
    {
        var repositorio = new RolRepositorioFalso();
        var servicio = new RolServicio(repositorio);

        var excepcion = Assert.Throws<ArgumentException>(
            () => servicio.Actualizar(idRol, CrearRolValido()));

        Assert.Equal("El Id del rol debe ser mayor a cero.", excepcion.Message);
        Assert.Equal(0, repositorio.CantidadDeLlamadasAActualizar);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Actualizar_NombreInvalido_LanzaArgumentExceptionYNoInvocaRepositorio(string nombre)
    {
        var repositorio = new RolRepositorioFalso();
        var servicio = new RolServicio(repositorio);
        var rol = CrearRolValido();
        rol.Nombre = nombre;

        var excepcion = Assert.Throws<ArgumentException>(
            () => servicio.Actualizar(1, rol));

        Assert.Equal("El nombre del rol es obligatorio.", excepcion.Message);
        Assert.Equal(0, repositorio.CantidadDeLlamadasAActualizar);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Actualizar_DescripcionInvalida_LanzaArgumentExceptionYNoInvocaRepositorio(
        string descripcion)
    {
        var repositorio = new RolRepositorioFalso();
        var servicio = new RolServicio(repositorio);
        var rol = CrearRolValido();
        rol.Descripcion = descripcion;

        var excepcion = Assert.Throws<ArgumentException>(
            () => servicio.Actualizar(1, rol));

        Assert.Equal("La descripcion del rol es obligatoria.", excepcion.Message);
        Assert.Equal(0, repositorio.CantidadDeLlamadasAActualizar);
    }

    [Fact]
    public void Actualizar_DatosValidos_DevuelveRolActualizadoPorRepositorio()
    {
        var rolActualizado = new Rol
        {
            Id = 7,
            Nombre = "Supervisor",
            Descripcion = "Supervisa operaciones"
        };
        var repositorio = new RolRepositorioFalso
        {
            RolActualizado = rolActualizado
        };
        var servicio = new RolServicio(repositorio);
        var cambios = CrearRolValido();

        var resultado = servicio.Actualizar(7, cambios);

        Assert.Same(rolActualizado, resultado);
        Assert.Equal(7, repositorio.IdRecibidoAlActualizar);
        Assert.Same(cambios, repositorio.RolRecibidoAlActualizar);
        Assert.Equal(1, repositorio.CantidadDeLlamadasAActualizar);
    }

    private static Rol CrearRolValido()
    {
        return new Rol
        {
            Nombre = "Administrador",
            Descripcion = "Acceso total al sistema"
        };
    }

    private sealed class RolRepositorioFalso : IRolRepositorio
    {
        public int IdGenerado { get; init; }
        public IReadOnlyList<Rol> Roles { get; init; } = Array.Empty<Rol>();
        public Rol RolActualizado { get; init; } = new();

        public Rol? RolRecibidoAlCrear { get; private set; }
        public int? IdRecibidoAlActualizar { get; private set; }
        public Rol? RolRecibidoAlActualizar { get; private set; }

        public int CantidadDeLlamadasACrear { get; private set; }
        public int CantidadDeLlamadasAObtenerTodos { get; private set; }
        public int CantidadDeLlamadasAActualizar { get; private set; }

        public IReadOnlyList<Rol> ObtenerTodos()
        {
            CantidadDeLlamadasAObtenerTodos++;
            return Roles;
        }

        public Rol? ObtenerPorId(int id)
        {
            return Roles.FirstOrDefault(rol => rol.Id == id);
        }

        public int Crear(Rol rol)
        {
            CantidadDeLlamadasACrear++;
            RolRecibidoAlCrear = rol;
            return IdGenerado;
        }

        public Rol Actualizar(int idRol, Rol rol)
        {
            CantidadDeLlamadasAActualizar++;
            IdRecibidoAlActualizar = idRol;
            RolRecibidoAlActualizar = rol;
            return RolActualizado;
        }
    }
}
