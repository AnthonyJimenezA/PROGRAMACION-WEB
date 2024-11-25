using Microsoft.AspNetCore.Mvc;
using Proyecto_1__CRUD.Services;

namespace Proyecto_1__CRUD.Controllers
{
    public class ReportesController : Controller
    {
        private readonly IReportesService _reportesService;

        // Inyección de dependencias del servicio de reportes
        public ReportesController(IReportesService reportesService)
        {
            _reportesService = reportesService;
        }

        [HttpGet("clientes-a-contactar")]
        public async Task<IActionResult> ClientesAContactar()
        {
            // Llamada al servicio para obtener los clientes a contactar
            var clientesAContactar = await _reportesService.ObtenerClientesAContactar();

            return View(clientesAContactar); // Devuelve la vista con el modelo
        }

        [HttpGet("clientes-sin-mantenimiento")]
        public async Task<IActionResult> ClientesSinMantenimiento()
        {
            // Llamada al servicio para obtener los clientes sin mantenimiento reciente
            var clientesSinMantenimiento = await _reportesService.ObtenerClientesSinMantenimiento();

            return View(clientesSinMantenimiento); // Devuelve la vista con el modelo
        }
    }
}
