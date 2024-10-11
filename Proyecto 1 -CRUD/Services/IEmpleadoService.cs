using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public interface IEmpleadoService
    {
        List<Empleado> ObtenerEmpleados(); 
        Empleado ObtenerEmpleadoPorCedula(string cedula); 
        bool AgregarEmpleado(Empleado empleado); 
        bool ActualizarEmpleado(Empleado empleado); 
        bool EliminarEmpleado(string cedula);
        List<Empleado> BuscarEmpleadosPorCedula(string searchTerm);
    }
}
