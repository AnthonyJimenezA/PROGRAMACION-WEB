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

        // GET: Maquinaria
        public IActionResult Index(string searchTerm = null)
        {
            try
            {
                List<Maquinaria> maquinarias = string.IsNullOrEmpty(searchTerm)
                    ? _maquinariaService.ObtenerMaquinarias()
                    : _maquinariaService.BuscarMaquinariasPorId(searchTerm);

                return View(maquinarias);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de maquinarias.");
                return View("Error", new ErrorViewModel { Message = "Error al obtener la lista de maquinarias." });
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
        public IActionResult Create(Maquinaria maquinaria)
        {
            if (ModelState.IsValid)
            {
                bool added = _maquinariaService.AgregarMaquinaria(maquinaria);
                if (added)
                {
                    TempData["SuccessMessage"] = "Maquinaria creada exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Ya existe una maquinaria con ese ID de inventario.");
                }
            }
            return View(maquinaria);
        }

        // GET: Maquinaria/Edit/idInventario
        public IActionResult Edit(int idInventario)
        {
            if (idInventario == 0)
            {
                return NotFound();
            }

            Maquinaria maquinaria = _maquinariaService.ObtenerMaquinariaPorId(idInventario);
            if (maquinaria == null)
            {
                return NotFound();
            }
            return View(maquinaria);
        }

        // POST: Maquinaria/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Maquinaria maquinaria)
        {
            if (ModelState.IsValid)
            {
                bool updated = _maquinariaService.ActualizarMaquinaria(maquinaria);
                if (updated)
                {
                    TempData["SuccessMessage"] = "Maquinaria actualizada exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo actualizar la maquinaria.");
                }
            }
            return View(maquinaria);
        }

        // GET: Maquinaria/Delete/idInventario
        public IActionResult Delete(int idInventario)
        {
            if (idInventario == 0)
            {
                return NotFound();
            }

            Maquinaria maquinaria = _maquinariaService.ObtenerMaquinariaPorId(idInventario);
            if (maquinaria == null)
            {
                return NotFound();
            }

            return View(maquinaria);
        }

        // POST: Maquinaria/DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int idInventario)
        {
            bool deleted = _maquinariaService.EliminarMaquinaria(idInventario);
            if (deleted)
            {
                TempData["SuccessMessage"] = "Maquinaria eliminada exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
