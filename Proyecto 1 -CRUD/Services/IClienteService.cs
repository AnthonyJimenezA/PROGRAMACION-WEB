using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public interface IClienteService
    {
        Task<List<Cliente>> ObtenerClientesAsync();
        Task<Cliente> ObtenerClientePorIdAsync(string identificacion);
        Task<bool> AgregarClienteAsync(Cliente cliente);
        Task<bool> ActualizarClienteAsync(Cliente cliente);
        Task<bool> EliminarClienteAsync(string identificacion);
    }
}
