using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public class ClienteService : IClienteService
    {
        // Lista estática para mantener la persistencia de los clientes en la aplicación
        private static readonly List<Cliente> _clientes = new List<Cliente>();

        public List<Cliente> ObtenerClientes()
        {
            return _clientes; // Retorna la lista completa de clientes
        }

        public Cliente ObtenerClientePorId(string identificacion)
        {
            return _clientes.FirstOrDefault(c => c.Identificacion == identificacion); // Busca el cliente por identificación
        }

        public bool AgregarCliente(Cliente cliente)
        {
            // Verificar si ya existe un cliente con la misma identificación
            if (_clientes.Any(c => c.Identificacion == cliente.Identificacion))
            {
                return false; // Si existe, no lo agrega
            }

            _clientes.Add(cliente); // Agregar el cliente a la lista
            return true; // Cliente agregado correctamente
        }

        public bool ActualizarCliente(Cliente cliente)
        {
            var clienteExistente = _clientes.FirstOrDefault(c => c.Identificacion == cliente.Identificacion);
            if (clienteExistente == null)
            {
                return false; // Cliente no encontrado
            }

            // Actualizar los datos del cliente existente
            clienteExistente.NombreCompleto = cliente.NombreCompleto;
            clienteExistente.Provincia = cliente.Provincia;
            clienteExistente.Canton = cliente.Canton;
            clienteExistente.Distrito = cliente.Distrito;
            clienteExistente.DireccionExacta = cliente.DireccionExacta;
            clienteExistente.PreferenciaInvierno = cliente.PreferenciaInvierno;
            clienteExistente.PreferenciaVerano = cliente.PreferenciaVerano;

            return true; // Cliente actualizado correctamente
        }

        public bool EliminarCliente(string identificacion)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Identificacion == identificacion);
            if (cliente == null)
            {
                return false; // Cliente no encontrado
            }

            _clientes.Remove(cliente); // Eliminar el cliente de la lista
            return true; // Cliente eliminado correctamente
        }

        public List<Cliente> BuscarClientesPorCedula(string searchTerm)
        {
            return _clientes
                .Where(c => c.Identificacion.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase))
                .ToList(); // Buscar clientes por término en la identificación
        }
    }
}
