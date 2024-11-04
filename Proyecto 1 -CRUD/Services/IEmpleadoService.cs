using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public interface IEmpleadoService
    {
        Task<List<Empleado>> ObtenerEmpleados(); 
        Task<Empleado> ObtenerEmpleadoPorCedula(string cedula);
        Task<bool> AgregarEmpleado(Empleado empleado);
        Task<bool> ActualizarEmpleado(Empleado empleado); 
        Task<bool> EliminarEmpleado(string cedula); 
        Task<List<Empleado>> BuscarEmpleadosPorCedula(string searchTerm);
    }
}
