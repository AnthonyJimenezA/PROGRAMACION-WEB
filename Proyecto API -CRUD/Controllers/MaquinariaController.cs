using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_API__CRUD.Data;
using Proyecto_API__CRUD.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proyecto_API__CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaquinariaController : ControllerBase
    {
        private readonly Proyecto_API__CRUDContext _context;

        public MaquinariaController(Proyecto_API__CRUDContext context)
        {
            _context = context;
        }

        // GET: api/Maquinaria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Maquinaria>>> Get()
        {
            try
            {
                var maquinarias = await _context.Maquinaria.ToListAsync();
                return Ok(maquinarias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener las maquinarias", Details = ex.Message });
            }
        }

        // GET api/Maquinaria/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Maquinaria>> Get(int id)
        {
            try
            {
                var maquinaria = await _context.Maquinaria.FindAsync(id);
                if (maquinaria == null)
                {
                    return NotFound(new { Message = "Maquinaria no encontrada" });
                }
                return Ok(maquinaria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener la maquinaria", Details = ex.Message });
            }
        }

        // POST api/Maquinaria
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Maquinaria maquinaria)
        {
            try
            {
                var existeMaquinaria = await _context.Maquinaria.AnyAsync(m => m.IdInventario == maquinaria.IdInventario);

                if (existeMaquinaria)
                {
                    return Conflict(new { Message = "Ya existe una maquinaria con ese ID." });

                }

                _context.Add(maquinaria);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { idInventario = maquinaria.IdInventario }, maquinaria);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al agregar la maquinaria", Details = ex.Message });
            }
        }

        // PUT api/Maquinaria/{id}
        [HttpPut("{id}")]
        public async  Task<ActionResult> Put(int id, [FromBody] Maquinaria maquinaria)
        {
            try
            {
                if (maquinaria.IdInventario != id)
                {
                    return BadRequest(new { Message = "La identificación en el cuerpo no coincide con el parámetro de la URL." });
                }

                var maquinariaExistente = await _context.Maquinaria.FindAsync(id);
                if (maquinariaExistente == null)
                {
                    return NotFound(new { Message = "Maquinaria no encontrado para actualización." });
                }

                _context.Entry(maquinariaExistente).CurrentValues.SetValues(maquinaria);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al actualizar la maquinaria", Details = ex.Message });
            }
        }

        // DELETE api/Maquinaria/{id}
        [HttpDelete("{id}")]
        public async  Task<ActionResult> Delete(string id)
        {
            try
            {
                var maquinaria = await _context.Maquinaria.FindAsync(id);
                if (maquinaria == null)
                {
                    return NotFound(new { Message = "Maquinaria no encontrado para eliminación." });
                }

                _context.Maquinaria.Remove(maquinaria);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al eliminar la maquinaria", Details = ex.Message });
            }
        }

        // GET api/Maquinaria/search?term={term}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Maquinaria>>> BuscarMaquinariasPorCedula([FromQuery] string term)
        {
            try
            {
                var maquinarias = await _context.Maquinaria
                 .Where(m => m.IdInventario.ToString().Contains(term))
                 .ToListAsync();


                if (!maquinarias.Any())
                {
                    return NotFound(new { Message = "No se encontraron maquinarias que coincidan con el término de búsqueda." });
                }

                return Ok(maquinarias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al buscar maquinarias.", Details = ex.Message });
            }
        }

    }
}
