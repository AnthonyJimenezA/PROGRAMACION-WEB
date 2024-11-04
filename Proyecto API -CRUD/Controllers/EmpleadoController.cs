using Microsoft.AspNetCore.Mvc;
using Proyecto_API__CRUD.Data;
using Proyecto_API__CRUD.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proyecto_API__CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly Memory _memory;

        public EmpleadoController()
        {
            _memory = new Memory();
        }

        // GET: api/Empleado
        [HttpGet]
        public ActionResult<IEnumerable<Empleado>> GetAll()
        {
            try
            {
                var empleados = _memory.ObtenerEmpleados();
                return Ok(empleados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener los empleados.", Details = ex.Message });
            }
        }

        // GET api/Empleado/{id}
        [HttpGet("{id}")]
        public ActionResult<Empleado> Get(string id)
        {
            try
            {
                var empleado = _memory.ObtenerEmpleadoPorId(id);
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
        public ActionResult Post([FromBody] Empleado empleado)
        {
            try
            {
                if (_memory.AgregarEmpleado(empleado))
                    return CreatedAtAction(nameof(Get), new { id = empleado.Cedula }, empleado);

                return Conflict(new { Message = "Ya existe un empleado con esa cédula." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al agregar el empleado.", Details = ex.Message });
            }
        }

        // PUT api/Empleado/{id}
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Empleado empleado)
        {
            try
            {
                if (empleado.Cedula != id.ToString())
                    return BadRequest(new { Message = "La cédula en el cuerpo no coincide con el parámetro de la URL." });

                if (_memory.ActualizarEmpleado(empleado))
                    return NoContent();

                return NotFound(new { Message = "Empleado no encontrado para actualización." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al actualizar el empleado.", Details = ex.Message });
            }
        }

        // DELETE api/Empleado/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                if (_memory.EliminarEmpleado(id))
                    return NoContent();

                return NotFound(new { Message = "Empleado no encontrado para eliminación." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al eliminar el empleado.", Details = ex.Message });
            }
        }


        // GET api/Empleado/search?term={term}
        [HttpGet("search")]
        public ActionResult<IEnumerable<Empleado>> BuscarEmpleadosPorCedula([FromQuery] string term)
        {
            try
            {
                var clientes = _memory.ObtenerEmpleados(term);
                if (!clientes.Any())
                {
                    return NotFound(new { Message = "No se encontraron empleados que coincidan con el término de búsqueda." });
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
