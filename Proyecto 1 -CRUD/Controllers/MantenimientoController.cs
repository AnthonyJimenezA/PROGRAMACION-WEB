﻿using Microsoft.AspNetCore.Mvc;
using Proyecto_1__CRUD.Models;
using Proyecto_1__CRUD.Services;

namespace Proyecto_1__CRUD.Controllers
{
    public class MantenimientoController : Controller
    {
        private readonly IMantenimientoService _mantenimientoService; // Servicio para manejar la lógica de mantenimiento
        private readonly IClienteService _clienteService; // Servicio para manejar la lógica de clientes
        private readonly ILogger<MantenimientoController> _logger; // Logger para registrar errores

        public MantenimientoController(IMantenimientoService mantenimientoService, ILogger<MantenimientoController> logger, IClienteService clienteService)
        {
            _mantenimientoService = mantenimientoService;
            _logger = logger;
            _clienteService = clienteService;
        }

        // GET: Mantenimiento
        public IActionResult Index(string searchTerm = null)
        {
            try
            {
                // Obtiene la lista de mantenimientos, filtrando si hay un término de búsqueda
                var mantenimientos = string.IsNullOrEmpty(searchTerm)
                    ? _mantenimientoService.ObtenerMantenimientos()
                    : _mantenimientoService.BuscarMantenimientosPorId(searchTerm);

                return View(mantenimientos); // Retorna la vista con la lista de mantenimientos
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de mantenimientos."); // Registra el error
                return View("Error", new ErrorViewModel { Message = "Error al obtener la lista de mantenimientos." }); // Retorna vista de error
            }
        }

        // GET: Mantenimiento/Create
        public IActionResult Create()
        {
            ViewBag.Clientes = _clienteService.ObtenerClientes(); // Carga la lista de clientes para el dropdown
            return View(); // Retorna la vista para crear nuevo mantenimiento
        }

        // POST: Mantenimiento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Mantenimiento mantenimiento)
        {
            if (ModelState.IsValid) // Verifica que el modelo sea válido
            {
                // Intenta agregar el nuevo mantenimiento
                if (_mantenimientoService.AgregarMantenimiento(mantenimiento))
                {
                    TempData["SuccessMessage"] = "Mantenimiento creado exitosamente."; // Mensaje de éxito
                    return RedirectToAction(nameof(Index)); // Redirige a la lista de mantenimientos
                }
                HandleMantenimientoCreationError(mantenimiento); // Maneja errores específicos al crear mantenimiento
            }
            return View(mantenimiento); // Retorna la vista con errores
        }

        private void HandleMantenimientoCreationError(Mantenimiento mantenimiento)
        {
            // Maneja errores específicos si el mantenimiento ya existe o el cliente tiene otro mantenimiento
            if (_mantenimientoService.ObtenerMantenimientoPorId(mantenimiento.IdMantenimiento) != null)
            {
                ModelState.AddModelError("", "Ya existe un mantenimiento con ese ID."); // Mensaje de error
            }
            else
            {
                ModelState.AddModelError("", "El cliente ya tiene un mantenimiento registrado."); // Mensaje de error
            }
        }

        // GET: Mantenimiento/Edit/idMantenimiento
        public IActionResult Edit(int idMantenimiento)
        {
            var mantenimiento = _mantenimientoService.ObtenerMantenimientoPorId(idMantenimiento); // Obtiene el mantenimiento por ID
            if (mantenimiento == null)
            {
                return NotFound(); // Retorna error 404 si no se encuentra
            }
            ViewBag.Clientes = _clienteService.ObtenerClientes(); // Carga la lista de clientes para el dropdown
            return View(mantenimiento); // Retorna la vista para editar
        }

        // POST: Mantenimiento/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Mantenimiento mantenimiento)
        {
            if (ModelState.IsValid) // Verifica que el modelo sea válido
            {
                // Intenta actualizar el mantenimiento
                if (_mantenimientoService.ActualizarMantenimiento(mantenimiento))
                {
                    TempData["SuccessMessage"] = "Mantenimiento actualizado exitosamente."; // Mensaje de éxito
                    return RedirectToAction(nameof(Index)); // Redirige a la lista de mantenimientos
                }
                HandleMantenimientoUpdateError(mantenimiento); // Maneja errores específicos al actualizar mantenimiento
            }
            return View(mantenimiento); // Retorna la vista con errores
        }

        private void HandleMantenimientoUpdateError(Mantenimiento mantenimiento)
        {
            // Maneja errores específicos si no se encuentra el mantenimiento o el cliente tiene otro mantenimiento
            if (_mantenimientoService.ObtenerMantenimientoPorId(mantenimiento.IdMantenimiento) == null)
            {
                ModelState.AddModelError("", "No se encontró el mantenimiento."); // Mensaje de error
            }
            else
            {
                ModelState.AddModelError("", "El cliente ya tiene otro mantenimiento registrado."); // Mensaje de error
            }
        }

        // GET: Mantenimiento/Delete/idMantenimiento
        public IActionResult Delete(int idMantenimiento)
        {
            var mantenimiento = _mantenimientoService.ObtenerMantenimientoPorId(idMantenimiento); // Obtiene el mantenimiento por ID
            return mantenimiento == null ? NotFound() : View(mantenimiento); // Retorna vista de confirmación o error 404
        }

        // POST: Mantenimiento/DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int idMantenimiento)
        {
            // Intenta eliminar el mantenimiento
            if (_mantenimientoService.EliminarMantenimiento(idMantenimiento))
            {
                TempData["SuccessMessage"] = "Mantenimiento eliminado exitosamente."; // Mensaje de éxito
                return RedirectToAction(nameof(Index)); // Redirige a la lista de mantenimientos
            }
            return NotFound(); // Retorna error 404 si no se encuentra
        }
    }
}
