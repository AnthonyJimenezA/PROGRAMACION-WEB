using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_API__CRUD.Data;
using Proyecto_API__CRUD.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proyecto_API__CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        private readonly Proyecto_API__CRUDContext _context;

        public ClienteController(Proyecto_API__CRUDContext context)
        {
            _context = context;
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            try
            {
                var clientes = await _context.Cliente.ToListAsync();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener los clientes.", Details = ex.Message });
            }
        }

        // GET api/Cliente/{identificacion}
        [HttpGet("{identificacion}")]
        public async Task<ActionResult<Cliente>> Get(string identificacion)
        {
            try
            {
                var cliente = await _context.Cliente.FindAsync(identificacion);
                if (cliente == null)
                {
                    return NotFound(new { Message = "Cliente no encontrado." });
                }
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener el cliente.", Details = ex.Message });
            }
        }

        // POST api/Cliente
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Cliente cliente)
        {
            try
            {
                var existeCliente = await _context.Cliente.AnyAsync(c => c.Identificacion == cliente.Identificacion);
                if (existeCliente)
                {
                    return Conflict(new { Message = "Ya existe un cliente con esa identificación." });
                }

                _context.Cliente.Add(cliente);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { identificacion = cliente.Identificacion }, cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al agregar el cliente.", Details = ex.Message });
            }
        }

        // PUT api/Cliente/{identificacion}
        [HttpPut("{identificacion}")]
        public async Task<ActionResult> Put(string identificacion, [FromBody] Cliente cliente)
        {
            try
            {
                if (cliente.Identificacion != identificacion)
                {
                    return BadRequest(new { Message = "La identificación en el cuerpo no coincide con el parámetro de la URL." });
                }

                var clienteExistente = await _context.Cliente.FindAsync(identificacion);
                if (clienteExistente == null)
                {
                    return NotFound(new { Message = "Cliente no encontrado para actualización." });
                }

                _context.Entry(clienteExistente).CurrentValues.SetValues(cliente);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al actualizar el cliente.", Details = ex.Message });
            }
        }

        // DELETE api/Cliente/{identificacion}
        [HttpDelete("{identificacion}")]
        public async Task<ActionResult> Delete(string identificacion)
        {
            try
            {
                var cliente = await _context.Cliente.FindAsync(identificacion);
                if (cliente == null)
                {
                    return NotFound(new { Message = "Cliente no encontrado para eliminación." });
                }

                _context.Cliente.Remove(cliente);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al eliminar el cliente.", Details = ex.Message });
            }
        }

        // GET api/Cliente/search?term={term}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Cliente>>> BuscarClientesPorCedula([FromQuery] string term)
        {
            try
            {
                var clientes = await _context.Cliente
                     .Where(c => c.Identificacion.Contains(term))
                     .ToListAsync();

                if (!clientes.Any())
                {
                    return NotFound(new { Message = "No se encontraron clientes que coincidan con el término de búsqueda." });
                }

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al buscar clientes.", Details = ex.Message });
            }
        }

    }
}
