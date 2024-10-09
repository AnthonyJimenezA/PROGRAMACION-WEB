using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public interface IEmpleadoService
    {
        Task<List<Empleado>> ObtenerEmpleadosAsync();
        Task<Empleado> ObtenerEmpleadoPorCedulaAsync(string cedula);
        Task<bool> AgregarEmpleadoAsync(Empleado empleado);
        Task<bool> ActualizarEmpleadoAsync(Empleado empleado);
        Task<bool> EliminarEmpleadoAsync(string cedula);
    }
}
