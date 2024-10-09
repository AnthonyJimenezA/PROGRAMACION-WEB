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

        // GET: Empleado/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var empleados = await _empleadoService.ObtenerEmpleadosAsync();
                return View(empleados);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de empleados.");
                return View("Error", new ErrorViewModel { Message = "Error al obtener la lista de empleados." });
            }
        }

        // GET: Empleado/Details/cedula
        public async Task<IActionResult> Details(string cedula)
        {
            if (string.IsNullOrEmpty(cedula))
            {
                _logger.LogWarning("Detalles del empleado solicitados sin cédula.");
                return BadRequest("Cédula no proporcionada.");
            }

            try
            {
                var empleado = await _empleadoService.ObtenerEmpleadoPorCedulaAsync(cedula);
                if (empleado == null)
                {
                    _logger.LogWarning($"Empleado con cédula {cedula} no encontrado.");
                    return NotFound($"Empleado con cédula {cedula} no encontrado.");
                }

                return View(empleado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener detalles del empleado con cédula {cedula}.");
                return View("Error", new ErrorViewModel { Message = "Error al obtener detalles del empleado." });
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
        public async Task<IActionResult> Create(Empleado empleadoModel)
        {
            if (!ModelState.IsValid)
            {
                return View(empleadoModel);
            }

            try
            {
                var empleado = new Empleado
                {
                    Cedula = empleadoModel.Cedula,
                    NombreCompleto = empleadoModel.NombreCompleto,
                    FechaNacimiento = empleadoModel.FechaNacimiento,
                    Lateralidad = empleadoModel.Lateralidad,
                    FechaIngreso = empleadoModel.FechaIngreso,
                    SalarioPorHora = empleadoModel.SalarioPorHora
                };

                var resultado = await _empleadoService.AgregarEmpleadoAsync(empleado);
                if (resultado)
                {
                    _logger.LogInformation($"Empleado creado exitosamente con cédula {empleado.Cedula}.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Ya existe un empleado con esa cédula.");
                    _logger.LogWarning($"Intento de crear empleado con cédula existente: {empleado.Cedula}.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un nuevo empleado.");
                ModelState.AddModelError("", "Ocurrió un error al crear el empleado.");
            }

            return View(empleadoModel);
        }

        // GET: Empleado/Edit/cedula
        public async Task<IActionResult> Edit(string cedula)
        {
            if (string.IsNullOrEmpty(cedula))
            {
                _logger.LogWarning("Edición de empleado solicitada sin cédula.");
                return BadRequest("Cédula no proporcionada.");
            }

            try
            {
                var empleado = await _empleadoService.ObtenerEmpleadoPorCedulaAsync(cedula);
                if (empleado == null)
                {
                    _logger.LogWarning($"Empleado con cédula {cedula} no encontrado para edición.");
                    return NotFound($"Empleado con cédula {cedula} no encontrado.");
                }

                var empleadoViewModel = new Empleado
                {
                    Cedula = empleado.Cedula,
                    NombreCompleto = empleado.NombreCompleto,
                    FechaNacimiento = empleado.FechaNacimiento,
                    Lateralidad = empleado.Lateralidad,
                    FechaIngreso = empleado.FechaIngreso,
                    SalarioPorHora = empleado.SalarioPorHora
                };

                return View(empleadoViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el empleado con cédula {cedula} para edición.");
                return View("Error", new ErrorViewModel { Message = "Error al obtener el empleado para edición." });
            }
        }

        // POST: Empleado/Edit/cedula
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string cedula, Empleado empleadoModel)
        {
            if (cedula != empleadoModel.Cedula)
            {
                _logger.LogWarning($"Cédulas no coinciden en la edición: {cedula} != {empleadoModel.Cedula}");
                return BadRequest("Cédula no coincide.");
            }

            if (!ModelState.IsValid)
            {
                return View(empleadoModel);
            }

            try
            {
                var empleado = new Empleado
                {
                    Cedula = empleadoModel.Cedula,
                    NombreCompleto = empleadoModel.NombreCompleto,
                    FechaNacimiento = empleadoModel.FechaNacimiento,
                    Lateralidad = empleadoModel.Lateralidad,
                    FechaIngreso = empleadoModel.FechaIngreso,
                    SalarioPorHora = empleadoModel.SalarioPorHora
                };

                var resultado = await _empleadoService.ActualizarEmpleadoAsync(empleado);
                if (resultado)
                {
                    _logger.LogInformation($"Empleado actualizado exitosamente con cédula {empleado.Cedula}.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo actualizar el empleado.");
                    _logger.LogWarning($"No se pudo actualizar el empleado con cédula {empleado.Cedula}.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el empleado con cédula {cedula}.");
                ModelState.AddModelError("", "Ocurrió un error al actualizar el empleado.");
            }

            return View(empleadoModel);
        }

        // GET: Empleado/Delete/cedula
        public async Task<IActionResult> Delete(string cedula)
        {
            if (string.IsNullOrEmpty(cedula))
            {
                _logger.LogWarning("Eliminación de empleado solicitada sin cédula.");
                return BadRequest("Cédula no proporcionada.");
            }

            try
            {
                var empleado = await _empleadoService.ObtenerEmpleadoPorCedulaAsync(cedula);
                if (empleado == null)
                {
                    _logger.LogWarning($"Empleado con cédula {cedula} no encontrado para eliminación.");
                    return NotFound($"Empleado con cédula {cedula} no encontrado.");
                }

                return View(empleado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el empleado con cédula {cedula} para eliminación.");
                return View("Error", new ErrorViewModel { Message = "Error al obtener el empleado para eliminación." });
            }
        }

        // POST: Empleado/Delete/cedula
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string cedula)
        {
            if (string.IsNullOrEmpty(cedula))
            {
                _logger.LogWarning("Eliminación de empleado confirmada sin cédula.");
                return BadRequest("Cédula no proporcionada.");
            }

            try
            {
                var resultado = await _empleadoService.EliminarEmpleadoAsync(cedula);
                if (resultado)
                {
                    _logger.LogInformation($"Empleado eliminado exitosamente con cédula {cedula}.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogWarning($"Empleado con cédula {cedula} no encontrado para eliminación.");
                    return NotFound($"Empleado con cédula {cedula} no encontrado.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar el empleado con cédula {cedula}.");
                return View("Error", new ErrorViewModel { Message = "Error al eliminar el empleado." });
            }
        }
      
    }
}
