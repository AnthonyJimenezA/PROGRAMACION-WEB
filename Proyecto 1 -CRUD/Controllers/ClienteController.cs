using Microsoft.AspNetCore.Mvc;
using Proyecto_1__CRUD.Models;
using Proyecto_1__CRUD.Services;

namespace Proyecto_1__CRUD.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(IClienteService clienteService, ILogger<ClienteController> logger)
        {
            _clienteService = clienteService;
            _logger = logger;
        }

        // GET: Cliente
        public IActionResult Index(string searchTerm = null)
        {
            try
            {
                List<Cliente> clientes = string.IsNullOrEmpty(searchTerm)
                    ? _clienteService.ObtenerClientes()
                    : _clienteService.BuscarClientesPorCedula(searchTerm);

                return View(clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de clientes.");
                return View("Error", new ErrorViewModel { Message = "Error al obtener la lista de clientes." });
            }
        }

        // GET: Cliente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                bool added = _clienteService.AgregarCliente(cliente);
                if (added)
                {
                    TempData["SuccessMessage"] = "Cliente creado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Ya existe un cliente con esa identificación.");
                }
            }
            return View(cliente);
        }

        // GET: Cliente/Edit/identificacion
        public IActionResult Edit(string identificacion)
        {
            if (identificacion == null)
            {
                return NotFound();
            }

            Cliente cliente = _clienteService.ObtenerClientePorId(identificacion);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                bool updated = _clienteService.ActualizarCliente(cliente);
                if (updated)
                {
                    TempData["SuccessMessage"] = "Cliente actualizado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo actualizar el cliente.");
                }
            }
            return View(cliente);
        }

        // GET: Cliente/Delete/identificacion
        public IActionResult Delete(string identificacion)
        {
            if (identificacion == null)
            {
                return NotFound();
            }

            Cliente cliente = _clienteService.ObtenerClientePorId(identificacion);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Cliente/DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string identificacion)
        {
            bool deleted = _clienteService.EliminarCliente(identificacion);
            if (deleted)
            {
                TempData["SuccessMessage"] = "Cliente eliminado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
