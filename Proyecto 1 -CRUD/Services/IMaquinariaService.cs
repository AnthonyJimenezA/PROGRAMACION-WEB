using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public interface IMaquinariaService
    {
        Task<List<Maquinaria>> ObtenerMaquinarias();
        Task<Maquinaria> ObtenerMaquinariaPorId(string idInventario);
        Task<bool> AgregarMaquinaria(Maquinaria maquinaria);
        Task<bool> ActualizarMaquinaria(Maquinaria maquinaria);
        Task<bool> EliminarMaquinaria(string idInventario);
        Task<List<Maquinaria>> BuscarMaquinariasPorId(string searchTerm);
    }
}
