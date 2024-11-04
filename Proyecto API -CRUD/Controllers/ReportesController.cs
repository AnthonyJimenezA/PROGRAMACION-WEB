using Microsoft.AspNetCore.Mvc;
using Proyecto_API__CRUD.Data;
using Proyecto_API__CRUD.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proyecto_API__CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly Memory _memory;

        public ReportesController(Memory memory)
        {
            _memory = memory;
        }

        // Reporte 1: Clientes que deben ser contactados para agendar mantenimiento la próxima semana
        [HttpGet("clientes-proximos-mantenimientos")]
        public ActionResult<List<Cliente>> ObtenerClientesProximosMantenimientos()
        {
            try
            {
                var mantenimientos = _memory.ObtenerMantenimientos(); // Obtener todos los mantenimientos
                var clientes = _memory.ObtenerClientes(); // Obtener todos los clientes

                var fechaInicio = DateTime.Now.AddDays(7).StartOfWeek(DayOfWeek.Monday);
                var fechaFin = fechaInicio.AddDays(7);

                var clientesAContactar = mantenimientos
                    .Where(m => m.FechaSiguienteChapia >= fechaInicio && m.FechaSiguienteChapia < fechaFin)
                    .Select(m => clientes.FirstOrDefault(c => c.Identificacion == m.IdCliente))
                    .Where(c => c != null)
                    .Distinct()
                    .ToList();

                if (!clientesAContactar.Any())
                {
                    return NotFound("No hay clientes que deban ser contactados para mantenimiento la próxima semana.");
                }

                return Ok(clientesAContactar);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // Reporte 2: Clientes que no han agendado un servicio en más de dos meses
        [HttpGet("clientes-sin-mantenimiento")]
        public ActionResult<List<Cliente>> ObtenerClientesSinMantenimiento()
        {
            try
            {
                var mantenimientos = _memory.ObtenerMantenimientos(); // Obtener todos los mantenimientos
                var clientes = _memory.ObtenerClientes(); // Obtener todos los clientes

                var fechaLimite = DateTime.Now.AddMonths(-2);

                var clientesSinMantenimiento = clientes
                    .Where(c => !mantenimientos.Any(m => m.IdCliente == c.Identificacion && m.FechaEjecutado >= fechaLimite))
                    .ToList();

                if (!clientesSinMantenimiento.Any())
                {
                    return NotFound("No hay clientes que no hayan agendado un servicio en más de dos meses.");
                }

                return Ok(clientesSinMantenimiento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }

    // Extensión para obtener el inicio de la semana
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dateTime, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dateTime.DayOfWeek - startOfWeek)) % 7;
            return dateTime.AddDays(-1 * diff).Date;
        }
    }
}

