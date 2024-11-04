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
        public async Task<IActionResult> Index(string searchTerm = null)
        {
            try
            {
                // Obtiene la lista de empleados, filtrando si hay un término de búsqueda
                List<Empleado> empleados = string.IsNullOrEmpty(searchTerm)
                    ? await _empleadoService.ObtenerEmpleados()
                    : await _empleadoService.BuscarEmpleadosPorCedula(searchTerm);

                return View(empleados); // Retorna la vista con la lista de empleados
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de empleados."); // Registra el error
                return View("Error", new ErrorViewModel { Message = "Error al obtener la lista de empleados." }); // Retorna vista de error
            }
        }

        // GET: Empleado/Create
        public IActionResult Create()
        {
            return View(); // Retorna la vista para crear nuevo empleado
        }

        // POST: Empleado/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Empleado empleado)
        {
            if (ModelState.IsValid) // Verifica que el modelo sea válido
            {
                // Intenta agregar el nuevo empleado
                bool added = await _empleadoService.AgregarEmpleado(empleado);
                if (added)
                {
                    TempData["SuccessMessage"] = "Empleado creado exitosamente."; // Mensaje de éxito
                    return RedirectToAction(nameof(Index)); // Redirige a la lista de empleados
                }
                else
                {
                    ModelState.AddModelError("", "Ya existe un empleado con esa cédula."); // Mensaje de error
                }
            }
            return View(empleado); // Retorna la vista con errores
        }

        // GET: Empleado/Edit/cedula
        public async Task<IActionResult> Edit(string cedula)
        {
            if (string.IsNullOrEmpty(cedula)) // Verifica si la cédula es nula
            {
                return NotFound(); // Retorna error 404
            }

            // Obtiene el empleado por cédula
            Empleado empleado = await _empleadoService.ObtenerEmpleadoPorCedula(cedula);
            if (empleado == null) // Verifica si el empleado no fue encontrado
            {
                return NotFound(); // Retorna error 404
            }
            return View(empleado); // Retorna la vista para editar el empleado
        }

        // POST: Empleado/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Empleado empleado)
        {
            if (ModelState.IsValid) // Verifica que el modelo sea válido
            {
                // Intenta actualizar el empleado
                bool updated = await _empleadoService.ActualizarEmpleado(empleado);
                if (updated)
                {
                    TempData["SuccessMessage"] = "Empleado actualizado exitosamente."; // Mensaje de éxito
                    return RedirectToAction(nameof(Index)); // Redirige a la lista de empleados
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo actualizar el empleado."); // Mensaje de error
                }
            }
            return View(empleado); // Retorna la vista con errores
        }

        // GET: Empleado/Delete/cedula
        public async Task<IActionResult> Delete(string cedula)
        {
            if (string.IsNullOrEmpty(cedula)) // Verifica si la cédula es nula
            {
                return NotFound(); // Retorna error 404
            }

            // Obtiene el empleado por cédula
            Empleado empleado = await _empleadoService.ObtenerEmpleadoPorCedula(cedula);
            if (empleado == null) // Verifica si el empleado no fue encontrado
            {
                return NotFound(); // Retorna error 404
            }

            return View(empleado); // Retorna vista de confirmación de eliminación
        }

        // POST: Empleado/DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string cedula)
        {
            // Intenta eliminar el empleado
            bool deleted = await _empleadoService.EliminarEmpleado(cedula);
            if (deleted)
            {
                TempData["SuccessMessage"] = "Empleado eliminado exitosamente."; // Mensaje de éxito
                return RedirectToAction(nameof(Index)); // Redirige a la lista de empleados
            }
            else
            {
                return NotFound(); // Retorna error 404 si no se encuentra
            }
        }


    }
}
