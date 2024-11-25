using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_API__CRUD.Data;
using Proyecto_API__CRUD.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proyecto_API__CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientoController : ControllerBase
    {
        private readonly Proyecto_API__CRUDContext _context;

        public MantenimientoController(Proyecto_API__CRUDContext context)
        {
            _context = context;
        }

        // GET: api/Mantenimiento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mantenimiento>>> Get()
        {
            try
            {
                var mantenimientos = await _context.Mantenimiento.ToListAsync();
                return Ok(mantenimientos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener los mantenimientos", Details = ex.Message });
            }
        }

        // GET api/Mantenimiento/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Mantenimiento>> Get(int id)
        {
            try
            {
                var mantenimiento = await _context.Mantenimiento.FindAsync(id);
                if (mantenimiento == null)
                {
                    return NotFound(new { Message = "Mantenimiento no encontrado." });
                }
                return Ok(mantenimiento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener el mantenimiento", Details = ex.Message });
            }
        }

        // POST api/Mantenimiento
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Mantenimiento mantenimiento)
        {
            try
            {
                var existeMantenimiento = await _context.Mantenimiento.AnyAsync(m => m.IdMantenimiento == mantenimiento.IdMantenimiento);
                if (existeMantenimiento)
                {
                    return Conflict(new { Message = "Ya existe un mantenimiento con esa identificación." });
                }

                _context.Mantenimiento.Add(mantenimiento);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { identificacion = mantenimiento.IdMantenimiento }, mantenimiento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al agregar el mantenimiento", Details = ex.Message });
            }
        }

        // PUT api/Mantenimiento/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Mantenimiento mantenimiento)
        {
            try
            {
                if (mantenimiento.IdMantenimiento != id)
                {
                    return BadRequest(new { Message = "La identificación en el cuerpo no coincide con el parámetro de la URL." });
                }

                var mantenimientoExistente = await _context.Mantenimiento.FindAsync(id);
                if (mantenimientoExistente == null)
                {
                    return NotFound(new { Message = "Mantenimiento no encontrado para actualización." });
                }

                _context.Entry(mantenimientoExistente).CurrentValues.SetValues(mantenimiento);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al actualizar el mantenimiento", Details = ex.Message });
            }
        }

        // DELETE api/Mantenimiento/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var mantenimiento = await _context.Mantenimiento.FindAsync(id);
                if (mantenimiento == null)
                {
                    return NotFound(new { Message = "Mantenimiento no encontrado para eliminación." });
                }

                _context.Mantenimiento.Remove(mantenimiento);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al eliminar el mantenimiento", Details = ex.Message });
            }
        }

        // GET api/Mantenimiento/search?term={term}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Mantenimiento>>> BuscarMantenimientosPorCedula([FromQuery] string term)
        {
            try
            {
                var mantenimientos = await _context.Mantenimiento
                     .Where(m => m.IdMantenimiento.ToString().Contains(term))
                     .ToListAsync();

                if (!mantenimientos.Any())
                {
                    return NotFound(new { Message = "No se encontraron mantenimientos que coincidan con el término de búsqueda." });
                }

                return Ok(mantenimientos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al buscar mantenimientos.", Details = ex.Message });
            }
        }

    }
}
