using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_API__CRUD.Data;

namespace Proyecto_API__CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly Proyecto_API__CRUDContext _context;

        public ReportesController(Proyecto_API__CRUDContext context)
        {
            _context = context;
        }

        // Reporte 1: Clientes a contactar la próxima semana
        [HttpGet("clientes-a-contactar")]
        public async Task<ActionResult> ObtenerClientesAContactar()
        {
            // Definir la fecha de la próxima semana
            var fechaInicio = DateTime.Now.AddDays(7 - (int)DateTime.Now.DayOfWeek); // Primer día de la próxima semana
            var fechaFin = fechaInicio.AddDays(7); // Último día de la próxima semana

            // Obtener todos los mantenimientos y clientes desde la base de datos
            var mantenimientos = await _context.Mantenimiento.ToListAsync();
            var clientes = await _context.Cliente.ToListAsync();

            // Filtrar los datos en memoria
            var clientesAContactar = mantenimientos
                .Where(m => m.FechaSiguienteChapia >= fechaInicio && m.FechaSiguienteChapia <= fechaFin)
                .Join(clientes,
                    m => m.IdCliente,
                    c => c.Identificacion,
                    (m, c) => new
                    {
                        ClienteId = c.Identificacion,
                        Nombre = c.NombreCompleto,
                        MantenimientoId = m.IdMantenimiento,
                        DateSiguienteChapia = m.FechaSiguienteChapia
                    })
                .ToList();

            return Ok(clientesAContactar);
        }

        // Reporte 2: Clientes que no han agendado mantenimiento en más de dos meses
        [HttpGet("clientes-sin-mantenimiento")]
        public async Task<ActionResult> ObtenerClientesSinMantenimiento()
        {
            // Definir la fecha límite para dos meses atrás
            var fechaLimite = DateTime.Now.AddMonths(-2); // Fecha límite para dos meses atrás

            // Obtener todos los mantenimientos y clientes desde la base de datos
            var mantenimientos = await _context.Mantenimiento.ToListAsync();
            var clientes = await _context.Cliente.ToListAsync();

            // Filtrar los datos en memoria
            var clientesSinMantenimiento = mantenimientos
                .Where(m => m.FechaAgendado < fechaLimite)
                .Join(clientes,
                    m => m.IdCliente,
                    c => c.Identificacion,
                    (m, c) => new
                    {
                        ClienteId = c.Identificacion,
                        Nombre = c.NombreCompleto,
                        MantenimientoId = m.IdMantenimiento,
                        DateAgendado = m.FechaAgendado
                    })
                .ToList();

            return Ok(clientesSinMantenimiento);
        }
    }
}
