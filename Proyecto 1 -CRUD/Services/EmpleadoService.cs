using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        // Hacemos la lista estática para que persista en todo el proyecto
        private static readonly List<Empleado> _empleados = new List<Empleado>();

        public List<Empleado> ObtenerEmpleados()
        {
            return _empleados; // Retorna la lista de empleados
        }

        public Empleado ObtenerEmpleadoPorCedula(string cedula)
        {
            return _empleados.FirstOrDefault(e => e.Cedula == cedula); // Busca el empleado por cédula
        }

        public bool AgregarEmpleado(Empleado empleado)
        {
            // Verificar si ya existe un empleado con la misma cédula
            if (_empleados.Any(e => e.Cedula == empleado.Cedula))
            {
                return false; // Indica que el empleado no se pudo agregar
            }

            _empleados.Add(empleado); // Agregar el nuevo empleado
            return true; // Indica que el empleado se agregó correctamente
        }

        public bool ActualizarEmpleado(Empleado empleado)
        {
            var empleadoExistente = _empleados.FirstOrDefault(e => e.Cedula == empleado.Cedula);
            if (empleadoExistente == null)
            {
                return false; // Indica que el empleado no se pudo actualizar
            }

            // Actualizar los datos del empleado existente
            empleadoExistente.NombreCompleto = empleado.NombreCompleto;
            empleadoExistente.FechaNacimiento = empleado.FechaNacimiento;
            empleadoExistente.Lateralidad = empleado.Lateralidad;
            empleadoExistente.FechaIngreso = empleado.FechaIngreso;
            empleadoExistente.SalarioPorHora = empleado.SalarioPorHora;

            return true; // Indica que el empleado se actualizó correctamente
        }

        public bool EliminarEmpleado(string cedula)
        {
            var empleado = _empleados.FirstOrDefault(e => e.Cedula == cedula);
            if (empleado == null)
            {
                return false; // Indica que el empleado no se pudo eliminar
            }

            _empleados.Remove(empleado); // Eliminar el empleado
            return true; // Indica que el empleado se eliminó correctamente
        }

        public List<Empleado> BuscarEmpleadosPorCedula(string searchTerm)
        {
            return _empleados
                .Where(e => e.Cedula.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

    }
}
