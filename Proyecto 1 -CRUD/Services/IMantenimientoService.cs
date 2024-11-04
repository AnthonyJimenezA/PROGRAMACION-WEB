using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public interface IMantenimientoService
    {
        Task<List<Mantenimiento>> ObtenerMantenimientos();
        Task<Mantenimiento> ObtenerMantenimientoPorId(int idMantenimiento);
        Task<bool> AgregarMantenimiento(Mantenimiento mantenimiento);
        Task<bool> ActualizarMantenimiento(Mantenimiento mantenimiento);
        Task<bool> EliminarMantenimiento(int idMantenimiento);
        Task<List<Mantenimiento>> BuscarMantenimientosPorId(string searchTerm);

    }
}
