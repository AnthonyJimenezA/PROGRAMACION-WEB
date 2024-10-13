using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public class MaquinariaService : IMaquinariaService
    {
        
        private static readonly List<Maquinaria> _maquinarias = new List<Maquinaria>();

        public List<Maquinaria> ObtenerMaquinarias()
        {
            return _maquinarias; 
        }

        public Maquinaria ObtenerMaquinariaPorId(int idInventario)
        {
            return _maquinarias.FirstOrDefault(m => m.IdInventario == idInventario); 
        }

        public bool AgregarMaquinaria(Maquinaria maquinaria)
        {
            
            if (_maquinarias.Any(m => m.IdInventario == maquinaria.IdInventario))
            {
                return false; 
            }

            _maquinarias.Add(maquinaria); 
            return true; 
        }

        public bool ActualizarMaquinaria(Maquinaria maquinaria)
        {
            var maquinariaExistente = _maquinarias.FirstOrDefault(m => m.IdInventario == maquinaria.IdInventario);
            if (maquinariaExistente == null)
            {
                return false; 
            }

           
            maquinariaExistente.Descripcion = maquinaria.Descripcion;
            maquinariaExistente.Tipo = maquinaria.Tipo;
            maquinariaExistente.HorasUsoActuales = maquinaria.HorasUsoActuales;
            maquinariaExistente.HorasUsoMaximoDia = maquinaria.HorasUsoMaximoDia;
            maquinariaExistente.HorasUsoParaMantenimiento = maquinaria.HorasUsoParaMantenimiento;

            return true; 
        }

        public bool EliminarMaquinaria(int idInventario)
        {
            var maquinaria = _maquinarias.FirstOrDefault(m => m.IdInventario == idInventario);
            if (maquinaria == null)
            {
                return false; 
            }

            _maquinarias.Remove(maquinaria); 
            return true; 
        }

        public List<Maquinaria> BuscarMaquinariasPorId(string searchTerm)
        {
            return _maquinarias
                .Where(m => m.IdInventario.ToString().Contains(searchTerm)) 
                .ToList();
        }
    }
}
