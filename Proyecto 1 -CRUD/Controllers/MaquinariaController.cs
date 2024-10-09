using Microsoft.AspNetCore.Mvc;
using Proyecto_1__CRUD.Models;
using Proyecto_1__CRUD.Services;

namespace Proyecto_1__CRUD.Controllers
{
    public class MaquinariaController : Controller
    {
        private readonly IMaquinariaService _maquinariaService;
        private readonly ILogger<MaquinariaController> _logger;

        public MaquinariaController(IMaquinariaService maquinariaService, ILogger<MaquinariaController> logger)
        {
            _maquinariaService = maquinariaService;
            _logger = logger;
        }

        // GET: Maquinaria/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var maquinarias = await _maquinariaService.ObtenerMaquinariasAsync();
                return View(maquinarias);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de maquinarias.");
                return View("Error", new ErrorViewModel { Message = "Error al obtener la lista de maquinarias." });
            }
        }

        // GET: Maquinaria/Details/idInventario
        public async Task<IActionResult> Details(int idInventario)
        {
            if (idInventario <= 0)
            {
                _logger.LogWarning("Detalles de maquinaria solicitados con ID inválido.");
                return BadRequest("ID de maquinaria no válido.");
            }

            try
            {
                var maquinaria = await _maquinariaService.ObtenerMaquinariaPorIdAsync(idInventario);
                if (maquinaria == null)
                {
                    _logger.LogWarning($"Maquinaria con ID {idInventario} no encontrada.");
                    return NotFound($"Maquinaria con ID {idInventario} no encontrada.");
                }

                return View(maquinaria);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener detalles de maquinaria con ID {idInventario}.");
                return View("Error", new ErrorViewModel { Message = "Error al obtener detalles de la maquinaria." });
            }
        }

        // GET: Maquinaria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Maquinaria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Maquinaria maquinariaModel)
        {
            if (!ModelState.IsValid)
            {
                return View(maquinariaModel);
            }

            try
            {
                var maquinaria = new Maquinaria
                {
                    Descripcion = maquinariaModel.Descripcion,
                    Tipo = maquinariaModel.Tipo,
                    HorasUsoActuales = maquinariaModel.HorasUsoActuales,
                    HorasUsoMaximoDia = maquinariaModel.HorasUsoMaximoDia,
                    HorasUsoParaMantenimiento = maquinariaModel.HorasUsoParaMantenimiento
                };

                var resultado = await _maquinariaService.AgregarMaquinariaAsync(maquinaria);
                if (resultado)
                {
                    _logger.LogInformation($"Maquinaria creada exitosamente con ID {maquinaria.IdInventario}.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo crear la maquinaria.");
                    _logger.LogWarning($"Intento de crear maquinaria fallido.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear una nueva maquinaria.");
                ModelState.AddModelError("", "Ocurrió un error al crear la maquinaria.");
            }

            return View(maquinariaModel);
        }

        // GET: Maquinaria/Edit/idInventario
        public async Task<IActionResult> Edit(int idInventario)
        {
            if (idInventario <= 0)
            {
                _logger.LogWarning("Edición de maquinaria solicitada con ID inválido.");
                return BadRequest("ID de maquinaria no válido.");
            }

            try
            {
                var maquinaria = await _maquinariaService.ObtenerMaquinariaPorIdAsync(idInventario);
                if (maquinaria == null)
                {
                    _logger.LogWarning($"Maquinaria con ID {idInventario} no encontrada para edición.");
                    return NotFound($"Maquinaria con ID {idInventario} no encontrada.");
                }

                var maquinariaViewModel = new Maquinaria
                {
                    IdInventario = maquinaria.IdInventario,
                    Descripcion = maquinaria.Descripcion,
                    Tipo = maquinaria.Tipo,
                    HorasUsoActuales = maquinaria.HorasUsoActuales,
                    HorasUsoMaximoDia = maquinaria.HorasUsoMaximoDia,
                    HorasUsoParaMantenimiento = maquinaria.HorasUsoParaMantenimiento
                };

                return View(maquinariaViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la maquinaria con ID {idInventario} para edición.");
                return View("Error", new ErrorViewModel { Message = "Error al obtener la maquinaria para edición." });
            }
        }

        // POST: Maquinaria/Edit/idInventario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idInventario, Maquinaria maquinariaViewModel)
        {
            if (idInventario != maquinariaViewModel.IdInventario)
            {
                _logger.LogWarning($"ID de maquinaria no coincide en la edición: {idInventario} != {maquinariaViewModel.IdInventario}");
                return BadRequest("ID de maquinaria no coincide.");
            }

            if (!ModelState.IsValid)
            {
                return View(maquinariaViewModel);
            }

            try
            {
                var maquinaria = new Maquinaria
                {
                    IdInventario = maquinariaViewModel.IdInventario,
                    Descripcion = maquinariaViewModel.Descripcion,
                    Tipo = maquinariaViewModel.Tipo,
                    HorasUsoActuales = maquinariaViewModel.HorasUsoActuales,
                    HorasUsoMaximoDia = maquinariaViewModel.HorasUsoMaximoDia,
                    HorasUsoParaMantenimiento = maquinariaViewModel.HorasUsoParaMantenimiento
                };

                var resultado = await _maquinariaService.ActualizarMaquinariaAsync(maquinaria);
                if (resultado)
                {
                    _logger.LogInformation($"Maquinaria actualizada exitosamente con ID {maquinaria.IdInventario}.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo actualizar la maquinaria.");
                    _logger.LogWarning($"No se pudo actualizar la maquinaria con ID {maquinaria.IdInventario}.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar la maquinaria con ID {idInventario}.");
                ModelState.AddModelError("", "Ocurrió un error al actualizar la maquinaria.");
            }

            return View(maquinariaViewModel);
        }

        // GET: Maquinaria/Delete/idInventario
        public async Task<IActionResult> Delete(int idInventario)
        {
            if (idInventario <= 0)
            {
                _logger.LogWarning("Eliminación de maquinaria solicitada con ID inválido.");
                return BadRequest("ID de maquinaria no válido.");
            }

            try
            {
                var maquinaria = await _maquinariaService.ObtenerMaquinariaPorIdAsync(idInventario);
                if (maquinaria == null)
                {
                    _logger.LogWarning($"Maquinaria con ID {idInventario} no encontrada para eliminación.");
                    return NotFound($"Maquinaria con ID {idInventario} no encontrada.");
                }

                return View(maquinaria);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la maquinaria con ID {idInventario} para eliminación.");
                return View("Error", new ErrorViewModel { Message = "Error al obtener la maquinaria para eliminación." });
            }
        }

        // POST: Maquinaria/Delete/idInventario
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idInventario)
        {
            if (idInventario <= 0)
            {
                _logger.LogWarning("Eliminación de maquinaria confirmada con ID inválido.");
                return BadRequest("ID de maquinaria no válido.");
            }

            try
            {
                var resultado = await _maquinariaService.EliminarMaquinariaAsync(idInventario);
                if (resultado)
                {
                    _logger.LogInformation($"Maquinaria eliminada exitosamente con ID {idInventario}.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogWarning($"Maquinaria con ID {idInventario} no encontrada para eliminación.");
                    return NotFound($"Maquinaria con ID {idInventario} no encontrada.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar la maquinaria con ID {idInventario}.");
                return View("Error", new ErrorViewModel { Message = "Error al eliminar la maquinaria." });
            }
        }
    }
}
