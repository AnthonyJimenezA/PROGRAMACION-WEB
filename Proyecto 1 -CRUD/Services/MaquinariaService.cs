using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public class MaquinariaService : IMaquinariaService
    {
        // Lista estática para mantener la persistencia de la maquinaria
        private static readonly List<Maquinaria> _maquinarias = new List<Maquinaria>();

        // Retorna la lista completa de maquinarias
        public List<Maquinaria> ObtenerMaquinarias()
        {
            return _maquinarias;
        }

        // Busca y retorna una maquinaria por su ID
        public Maquinaria ObtenerMaquinariaPorId(int idInventario)
        {
            return _maquinarias.FirstOrDefault(m => m.IdInventario == idInventario);
        }

        // Agrega una nueva maquinaria si no existe ya en la lista
        public bool AgregarMaquinaria(Maquinaria maquinaria)
        {
            if (_maquinarias.Any(m => m.IdInventario == maquinaria.IdInventario))
            {
                return false; // No se puede agregar si ya existe
            }

            _maquinarias.Add(maquinaria); // Agregar maquinaria a la lista
            return true; // Indica que se agregó correctamente
        }

        // Actualiza los detalles de una maquinaria existente
        public bool ActualizarMaquinaria(Maquinaria maquinaria)
        {
            var maquinariaExistente = _maquinarias.FirstOrDefault(m => m.IdInventario == maquinaria.IdInventario);
            if (maquinariaExistente == null)
            {
                return false; // No se encontró la maquinaria
            }

            // Actualizar los campos de la maquinaria
            maquinariaExistente.Descripcion = maquinaria.Descripcion;
            maquinariaExistente.Tipo = maquinaria.Tipo;
            maquinariaExistente.HorasUsoActuales = maquinaria.HorasUsoActuales;
            maquinariaExistente.HorasUsoMaximoDia = maquinaria.HorasUsoMaximoDia;
            maquinariaExistente.HorasUsoParaMantenimiento = maquinaria.HorasUsoParaMantenimiento;

            return true; // Indica que se actualizó correctamente
        }

        // Elimina una maquinaria de la lista por su ID
        public bool EliminarMaquinaria(int idInventario)
        {
            var maquinaria = _maquinarias.FirstOrDefault(m => m.IdInventario == idInventario);
            if (maquinaria == null)
            {
                return false; // No se encontró la maquinaria
            }

            _maquinarias.Remove(maquinaria); // Eliminar maquinaria
            return true; // Indica que se eliminó correctamente
        }

        // Busca maquinarias por ID basado en un término de búsqueda
        public List<Maquinaria> BuscarMaquinariasPorId(string searchTerm)
        {
            return _maquinarias
                .Where(m => m.IdInventario.ToString().Contains(searchTerm))
                .ToList(); // Retorna la lista de maquinarias que coinciden
        }
    }
}
