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

        // GET: Cliente/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var clientes = await _clienteService.ObtenerClientesAsync();
                return View(clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de clientes.");
                return View("Error", new ErrorViewModel { Message = "Error al obtener la lista de clientes." });
            }
        }

        // GET: Cliente/Details/identificacion
        public async Task<IActionResult> Details(string identificacion)
        {
            if (string.IsNullOrEmpty(identificacion))
            {
                _logger.LogWarning("Detalles del cliente solicitados sin identificación.");
                return BadRequest("Identificación no proporcionada.");
            }

            try
            {
                var cliente = await _clienteService.ObtenerClientePorIdAsync(identificacion);
                if (cliente == null)
                {
                    _logger.LogWarning($"Cliente con identificación {identificacion} no encontrado.");
                    return NotFound($"Cliente con identificación {identificacion} no encontrado.");
                }

                return View(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener detalles del cliente con identificación {identificacion}.");
                return View("Error", new ErrorViewModel { Message = "Error al obtener detalles del cliente." });
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
        public async Task<IActionResult> Create(Cliente clienteViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(clienteViewModel);
            }

            try
            {
                var cliente = new Cliente
                {
                    Identificacion = clienteViewModel.Identificacion,
                    NombreCompleto = clienteViewModel.NombreCompleto,
                    Provincia = clienteViewModel.Provincia,
                    Canton = clienteViewModel.Canton,
                    Distrito = clienteViewModel.Distrito,
                    DireccionExacta = clienteViewModel.DireccionExacta,
                    PreferenciaInvierno = clienteViewModel.PreferenciaInvierno,
                    PreferenciaVerano = clienteViewModel.PreferenciaVerano
                };

                var resultado = await _clienteService.AgregarClienteAsync(cliente);
                if (resultado)
                {
                    _logger.LogInformation($"Cliente creado exitosamente con identificación {cliente.Identificacion}.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Ya existe un cliente con esa identificación.");
                    _logger.LogWarning($"Intento de crear cliente con identificación existente: {cliente.Identificacion}.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un nuevo cliente.");
                ModelState.AddModelError("", "Ocurrió un error al crear el cliente.");
            }

            return View(clienteViewModel);
        }

        // GET: Cliente/Edit/identificacion
        public async Task<IActionResult> Edit(string identificacion)
        {
            if (string.IsNullOrEmpty(identificacion))
            {
                _logger.LogWarning("Edición de cliente solicitada sin identificación.");
                return BadRequest("Identificación no proporcionada.");
            }

            try
            {
                var cliente = await _clienteService.ObtenerClientePorIdAsync(identificacion);
                if (cliente == null)
                {
                    _logger.LogWarning($"Cliente con identificación {identificacion} no encontrado para edición.");
                    return NotFound($"Cliente con identificación {identificacion} no encontrado.");
                }

                var clienteViewModel = new Cliente
                {
                    Identificacion = cliente.Identificacion,
                    NombreCompleto = cliente.NombreCompleto,
                    Provincia = cliente.Provincia,
                    Canton = cliente.Canton,
                    Distrito = cliente.Distrito,
                    DireccionExacta = cliente.DireccionExacta,
                    PreferenciaInvierno = cliente.PreferenciaInvierno,
                    PreferenciaVerano = cliente.PreferenciaVerano
                };

                return View(clienteViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el cliente con identificación {identificacion} para edición.");
                return View("Error", new ErrorViewModel { Message = "Error al obtener el cliente para edición." });
            }
        }

        // POST: Cliente/Edit/identificacion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string identificacion, Cliente clienteModel)
        {
            if (identificacion != clienteModel.Identificacion)
            {
                _logger.LogWarning($"Identificaciones no coinciden en la edición: {identificacion} != {clienteModel.Identificacion}");
                return BadRequest("Identificación no coincide.");
            }

            if (!ModelState.IsValid)
            {
                return View(clienteModel);
            }

            try
            {
                var cliente = new Cliente
                {
                    Identificacion = clienteModel.Identificacion,
                    NombreCompleto = clienteModel.NombreCompleto,
                    Provincia = clienteModel.Provincia,
                    Canton = clienteModel.Canton,
                    Distrito = clienteModel.Distrito,
                    DireccionExacta = clienteModel.DireccionExacta,
                    PreferenciaInvierno = clienteModel.PreferenciaInvierno,
                    PreferenciaVerano = clienteModel.PreferenciaVerano
                };

                var resultado = await _clienteService.ActualizarClienteAsync(cliente);
                if (resultado)
                {
                    _logger.LogInformation($"Cliente actualizado exitosamente con identificación {cliente.Identificacion}.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo actualizar el cliente.");
                    _logger.LogWarning($"No se pudo actualizar el cliente con identificación {cliente.Identificacion}.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el cliente con identificación {identificacion}.");
                ModelState.AddModelError("", "Ocurrió un error al actualizar el cliente.");
            }

            return View(clienteModel);
        }

        // GET: Cliente/Delete/identificacion
        public async Task<IActionResult> Delete(string identificacion)
        {
            if (string.IsNullOrEmpty(identificacion))
            {
                _logger.LogWarning("Eliminación de cliente solicitada sin identificación.");
                return BadRequest("Identificación no proporcionada.");
            }

            try
            {
                var cliente = await _clienteService.ObtenerClientePorIdAsync(identificacion);
                if (cliente == null)
                {
                    _logger.LogWarning($"Cliente con identificación {identificacion} no encontrado para eliminación.");
                    return NotFound($"Cliente con identificación {identificacion} no encontrado.");
                }

                return View(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el cliente con identificación {identificacion} para eliminación.");
                return View("Error", new ErrorViewModel { Message = "Error al obtener el cliente para eliminación." });
            }
        }

        // POST: Cliente/Delete/identificacion
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string identificacion)
        {
            if (string.IsNullOrEmpty(identificacion))
            {
                _logger.LogWarning("Eliminación de cliente confirmada sin identificación.");
                return BadRequest("Identificación no proporcionada.");
            }

            try
            {
                var resultado = await _clienteService.EliminarClienteAsync(identificacion);
                if (resultado)
                {
                    _logger.LogInformation($"Cliente eliminado exitosamente con identificación {identificacion}.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogWarning($"Cliente con identificación {identificacion} no encontrado para eliminación.");
                    return NotFound($"Cliente con identificación {identificacion} no encontrado.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el cliente con identificación {identificacion}.");
                return View("Error", new ErrorViewModel { Message = "Error al eliminar el cliente." });
            }
        }
    }
}
