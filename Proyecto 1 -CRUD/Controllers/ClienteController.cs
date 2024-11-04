using Microsoft.AspNetCore.Mvc;
using Proyecto_1__CRUD.Models;
using Proyecto_1__CRUD.Services;

namespace Proyecto_1__CRUD.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService; // Servicio para manejar la lógica de clientes
        private readonly ILogger<ClienteController> _logger; // Logger para registrar errores

        public ClienteController(IClienteService clienteService, ILogger<ClienteController> logger)
        {
            _clienteService = clienteService;
            _logger = logger;
        }

        // GET: Cliente
        public async Task<IActionResult> Index(string searchTerm = null)
        {
            try
            {
                // Obtiene la lista de clientes, filtrando si hay un término de búsqueda
                List<Cliente> clientes = string.IsNullOrEmpty(searchTerm)
                    ? await _clienteService.ObtenerClientes()
                    : await _clienteService.BuscarClientesPorCedula(searchTerm);

                return View(clientes); // Retorna la vista con la lista de clientes
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de clientes."); // Registra el error
                return View("Error", new ErrorViewModel { Message = "Error al obtener la lista de clientes." }); // Retorna vista de error
            }
        }

        // GET: Cliente/Create
        public IActionResult Create()
        {
            return View(); // Retorna la vista para crear un nuevo cliente
        }

        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (ModelState.IsValid) // Verifica que el modelo sea válido
            {
                // Intenta agregar el nuevo cliente
                bool added = await _clienteService.AgregarCliente(cliente);
                if (added)
                {
                    TempData["SuccessMessage"] = "Cliente creado exitosamente."; // Mensaje de éxito
                    return RedirectToAction(nameof(Index)); // Redirige a la lista de clientes
                }
                else
                {
                    ModelState.AddModelError("", "Ya existe un cliente con esa identificación."); // Mensaje de error
                }
            }
            return View(cliente); // Retorna la vista con errores
        }

        // GET: Cliente/Edit/identificacion
        public async Task<IActionResult> Edit(string identificacion)
        {
            if (identificacion == null) // Verifica si la identificación es nula
            {
                return NotFound(); // Retorna error 404
            }

            // Obtiene el cliente por identificación
            Cliente cliente = await _clienteService.ObtenerClientePorId(identificacion);
            if (cliente == null) // Verifica si el cliente no fue encontrado
            {
                return NotFound(); // Retorna error 404
            }
            return View(cliente); // Retorna la vista para editar el cliente
        }

        // POST: Cliente/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Cliente cliente)
        {
            if (ModelState.IsValid) // Verifica que el modelo sea válido
            {
                // Intenta actualizar el cliente
                bool updated = await _clienteService.ActualizarCliente(cliente);
                if (updated)
                {
                    TempData["SuccessMessage"] = "Cliente actualizado exitosamente."; // Mensaje de éxito
                    return RedirectToAction(nameof(Index)); // Redirige a la lista de clientes
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo actualizar el cliente."); // Mensaje de error
                }
            }
            return View(cliente); // Retorna la vista con errores
        }

        // GET: Cliente/Delete/identificacion
        public async Task<IActionResult> Delete(string identificacion)
        {
            if (identificacion == null) // Verifica si la identificación es nula
            {
                return NotFound(); // Retorna error 404
            }

            // Obtiene el cliente por identificación
            Cliente cliente = await _clienteService.ObtenerClientePorId(identificacion);
            if (cliente == null) // Verifica si el cliente no fue encontrado
            {
                return NotFound(); // Retorna error 404
            }

            return View(cliente); // Retorna vista de confirmación de eliminación
        }

        // POST: Cliente/DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string identificacion)
        {
            // Intenta eliminar el cliente
            bool deleted = await _clienteService.EliminarCliente(identificacion);
            if (deleted)
            {
                TempData["SuccessMessage"] = "Cliente eliminado exitosamente."; // Mensaje de éxito
                return RedirectToAction(nameof(Index)); // Redirige a la lista de clientes
            }
            else
            {
                return NotFound(); // Retorna error 404 si no se encuentra
            }
        }
    }
}
