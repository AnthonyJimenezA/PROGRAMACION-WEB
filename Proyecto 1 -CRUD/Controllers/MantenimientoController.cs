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

        // GET: Mantenimiento
        public IActionResult Index(string searchTerm = null)
        {
            try
            {
                List<Mantenimiento> mantenimientos = string.IsNullOrEmpty(searchTerm)
                    ? _mantenimientoService.ObtenerMantenimientos()
                    : _mantenimientoService.BuscarMantenimientosPorId(searchTerm);

                return View(mantenimientos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de mantenimientos.");
                return View("Error", new ErrorViewModel { Message = "Error al obtener la lista de mantenimientos." });
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
        public IActionResult Create(Mantenimiento mantenimiento)
        {
            if (ModelState.IsValid)
            {
                bool added = _mantenimientoService.AgregarMantenimiento(mantenimiento);
                if (added)
                {
                    TempData["SuccessMessage"] = "Mantenimiento creado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Ya existe un mantenimiento con ese ID.");
                }
            }
            return View(mantenimiento);
        }

        // GET: Mantenimiento/Edit/idMantenimiento
        public IActionResult Edit(int idMantenimiento)
        {
            if (idMantenimiento == 0)
            {
                return NotFound();
            }

            Mantenimiento mantenimiento = _mantenimientoService.ObtenerMantenimientoPorId(idMantenimiento);
            if (mantenimiento == null)
            {
                return NotFound();
            }
            return View(mantenimiento);
        }

        // POST: Mantenimiento/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Mantenimiento mantenimiento)
        {
            if (ModelState.IsValid)
            {
                bool updated = _mantenimientoService.ActualizarMantenimiento(mantenimiento);
                if (updated)
                {
                    TempData["SuccessMessage"] = "Mantenimiento actualizado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo actualizar el mantenimiento.");
                }
            }
            return View(mantenimiento);
        }

        // GET: Mantenimiento/Delete/idMantenimiento
        public IActionResult Delete(int idMantenimiento)
        {
            if (idMantenimiento == 0)
            {
                return NotFound();
            }

            Mantenimiento mantenimiento = _mantenimientoService.ObtenerMantenimientoPorId(idMantenimiento);
            if (mantenimiento == null)
            {
                return NotFound();
            }

            return View(mantenimiento);
        }

        // POST: Mantenimiento/DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int idMantenimiento)
        {
            bool deleted = _mantenimientoService.EliminarMantenimiento(idMantenimiento);
            if (deleted)
            {
                TempData["SuccessMessage"] = "Mantenimiento eliminado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
