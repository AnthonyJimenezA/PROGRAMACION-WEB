using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public interface IMaquinariaService
    {
        Task<List<Maquinaria>> ObtenerMaquinarias();
        Task<Maquinaria> ObtenerMaquinariaPorId(int idInventario);
        Task<bool> AgregarMaquinaria(Maquinaria maquinaria);
        Task<bool> ActualizarMaquinaria(Maquinaria maquinaria);
        Task<bool> EliminarMaquinaria(int idInventario);
        Task<List<Maquinaria>> BuscarMaquinariasPorId(string searchTerm);
    }
}
