using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public interface IMaquinariaService
    {
        Task<List<Maquinaria>> ObtenerMaquinariasAsync();
        Task<Maquinaria> ObtenerMaquinariaPorIdAsync(int idInventario);
        Task<bool> AgregarMaquinariaAsync(Maquinaria maquinaria);
        Task<bool> ActualizarMaquinariaAsync(Maquinaria maquinaria);
        Task<bool> EliminarMaquinariaAsync(int idInventario);
    }
}
