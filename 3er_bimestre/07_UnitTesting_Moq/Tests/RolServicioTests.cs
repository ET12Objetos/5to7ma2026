using Aplicacion.Entidades;
using Aplicacion.Repositorios;
using Aplicacion.Servicios;
using Moq;

namespace Tests;

public class RolServicioTests
{
    [Fact]
    public void Crear_RolValido_DevuelveIdGeneradoPorRepositorio()
    {
        var rol = CrearRolValido();
        var repositorio = new Mock<IRolRepositorio>(MockBehavior.Strict);
        repositorio
            .Setup(r => r.Crear(rol))
            .Returns(15);
        var servicio = new RolServicio(repositorio.Object);

        var resultado = servicio.Crear(rol);

        Assert.Equal(15, resultado);
        repositorio.Verify(r => r.Crear(rol), Times.Once);
        repositorio.VerifyNoOtherCalls();
    }

    [Fact]
    public void ObtenerTodos_CuandoHayRoles_DevuelveLosRolesDelRepositorio()
    {
        var roles = new List<Rol>
        {
            new() { Id = 1, Nombre = "Administrador", Descripcion = "Acceso total" },
            new() { Id = 2, Nombre = "Operador", Descripcion = "Acceso operativo" }
        };
        var repositorio = new Mock<IRolRepositorio>(MockBehavior.Strict);
        repositorio
            .Setup(r => r.ObtenerTodos())
            .Returns(roles);
        var servicio = new RolServicio(repositorio.Object);

        var resultado = servicio.ObtenerTodos();

        Assert.Equal(roles, resultado);
        Assert.NotSame(roles, resultado);
        repositorio.Verify(r => r.ObtenerTodos(), Times.Once);
        repositorio.VerifyNoOtherCalls();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Actualizar_IdInvalido_LanzaArgumentExceptionYNoInvocaRepositorio(int idRol)
    {
        var repositorio = new Mock<IRolRepositorio>(MockBehavior.Strict);
        var servicio = new RolServicio(repositorio.Object);

        var excepcion = Assert.Throws<ArgumentException>(
            () => servicio.Actualizar(idRol, CrearRolValido()));

        Assert.Equal("El Id del rol debe ser mayor a cero.", excepcion.Message);
        repositorio.Verify(
            r => r.Actualizar(It.IsAny<int>(), It.IsAny<Rol>()),
            Times.Never);
        repositorio.VerifyNoOtherCalls();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Actualizar_NombreInvalido_LanzaArgumentExceptionYNoInvocaRepositorio(string nombre)
    {
        var repositorio = new Mock<IRolRepositorio>(MockBehavior.Strict);
        var servicio = new RolServicio(repositorio.Object);
        var rol = CrearRolValido();
        rol.Nombre = nombre;

        var excepcion = Assert.Throws<ArgumentException>(
            () => servicio.Actualizar(1, rol));

        Assert.Equal("El nombre del rol es obligatorio.", excepcion.Message);
        repositorio.Verify(
            r => r.Actualizar(It.IsAny<int>(), It.IsAny<Rol>()),
            Times.Never);
        repositorio.VerifyNoOtherCalls();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Actualizar_DescripcionInvalida_LanzaArgumentExceptionYNoInvocaRepositorio(
        string descripcion)
    {
        var repositorio = new Mock<IRolRepositorio>(MockBehavior.Strict);
        var servicio = new RolServicio(repositorio.Object);
        var rol = CrearRolValido();
        rol.Descripcion = descripcion;

        var excepcion = Assert.Throws<ArgumentException>(
            () => servicio.Actualizar(1, rol));

        Assert.Equal("La descripcion del rol es obligatoria.", excepcion.Message);
        repositorio.Verify(
            r => r.Actualizar(It.IsAny<int>(), It.IsAny<Rol>()),
            Times.Never);
        repositorio.VerifyNoOtherCalls();
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
        var cambios = CrearRolValido();
        var repositorio = new Mock<IRolRepositorio>(MockBehavior.Strict);
        repositorio
            .Setup(r => r.Actualizar(7, cambios))
            .Returns(rolActualizado);
        var servicio = new RolServicio(repositorio.Object);

        var resultado = servicio.Actualizar(7, cambios);

        Assert.Same(rolActualizado, resultado);
        repositorio.Verify(r => r.Actualizar(7, cambios), Times.Once);
        repositorio.VerifyNoOtherCalls();
    }

    private static Rol CrearRolValido()
    {
        return new Rol
        {
            Nombre = "Administrador",
            Descripcion = "Acceso total al sistema"
        };
    }
}
