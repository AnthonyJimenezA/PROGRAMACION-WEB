using Microsoft.AspNetCore.Mvc;
using Proyecto_API__CRUD.Data;
using Proyecto_API__CRUD.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proyecto_API__CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        private readonly Memory _memory;

        public ClienteController()
        {
            _memory = new Memory();
        }

        // GET: api/Cliente
        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            try
            {
                var clientes = _memory.ObtenerClientes();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener los clientes.", Details = ex.Message });
            }
        }

        // GET api/Cliente/{identificacion}
        [HttpGet("{identificacion}")]
        public ActionResult<Cliente> Get(string identificacion)
        {
            try
            {
                var cliente = _memory.ObtenerClientePorId(identificacion);
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
        public ActionResult Post([FromBody] Cliente cliente)
        {
            try
            {
                if (_memory.AgregarCliente(cliente))
                {
                    return CreatedAtAction(nameof(Get), new { identificacion = cliente.Identificacion }, cliente);
                }
                return Conflict(new { Message = "Ya existe un cliente con esa identificación." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al agregar el cliente.", Details = ex.Message });
            }
        }

        // PUT api/Cliente/{identificacion}
        [HttpPut("{identificacion}")]
        public ActionResult Put(string identificacion, [FromBody] Cliente cliente)
        {
            try
            {
                if (cliente.Identificacion.ToString() != identificacion)
                {
                    return BadRequest(new { Message = "La identificación en el cuerpo no coincide con el parámetro de la URL." });
                }

                if (_memory.ActualizarCliente(cliente))
                {
                    return NoContent();
                }
                return NotFound(new { Message = "Cliente no encontrado para actualización." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al actualizar el cliente.", Details = ex.Message });
            }
        }

        // DELETE api/Cliente/{identificacion}
        [HttpDelete("{identificacion}")]
        public ActionResult Delete(string identificacion)
        {
            try
            {
                if (_memory.EliminarCliente(identificacion))
                {
                    return NoContent();
                }
                return NotFound(new { Message = "Cliente no encontrado para eliminación." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al eliminar el cliente.", Details = ex.Message });
            }
        }
    }
}
