using Aplicacion.Entidades;
using Aplicacion.Repositorios;

namespace Aplicacion.Servicios;

public sealed class ClienteServicio(IClienteRepositorio clienteRepositorio)
{
    private readonly IClienteRepositorio _clienteRepositorio = clienteRepositorio ?? throw new ArgumentNullException(nameof(clienteRepositorio));

    public IReadOnlyList<Cliente> ObtenerTodos()
    {
        return _clienteRepositorio.ObtenerTodos();
    }

    public int Crear(Cliente cliente)
    {
        ArgumentNullException.ThrowIfNull(cliente);
        ValidarTexto(cliente.Nombre, "El nombre del cliente es obligatorio.");
        ValidarTexto(cliente.Apellido, "El apellido del cliente es obligatorio.");
        ValidarTexto(cliente.Telefono, "El telefono del cliente es obligatorio.");
        ValidarTexto(cliente.Email, "El email del cliente es obligatorio.");

        return _clienteRepositorio.Crear(cliente);
    }

    private static void ValidarTexto(string valor, string mensaje)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new ArgumentException(mensaje);
        }
    }
}
