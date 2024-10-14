using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public class MantenimientoService : IMantenimientoService
    {
        // Lista estática para mantener la persistencia de los mantenimientos
        private static readonly List<Mantenimiento> _mantenimientos = new List<Mantenimiento>();

        // Retorna la lista completa de mantenimientos
        public List<Mantenimiento> ObtenerMantenimientos()
        {
            return _mantenimientos;
        }

        // Busca y retorna un mantenimiento por su ID
        public Mantenimiento ObtenerMantenimientoPorId(int idMantenimiento)
        {
            return _mantenimientos.FirstOrDefault(m => m.IdMantenimiento == idMantenimiento);
        }

        // Agrega un nuevo mantenimiento si no existe ya y si el cliente no tiene otro mantenimiento registrado
        public bool AgregarMantenimiento(Mantenimiento mantenimiento)
        {
            if (_mantenimientos.Any(m => m.IdMantenimiento == mantenimiento.IdMantenimiento))
            {
                return false; // No se puede agregar si ya existe
            }

            if (_mantenimientos.Any(m => m.IdCliente == mantenimiento.IdCliente))
            {
                return false; // Solo un mantenimiento por cliente
            }

            // Calcular los días sin chapia y la fecha de la siguiente chapia
            mantenimiento.DiasSinChapia = (DateTime.Now - mantenimiento.FechaEjecutado).Days;
            if (mantenimiento.DiasSinChapia < 0)
            {
                mantenimiento.DiasSinChapia = 0; // Evita valores negativos
            }

            mantenimiento.FechaSiguienteChapia = mantenimiento.PreferenciaFrecuencia switch
            {
                "Quincenal" => mantenimiento.FechaEjecutado.AddDays(15),
                "Mensual" => mantenimiento.FechaEjecutado.AddDays(30),
                _ => mantenimiento.FechaEjecutado.AddDays(30) // Default a mensual si no se especifica
            };

            _mantenimientos.Add(mantenimiento); // Agregar mantenimiento a la lista
            return true; // Indica que se agregó correctamente
        }

        // Actualiza los detalles de un mantenimiento existente
        public bool ActualizarMantenimiento(Mantenimiento mantenimiento)
        {
            var mantenimientoExistente = _mantenimientos.FirstOrDefault(m => m.IdMantenimiento == mantenimiento.IdMantenimiento);
            if (mantenimientoExistente == null)
            {
                return false; // No se encontró el mantenimiento
            }

            // Verifica si el cliente está cambiando y ya tiene otro mantenimiento
            if (mantenimientoExistente.IdCliente != mantenimiento.IdCliente &&
                _mantenimientos.Any(m => m.IdCliente == mantenimiento.IdCliente))
            {
                return false; // Otro mantenimiento ya existe para el nuevo cliente
            }

            // Actualizar campos del mantenimiento existente
            mantenimientoExistente.IdCliente = mantenimiento.IdCliente;
            mantenimientoExistente.FechaEjecutado = mantenimiento.FechaEjecutado;
            mantenimientoExistente.FechaAgendado = mantenimiento.FechaAgendado;
            mantenimientoExistente.M2Propiedad = mantenimiento.M2Propiedad;
            mantenimientoExistente.M2CercaViva = mantenimiento.M2CercaViva;
            mantenimientoExistente.TipoZacate = mantenimiento.TipoZacate;
            mantenimientoExistente.ProductoAplicado = mantenimiento.ProductoAplicado;
            mantenimientoExistente.Producto = mantenimiento.Producto;
            mantenimientoExistente.CostoChapiaPorM2 = mantenimiento.CostoChapiaPorM2;
            mantenimientoExistente.CostoAplicacionProductoPorM2 = mantenimiento.CostoAplicacionProductoPorM2;
            mantenimientoExistente.IVA = mantenimiento.IVA;
            mantenimientoExistente.Estado = mantenimiento.Estado;
            mantenimientoExistente.PreferenciaFrecuencia = mantenimiento.PreferenciaFrecuencia;

            // Recalcular los días sin chapia y la fecha de la siguiente chapia
            mantenimientoExistente.DiasSinChapia = (DateTime.Now - mantenimiento.FechaEjecutado).Days;
            if (mantenimientoExistente.DiasSinChapia < 0)
            {
                mantenimientoExistente.DiasSinChapia = 0; // Evita valores negativos
            }

            mantenimientoExistente.FechaSiguienteChapia = mantenimientoExistente.PreferenciaFrecuencia switch
            {
                "Quincenal" => mantenimientoExistente.FechaEjecutado.AddDays(15),
                "Mensual" => mantenimientoExistente.FechaEjecutado.AddDays(30),
                _ => mantenimientoExistente.FechaEjecutado.AddDays(30)
            };

            return true; // Indica que se actualizó correctamente
        }

        // Elimina un mantenimiento de la lista por su ID
        public bool EliminarMantenimiento(int idMantenimiento)
        {
            var mantenimiento = _mantenimientos.FirstOrDefault(m => m.IdMantenimiento == idMantenimiento);
            if (mantenimiento == null)
            {
                return false; // No se encontró el mantenimiento
            }

            _mantenimientos.Remove(mantenimiento); // Eliminar mantenimiento
            return true; // Indica que se eliminó correctamente
        }

        // Busca mantenimientos por ID basado en un término de búsqueda
        public List<Mantenimiento> BuscarMantenimientosPorId(string searchTerm)
        {
            if (int.TryParse(searchTerm, out int id))
            {
                return _mantenimientos
                    .Where(m => m.IdMantenimiento == id)
                    .ToList(); // Retorna la lista de mantenimientos que coinciden
            }

            return new List<Mantenimiento>(); // Retorna lista vacía si no se puede convertir a ID
        }
    }
}
