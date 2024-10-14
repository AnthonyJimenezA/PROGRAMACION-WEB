using Microsoft.AspNetCore.Mvc;
using Proyecto_1__CRUD.Models;
using Proyecto_1__CRUD.Services;

namespace Proyecto_1__CRUD.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IEmpleadoService _empleadoService; // Servicio para manejar la lógica de empleados
        private readonly ILogger<EmpleadoController> _logger; // Logger para registrar errores

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
                // Obtiene la lista de empleados, filtrando si hay un término de búsqueda
                var empleados = string.IsNullOrEmpty(searchTerm)
                    ? _empleadoService.ObtenerEmpleados()
                    : _empleadoService.BuscarEmpleadosPorCedula(searchTerm);

                return View(empleados); // Retorna la vista con la lista de empleados
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de empleados."); // Registra el error
                return View("Error", new ErrorViewModel { Message = "Error al obtener la lista de empleados." }); // Retorna vista de error
            }
        }

        // GET: Empleado/Create
        public IActionResult Create() => View(); // Retorna la vista para crear nuevo empleado

        // POST: Empleado/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Empleado empleado)
        {
            if (ModelState.IsValid) // Verifica que el modelo sea válido
            {
                // Intenta agregar el nuevo empleado
                if (_empleadoService.AgregarEmpleado(empleado))
                {
                    TempData["SuccessMessage"] = "Empleado creado exitosamente."; // Mensaje de éxito
                    return RedirectToAction(nameof(Index)); // Redirige a la lista de empleados
                }
                ModelState.AddModelError("", "Ya existe un empleado con esa cédula."); // Mensaje de error
            }
            return View(empleado); // Retorna la vista con errores
        }

        // GET: Empleado/Edit/cedula
        public IActionResult Edit(string cedula)
        {
            var empleado = _empleadoService.ObtenerEmpleadoPorCedula(cedula); // Obtiene el empleado por cédula
            return empleado == null ? NotFound() : View(empleado); // Retorna la vista para editar o error 404
        }

        // POST: Empleado/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Empleado empleado)
        {
            if (ModelState.IsValid) // Verifica que el modelo sea válido
            {
                // Intenta actualizar el empleado
                if (_empleadoService.ActualizarEmpleado(empleado))
                {
                    TempData["SuccessMessage"] = "Empleado actualizado exitosamente."; // Mensaje de éxito
                    return RedirectToAction(nameof(Index)); // Redirige a la lista de empleados
                }
                ModelState.AddModelError("", "No se pudo actualizar el empleado."); // Mensaje de error
            }
            return View(empleado); // Retorna la vista con errores
        }

        // GET: Empleado/Delete/cedula
        public IActionResult Delete(string cedula)
        {
            var empleado = _empleadoService.ObtenerEmpleadoPorCedula(cedula); // Obtiene el empleado por cédula
            return empleado == null ? NotFound() : View(empleado); // Retorna vista de confirmación o error 404
        }

        // POST: Empleado/DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string cedula)
        {
            // Intenta eliminar el empleado
            if (_empleadoService.EliminarEmpleado(cedula))
            {
                TempData["SuccessMessage"] = "Empleado eliminado exitosamente."; // Mensaje de éxito
                return RedirectToAction(nameof(Index)); // Redirige a la lista de empleados
            }
            return NotFound(); // Retorna error 404 si no se encuentra
        }
    }
}
