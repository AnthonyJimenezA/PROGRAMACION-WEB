using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public class MantenimientoService : IMantenimientoService
    {
        private static readonly List<Mantenimiento> _mantenimientos = new List<Mantenimiento>();

        public List<Mantenimiento> ObtenerMantenimientos()
        {
            return _mantenimientos;
        }

        public Mantenimiento ObtenerMantenimientoPorId(int idMantenimiento)
        {
            return _mantenimientos.FirstOrDefault(m => m.IdMantenimiento == idMantenimiento);
        }

        public bool AgregarMantenimiento(Mantenimiento mantenimiento)
        {
            // Verifica si ya existe un mantenimiento con el mismo ID
            if (_mantenimientos.Any(m => m.IdMantenimiento == mantenimiento.IdMantenimiento))
            {
                return false;
            }

            // Calcula los días sin chapia
            mantenimiento.DiasSinChapia = (DateTime.Now - mantenimiento.FechaEjecutado).Days;

            // Calcula la fecha aproximada de siguiente chapia
            if (mantenimiento.FechaEjecutado != null)
            {
                mantenimiento.FechaSiguienteChapia = mantenimiento.FechaEjecutado.AddDays(
                    mantenimiento.TipoZacate == "Quincenal" ? 15 : 30); // Asumiendo que los tipos de zacate pueden ser "Quincenal" o "Mensual"
            }

            // Calcula los costos
            mantenimiento.CostoTotal = (mantenimiento.CantidadM2Propiedad + mantenimiento.CantidadM2CercaViva) * mantenimiento.CostoChapiaPorM2;
            mantenimiento.CostoTotalAplicacionProducto = mantenimiento.ProductoAplicado ? mantenimiento.CantidadM2Propiedad * mantenimiento.CostoAplicacionProductoPorM2 : 0;

            // Sumar IVA
            double totalSinIVA = mantenimiento.CostoTotalChapia + mantenimiento.CostoTotalAplicacionProducto;
            mantenimiento.CostoTotalConIVA = totalSinIVA * 1.13; // Agregando el 13% de IVA

            // Agregar mantenimiento a la lista
            _mantenimientos.Add(mantenimiento);
            return true;
        }

        public bool ActualizarMantenimiento(Mantenimiento mantenimiento)
        {
            var mantenimientoExistente = _mantenimientos.FirstOrDefault(m => m.IdMantenimiento == mantenimiento.IdMantenimiento);
            if (mantenimientoExistente == null)
            {
                return false; 
            }

           
            mantenimientoExistente.IdCliente = mantenimiento.IdCliente;
            mantenimientoExistente.FechaEjecutado = mantenimiento.FechaEjecutado;
            mantenimientoExistente.FechaAgendado = mantenimiento.FechaAgendado;
            mantenimientoExistente.M2Propiedad = mantenimiento.M2Propiedad;
            mantenimientoExistente.M2CercaViva = mantenimiento.M2CercaViva;
            mantenimientoExistente.DiasSinChapia = mantenimiento.DiasSinChapia;
            mantenimientoExistente.FechaSiguienteChapia = mantenimiento.FechaSiguienteChapia;
            mantenimientoExistente.TipoZacate = mantenimiento.TipoZacate;
            mantenimientoExistente.ProductoAplicado = mantenimiento.ProductoAplicado;
            mantenimientoExistente.Producto = mantenimiento.Producto;
            mantenimientoExistente.CostoChapiaPorM2 = mantenimiento.CostoChapiaPorM2;
            mantenimientoExistente.CostoAplicacionProductoPorM2 = mantenimiento.CostoAplicacionProductoPorM2;
            mantenimientoExistente.IVA = mantenimiento.IVA;
            mantenimientoExistente.Estado = mantenimiento.Estado;

            return true; 
        }

        public bool EliminarMantenimiento(int idMantenimiento)
        {
            var mantenimiento = _mantenimientos.FirstOrDefault(m => m.IdMantenimiento == idMantenimiento);
            if (mantenimiento == null)
            {
                return false; 
            }

            _mantenimientos.Remove(mantenimiento);
            return true; 
        }

        public List<Mantenimiento> BuscarMantenimientosPorId(string searchTerm)
        {
            return _mantenimientos
                .Where(m => m.IdMantenimiento.ToString().Contains(searchTerm))
                .ToList();
        }
    }
}
