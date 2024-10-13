using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public interface IMaquinariaService
    {
        List<Maquinaria> ObtenerMaquinarias();
        Maquinaria ObtenerMaquinariaPorId(int idInventario);
        bool AgregarMaquinaria(Maquinaria maquinaria);
        bool ActualizarMaquinaria(Maquinaria maquinaria);
        bool EliminarMaquinaria(int idInventario);
        List<Maquinaria> BuscarMaquinariasPorId(string searchTerm);
    }
}
