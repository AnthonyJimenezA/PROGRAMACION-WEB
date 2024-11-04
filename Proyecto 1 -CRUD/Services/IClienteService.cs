using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public interface IClienteService
    {
        Task<List<Cliente>> ObtenerClientes(); 
        Task<Cliente> ObtenerClientePorId(string identificacion); 
        Task<bool> AgregarCliente(Cliente cliente); 
        Task<bool> ActualizarCliente(Cliente cliente); 
        Task<bool> EliminarCliente(string identificacion); 
        Task<List<Cliente>> BuscarClientesPorCedula(string searchTerm); 
    }
}
