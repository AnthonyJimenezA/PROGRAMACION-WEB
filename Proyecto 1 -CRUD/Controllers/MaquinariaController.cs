using Microsoft.AspNetCore.Mvc;
using Proyecto_1__CRUD.Models;
using Proyecto_1__CRUD.Services;

namespace Proyecto_1__CRUD.Controllers
{
    public class MaquinariaController : Controller
    {
        private readonly IMaquinariaService _maquinariaService; // Servicio para manejar la lógica de maquinaria
        private readonly ILogger<MaquinariaController> _logger; // Logger para registrar errores

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
                // Obtiene la lista de maquinarias, filtrando si hay un término de búsqueda
                var maquinarias = string.IsNullOrEmpty(searchTerm)
                    ? _maquinariaService.ObtenerMaquinarias()
                    : _maquinariaService.BuscarMaquinariasPorId(searchTerm);

                return View(maquinarias); // Retorna la vista con la lista de maquinarias
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de maquinarias."); // Registra el error
                return View("Error", new ErrorViewModel { Message = "Error al obtener la lista de maquinarias." }); // Retorna vista de error
            }
        }

        // GET: Maquinaria/Create
        public IActionResult Create() => View(); // Retorna la vista para crear nueva maquinaria

        // POST: Maquinaria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Maquinaria maquinaria)
        {
            if (ModelState.IsValid) // Verifica que el modelo sea válido
            {
                // Intenta agregar la nueva maquinaria
                if (_maquinariaService.AgregarMaquinaria(maquinaria))
                {
                    TempData["SuccessMessage"] = "Maquinaria creada exitosamente."; // Mensaje de éxito
                    return RedirectToAction(nameof(Index)); // Redirige a la lista de maquinarias
                }
                ModelState.AddModelError("", "Ya existe una maquinaria con ese ID de inventario."); // Mensaje de error
            }
            return View(maquinaria); // Retorna la vista con errores
        }

        // GET: Maquinaria/Edit/idInventario
        public IActionResult Edit(int idInventario)
        {
            var maquinaria = _maquinariaService.ObtenerMaquinariaPorId(idInventario); // Obtiene la maquinaria por ID
            return maquinaria == null ? NotFound() : View(maquinaria); // Retorna la vista para editar o error 404
        }

        // POST: Maquinaria/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Maquinaria maquinaria)
        {
            if (ModelState.IsValid) // Verifica que el modelo sea válido
            {
                // Intenta actualizar la maquinaria
                if (_maquinariaService.ActualizarMaquinaria(maquinaria))
                {
                    TempData["SuccessMessage"] = "Maquinaria actualizada exitosamente."; // Mensaje de éxito
                    return RedirectToAction(nameof(Index)); // Redirige a la lista de maquinarias
                }
                ModelState.AddModelError("", "No se pudo actualizar la maquinaria."); // Mensaje de error
            }
            return View(maquinaria); // Retorna la vista con errores
        }

        // GET: Maquinaria/Delete/idInventario
        public IActionResult Delete(int idInventario)
        {
            var maquinaria = _maquinariaService.ObtenerMaquinariaPorId(idInventario); // Obtiene la maquinaria por ID
            return maquinaria == null ? NotFound() : View(maquinaria); // Retorna vista de confirmación o error 404
        }

        // POST: Maquinaria/DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int idInventario)
        {
            // Intenta eliminar la maquinaria
            if (_maquinariaService.EliminarMaquinaria(idInventario))
            {
                TempData["SuccessMessage"] = "Maquinaria eliminada exitosamente."; // Mensaje de éxito
                return RedirectToAction(nameof(Index)); // Redirige a la lista de maquinarias
            }
            return NotFound(); // Retorna error 404 si no se encuentra
        }
    }
}
