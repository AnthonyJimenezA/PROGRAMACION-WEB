using Microsoft.AspNetCore.Mvc;
using Proyecto_1__CRUD.Models;
using Proyecto_1__CRUD.Services;

namespace Proyecto_1__CRUD.Controllers
{
    public class MantenimientoController : Controller
    {
        private readonly IMantenimientoService _mantenimientoService;
        private readonly ILogger<MantenimientoController> _logger;

        public MantenimientoController(IMantenimientoService mantenimientoService, ILogger<MantenimientoController> logger)
        {
            _mantenimientoService = mantenimientoService;
            _logger = logger;
        }

        // GET: Mantenimiento/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var mantenimientos = await _mantenimientoService.ObtenerMantenimientosAsync();
                return View(mantenimientos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de mantenimientos.");
                return View("Error", new ErrorViewModel { Message = "Error al obtener la lista de mantenimientos." });
            }
        }

        // GET: Mantenimiento/Details/idMantenimiento
        public async Task<IActionResult> Details(int idMantenimiento)
        {
            if (idMantenimiento <= 0)
            {
                _logger.LogWarning("Detalles del mantenimiento solicitados con ID inválido.");
                return BadRequest("ID de mantenimiento no válido.");
            }

            try
            {
                var mantenimiento = await _mantenimientoService.ObtenerMantenimientoPorIdAsync(idMantenimiento);
                if (mantenimiento == null)
                {
                    _logger.LogWarning($"Mantenimiento con ID {idMantenimiento} no encontrado.");
                    return NotFound($"Mantenimiento con ID {idMantenimiento} no encontrado.");
                }

                return View(mantenimiento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener detalles del mantenimiento con ID {idMantenimiento}.");
                return View("Error", new ErrorViewModel { Message = "Error al obtener detalles del mantenimiento." });
            }
        }

        // GET: Mantenimiento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mantenimiento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Mantenimiento mantenimientoModel)
        {
            if (!ModelState.IsValid)
            {
                return View(mantenimientoModel);
            }

            try
            {
                var mantenimiento = new Mantenimiento
                {
                    IdCliente = mantenimientoModel.IdCliente,
                    FechaEjecutado = mantenimientoModel.FechaEjecutado,
                    FechaAgendado = mantenimientoModel.FechaAgendado,
                    M2Propiedad = mantenimientoModel.M2Propiedad,
                    M2CercaViva = mantenimientoModel.M2CercaViva,
                    DiasSinChapia = mantenimientoModel.DiasSinChapia, // Autocalculado
                    FechaSiguienteChapia = mantenimientoModel.FechaSiguienteChapia, // Autocalculado
                    TipoZacate = mantenimientoModel.TipoZacate,
                    ProductoAplicado = mantenimientoModel.ProductoAplicado,
                    Producto = mantenimientoModel.Producto,
                    CostoChapiaPorM2 = mantenimientoModel.CostoChapiaPorM2,
                    CostoAplicacionProductoPorM2 = mantenimientoModel.CostoAplicacionProductoPorM2,
                    IVA = mantenimientoModel.IVA,
                    Estado = mantenimientoModel.Estado
                };

                var resultado = await _mantenimientoService.AgregarMantenimientoAsync(mantenimiento);
                if (resultado)
                {
                    _logger.LogInformation($"Mantenimiento creado exitosamente con ID {mantenimiento.IdMantenimiento}.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo crear el mantenimiento.");
                    _logger.LogWarning($"Intento de crear mantenimiento fallido.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un nuevo mantenimiento.");
                ModelState.AddModelError("", "Ocurrió un error al crear el mantenimiento.");
            }

            return View(mantenimientoModel);
        }

        // GET: Mantenimiento/Edit/idMantenimiento
        public async Task<IActionResult> Edit(int idMantenimiento)
        {
            if (idMantenimiento <= 0)
            {
                _logger.LogWarning("Edición de mantenimiento solicitada con ID inválido.");
                return BadRequest("ID de mantenimiento no válido.");
            }

            try
            {
                var mantenimiento = await _mantenimientoService.ObtenerMantenimientoPorIdAsync(idMantenimiento);
                if (mantenimiento == null)
                {
                    _logger.LogWarning($"Mantenimiento con ID {idMantenimiento} no encontrado para edición.");
                    return NotFound($"Mantenimiento con ID {idMantenimiento} no encontrado.");
                }

                var mantenimientoViewModel = new Mantenimiento
                {
                    IdMantenimiento = mantenimiento.IdMantenimiento,
                    IdCliente = mantenimiento.IdCliente,
                    FechaEjecutado = mantenimiento.FechaEjecutado,
                    FechaAgendado = mantenimiento.FechaAgendado,
                    M2Propiedad = mantenimiento.M2Propiedad,
                    M2CercaViva = mantenimiento.M2CercaViva,
                    DiasSinChapia = mantenimiento.DiasSinChapia,
                    FechaSiguienteChapia = mantenimiento.FechaSiguienteChapia,
                    TipoZacate = mantenimiento.TipoZacate,
                    ProductoAplicado = mantenimiento.ProductoAplicado,
                    Producto = mantenimiento.Producto,
                    CostoChapiaPorM2 = mantenimiento.CostoChapiaPorM2,
                    CostoAplicacionProductoPorM2 = mantenimiento.CostoAplicacionProductoPorM2,
                    IVA = mantenimiento.IVA,
                    Estado = mantenimiento.Estado
                };

                return View(mantenimientoViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el mantenimiento con ID {idMantenimiento} para edición.");
                return View("Error", new ErrorViewModel { Message = "Error al obtener el mantenimiento para edición." });
            }
        }

        // POST: Mantenimiento/Edit/idMantenimiento
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idMantenimiento, Mantenimiento mantenimientoViewModel)
        {
            if (idMantenimiento != mantenimientoViewModel.IdMantenimiento)
            {
                _logger.LogWarning($"IDs de mantenimiento no coinciden en la edición: {idMantenimiento} != {mantenimientoViewModel.IdMantenimiento}");
                return BadRequest("ID de mantenimiento no coincide.");
            }

            if (!ModelState.IsValid)
            {
                return View(mantenimientoViewModel);
            }

            try
            {
                var mantenimiento = new Mantenimiento
                {
                    IdMantenimiento = mantenimientoViewModel.IdMantenimiento,
                    IdCliente = mantenimientoViewModel.IdCliente,
                    FechaEjecutado = mantenimientoViewModel.FechaEjecutado,
                    FechaAgendado = mantenimientoViewModel.FechaAgendado,
                    M2Propiedad = mantenimientoViewModel.M2Propiedad,
                    M2CercaViva = mantenimientoViewModel.M2CercaViva,
                    DiasSinChapia = mantenimientoViewModel.DiasSinChapia,
                    FechaSiguienteChapia = mantenimientoViewModel.FechaSiguienteChapia,
                    TipoZacate = mantenimientoViewModel.TipoZacate,
                    ProductoAplicado = mantenimientoViewModel.ProductoAplicado,
                    Producto = mantenimientoViewModel.Producto,
                    CostoChapiaPorM2 = mantenimientoViewModel.CostoChapiaPorM2,
                    CostoAplicacionProductoPorM2 = mantenimientoViewModel.CostoAplicacionProductoPorM2,
                    IVA = mantenimientoViewModel.IVA,
                    Estado = mantenimientoViewModel.Estado
                };

                var resultado = await _mantenimientoService.ActualizarMantenimientoAsync(mantenimiento);
                if (resultado)
                {
                    _logger.LogInformation($"Mantenimiento actualizado exitosamente con ID {mantenimiento.IdMantenimiento}.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo actualizar el mantenimiento.");
                    _logger.LogWarning($"No se pudo actualizar el mantenimiento con ID {mantenimiento.IdMantenimiento}.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el mantenimiento con ID {idMantenimiento}.");
                ModelState.AddModelError("", "Ocurrió un error al actualizar el mantenimiento.");
            }

            return View(mantenimientoViewModel);
        }

        // GET: Mantenimiento/Delete/idMantenimiento
        public async Task<IActionResult> Delete(int idMantenimiento)
        {
            if (idMantenimiento <= 0)
            {
                _logger.LogWarning("Eliminación de mantenimiento solicitada con ID inválido.");
                return BadRequest("ID de mantenimiento no válido.");
            }

            try
            {
                var mantenimiento = await _mantenimientoService.ObtenerMantenimientoPorIdAsync(idMantenimiento);
                if (mantenimiento == null)
                {
                    _logger.LogWarning($"Mantenimiento con ID {idMantenimiento} no encontrado para eliminación.");
                    return NotFound($"Mantenimiento con ID {idMantenimiento} no encontrado.");
                }

                return View(mantenimiento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el mantenimiento con ID {idMantenimiento} para eliminación.");
                return View("Error", new ErrorViewModel { Message = "Error al obtener el mantenimiento para eliminación." });
            }
        }

        // POST: Mantenimiento/Delete/idMantenimiento
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idMantenimiento)
        {
            if (idMantenimiento <= 0)
            {
                _logger.LogWarning("Eliminación de mantenimiento confirmada con ID inválido.");
                return BadRequest("ID de mantenimiento no válido.");
            }

            try
            {
                var resultado = await _mantenimientoService.EliminarMantenimientoAsync(idMantenimiento);
                if (resultado)
                {
                    _logger.LogInformation($"Mantenimiento eliminado exitosamente con ID {idMantenimiento}.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogWarning($"Mantenimiento con ID {idMantenimiento} no encontrado para eliminación.");
                    return NotFound($"Mantenimiento con ID {idMantenimiento} no encontrado.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el mantenimiento con ID {idMantenimiento}.");
                return View("Error", new ErrorViewModel { Message = "Error al eliminar el mantenimiento." });
            }
        }
    }
}
