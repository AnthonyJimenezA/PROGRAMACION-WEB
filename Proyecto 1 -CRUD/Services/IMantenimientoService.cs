using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public interface IMantenimientoService
    {
        Task<List<Mantenimiento>> ObtenerMantenimientosAsync();
        Task<Mantenimiento> ObtenerMantenimientoPorIdAsync(int idMantenimiento);
        Task<bool> AgregarMantenimientoAsync(Mantenimiento mantenimiento);
        Task<bool> ActualizarMantenimientoAsync(Mantenimiento mantenimiento);
        Task<bool> EliminarMantenimientoAsync(int idMantenimiento);
    }
}
