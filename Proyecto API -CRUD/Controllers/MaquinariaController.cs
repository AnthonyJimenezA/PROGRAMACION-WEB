using Microsoft.AspNetCore.Mvc;
using Proyecto_API__CRUD.Data;
using Proyecto_API__CRUD.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proyecto_API__CRUD.Controllers
{
    [Route("api/[Maquinaria]")]
    [ApiController]
    public class MaquinariaController : ControllerBase
    {
        private readonly Memory _memory;

        public MaquinariaController()
        {
            _memory = new Memory();
        }

        // GET: api/Maquinaria
        [HttpGet]
        public ActionResult<IEnumerable<Maquinaria>> Get()
        {
            try
            {
                var maquinarias = _memory.ObtenerMaquinarias();
                return Ok(maquinarias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener las maquinarias", Details = ex.Message });
            }
        }

        // GET api/Maquinaria/{id}
        [HttpGet("{id}")]
        public ActionResult<Maquinaria> Get(int id)
        {
            try
            {
                var maquinaria = _memory.ObtenerMaquinariaPorId(id);
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
        public ActionResult Post([FromBody] Maquinaria maquinaria)
        {
            try
            {
                if (_memory.AgregarMaquinaria(maquinaria))
                {
                    return CreatedAtAction(nameof(Get), new { id = maquinaria.IdInventario }, maquinaria);
                }
                return Conflict(new { Message = "Ya existe una maquinaria con ese ID." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al agregar la maquinaria", Details = ex.Message });
            }
        }

        // PUT api/Maquinaria/{id}
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Maquinaria maquinaria)
        {
            try
            {
                if (maquinaria.IdInventario != id)
                {
                    return BadRequest(new { Message = "El ID en el cuerpo no coincide con el parámetro de la URL." });
                }

                if (_memory.ActualizarMaquinaria(maquinaria))
                {
                    return NoContent();
                }
                return NotFound(new { Message = "Maquinaria no encontrada para actualización." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al actualizar la maquinaria", Details = ex.Message });
            }
        }

        // DELETE api/Maquinaria/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                if (_memory.EliminarMaquinaria(id))
                {
                    return NoContent();
                }
                return NotFound(new { Message = "Maquinaria no encontrada para eliminación." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al eliminar la maquinaria", Details = ex.Message });
            }
        }
    }
}
