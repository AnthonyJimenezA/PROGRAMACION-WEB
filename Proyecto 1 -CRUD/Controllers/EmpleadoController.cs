using Microsoft.AspNetCore.Mvc;
using Proyecto_1__CRUD.Models;
using Proyecto_1__CRUD.Services;

namespace Proyecto_1__CRUD.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IEmpleadoService _empleadoService;
        private readonly ILogger<EmpleadoController> _logger;

        public EmpleadoController(IEmpleadoService empleadoService, ILogger<EmpleadoController> logger)
        {
            _empleadoService = empleadoService;
            _logger = logger;
        }

        // GET: Empleado
        public IActionResult Index(string searchTerm = null)
        {
            try
            {
                List<Empleado> empleados = string.IsNullOrEmpty(searchTerm)
                    ? _empleadoService.ObtenerEmpleados()
                    : _empleadoService.BuscarEmpleadosPorCedula(searchTerm);

                return View(empleados);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de empleados.");
                return View("Error", new ErrorViewModel { Message = "Error al obtener la lista de empleados." });
            }
        }

        // GET: Empleado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleado/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                bool added = _empleadoService.AgregarEmpleado(empleado);
                if (added)
                {
                    TempData["SuccessMessage"] = "Empleado creado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Ya existe un empleado con esa cédula.");
                }
            }
            return View(empleado);
        }

        // GET: Empleado/Edit/cedula
        public IActionResult Edit(string cedula)
        {
            if (cedula == null)
            {
                return NotFound();
            }

            Empleado empleado = _empleadoService.ObtenerEmpleadoPorCedula(cedula);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleado/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                bool updated = _empleadoService.ActualizarEmpleado(empleado);
                if (updated)
                {
                    TempData["SuccessMessage"] = "Empleado actualizado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo actualizar el empleado.");
                }
            }
            return View(empleado);
        }

        // GET: Empleado/Delete/cedula
        public IActionResult Delete(string cedula)
        {
            if (cedula == null)
            {
                return NotFound();
            }

            Empleado empleado = _empleadoService.ObtenerEmpleadoPorCedula(cedula);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleado/DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string cedula)
        {
            bool deleted = _empleadoService.EliminarEmpleado(cedula);
            if (deleted)
            {
                TempData["SuccessMessage"] = "Empleado eliminado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
