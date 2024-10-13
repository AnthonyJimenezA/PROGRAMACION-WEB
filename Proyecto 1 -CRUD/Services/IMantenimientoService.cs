using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public interface IMantenimientoService
    {
        List<Mantenimiento> ObtenerMantenimientos();
        Mantenimiento ObtenerMantenimientoPorId(int idMantenimiento);
        bool AgregarMantenimiento(Mantenimiento mantenimiento);
        bool ActualizarMantenimiento(Mantenimiento mantenimiento);
        bool EliminarMantenimiento(int idMantenimiento);
        List<Mantenimiento> BuscarMantenimientosPorId(string searchTerm);

    }
}
