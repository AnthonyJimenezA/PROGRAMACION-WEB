using Microsoft.AspNetCore.Mvc;
using Proyecto_API__CRUD.Data;
using Proyecto_API__CRUD.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proyecto_API__CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientoController : ControllerBase
    {
        private readonly Memory _memory;

        public MantenimientoController()
        {
            _memory = new Memory();
        }

        // GET: api/Mantenimiento
        [HttpGet]
        public ActionResult<IEnumerable<Mantenimiento>> Get()
        {
            try
            {
                var mantenimientos = _memory.ObtenerMantenimientos();
                return Ok(mantenimientos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener los mantenimientos", Details = ex.Message });
            }
        }

        // GET api/Mantenimiento/{id}
        [HttpGet("{id}")]
        public ActionResult<Mantenimiento> Get(string id)
        {
            try
            {
                var mantenimiento = _memory.ObtenerMantenimientoPorId(id);
                if (mantenimiento == null)
                {
                    return NotFound(new { Message = "Mantenimiento no encontrado" });
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
        public ActionResult Post([FromBody] Mantenimiento mantenimiento)
        {
            try
            {
                if (_memory.AgregarMantenimiento(mantenimiento))
                {
                    return CreatedAtAction(nameof(Get), new { id = mantenimiento.IdMantenimiento }, mantenimiento);
                }
                return Conflict(new { Message = "Ya existe un mantenimiento con ese ID." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al agregar el mantenimiento", Details = ex.Message });
            }
        }

        // PUT api/Mantenimiento/{id}
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Mantenimiento mantenimiento)
        {
            try
            {
                if (mantenimiento.IdMantenimiento != id)
                {
                    return BadRequest(new { Message = "El ID en el cuerpo no coincide con el parámetro de la URL." });
                }

                if (_memory.ActualizarMantenimiento(mantenimiento))
                {
                    return NoContent();
                }
                return NotFound(new { Message = "Mantenimiento no encontrado para actualización." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al actualizar el mantenimiento", Details = ex.Message });
            }
        }

        // DELETE api/Mantenimiento/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                if (_memory.EliminarMantenimiento(id))
                {
                    return NoContent();
                }
                return NotFound(new { Message = "Mantenimiento no encontrado para eliminación." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al eliminar el mantenimiento", Details = ex.Message });
            }
        }
    }
}
