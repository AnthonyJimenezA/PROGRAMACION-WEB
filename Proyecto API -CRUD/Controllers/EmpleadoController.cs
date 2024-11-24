using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_API__CRUD.Data;
using Proyecto_API__CRUD.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proyecto_API__CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly Proyecto_API__CRUDContext _context;

        public EmpleadoController(Proyecto_API__CRUDContext context)
        {
            _context = context;
        }

        // GET: api/Empleado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> Get()
        {
            try
            {
                var empleados = await _context.Empleado.ToListAsync();
                return Ok(empleados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener los empleados.", Details = ex.Message });
            }
        }

        // GET api/Empleado/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> Get(string id)
        {
            try
            {
                var empleado = await _context.Empleado.FindAsync(id);
                if (empleado == null)
                    return NotFound(new { Message = "Empleado no encontrado." });

                return Ok(empleado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener el empleado.", Details = ex.Message });
            }
        }

        // POST api/Empleado
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Empleado empleado)
        {
            try
            {
                var existeEmpleado = await _context.Empleado.AnyAsync(e => e.Cedula == empleado.Cedula);
                if (existeEmpleado)
                    return Conflict(new { Message = "Ya existe un empleado con esa cédula." });

                _context.Empleado.Add(empleado);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = empleado.Cedula }, empleado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al agregar el empleado.", Details = ex.Message });
            }
        }

        // PUT api/Empleado/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] Empleado empleado)
        {
            try
            {
                if (empleado.Cedula != id)
                {
                    return BadRequest(new { Message = "La cédula en el cuerpo no coincide con el parámetro de la URL." });
                }

                var empleadoExistente = await _context.Empleado.FindAsync(id);
                if (empleadoExistente == null)
                {
                    return NotFound(new { Message = "Empleado no encontrado para actualización." });
                }

                _context.Entry(empleadoExistente).CurrentValues.SetValues(empleado);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al actualizar el empleado.", Details = ex.Message });
            }
        }

        // DELETE api/Empleado/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {

                var empleado = await _context.Empleado.FindAsync(id);
                if (empleado == null)
                    return NotFound(new {Message = "Empleado no encontrado para eliminación." });

                
                _context.Empleado.Remove(empleado);
                await _context.SaveChangesAsync();
                return NoContent();
              
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al eliminar el empleado.", Details = ex.Message });
            }
        }


        // GET api/Empleado/search?term={term}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Empleado>>> BuscarEmpleadosPorCedula([FromQuery] string term)
        {
            try
            {
                var empleados = await _context.Empleado
                    .Where(e => e.Cedula.Contains(term))
                    .ToListAsync();


                if (!empleados.Any())
                {
                    return NotFound(new { Message = "No se encontraron empleados que coincidan con el término de búsqueda." });
                }
                return Ok(empleados);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al buscar clientes.", Details = ex.Message });
            }
        }

    }
}
