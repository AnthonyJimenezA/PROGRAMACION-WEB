using Proyecto_1__CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

            // Verifica si el cliente ya tiene un mantenimiento registrado
            if (_mantenimientos.Any(m => m.IdCliente == mantenimiento.IdCliente))
            {
                return false; // Solo un mantenimiento por cliente
            }

            // Calcular los días sin chapia
            mantenimiento.DiasSinChapia = (DateTime.Now - mantenimiento.FechaEjecutado).Days;
            if (mantenimiento.DiasSinChapia < 0)
            {
                mantenimiento.DiasSinChapia = 0; // Evita valores negativos
            }

            // Calcular la fecha aproximada de la siguiente chapia
            mantenimiento.FechaSiguienteChapia = mantenimiento.PreferenciaFrecuencia switch
            {
                "Quincenal" => mantenimiento.FechaEjecutado.AddDays(15),
                "Mensual" => mantenimiento.FechaEjecutado.AddDays(30),
                _ => mantenimiento.FechaEjecutado.AddDays(30) // Default a mensual si no se especifica
            };

            // Nota: CostoTotal se calcula automáticamente en el modelo

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

            // Verifica si el cliente está cambiando y ya tiene otro mantenimiento
            if (mantenimientoExistente.IdCliente != mantenimiento.IdCliente &&
                _mantenimientos.Any(m => m.IdCliente == mantenimiento.IdCliente))
            {
                return false; // Otro mantenimiento ya existe para el nuevo cliente
            }

            // Actualizar campos básicos
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

            // Recalcular los días sin chapia
            mantenimientoExistente.DiasSinChapia = (DateTime.Now - mantenimiento.FechaEjecutado).Days;
            if (mantenimientoExistente.DiasSinChapia < 0)
            {
                mantenimientoExistente.DiasSinChapia = 0; // Evita valores negativos
            }

            // Recalcular la fecha de la siguiente chapia
            mantenimientoExistente.FechaSiguienteChapia = mantenimientoExistente.PreferenciaFrecuencia switch
            {
                "Quincenal" => mantenimientoExistente.FechaEjecutado.AddDays(15),
                "Mensual" => mantenimientoExistente.FechaEjecutado.AddDays(30),
                _ => mantenimientoExistente.FechaEjecutado.AddDays(30)
            };

            // Nota: CostoTotal se calcula automáticamente en el modelo

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
            if (int.TryParse(searchTerm, out int id))
            {
                return _mantenimientos
                    .Where(m => m.IdMantenimiento == id)
                    .ToList();
            }

            return new List<Mantenimiento>();
        }
    }
}
