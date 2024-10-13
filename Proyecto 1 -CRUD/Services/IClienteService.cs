using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public interface IClienteService
    {
        List<Cliente> ObtenerClientes();
        Cliente ObtenerClientePorId(string identificacion);
        bool AgregarCliente(Cliente cliente);
        bool ActualizarCliente(Cliente cliente);
        bool EliminarCliente(string identificacion);
        List<Cliente> BuscarClientesPorCedula(string searchTerm);
    }
}
