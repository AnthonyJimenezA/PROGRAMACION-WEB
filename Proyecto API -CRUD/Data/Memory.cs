using Proyecto_API__CRUD.Models;
using System.Runtime.Caching;

namespace Proyecto_API__CRUD.Data
{
    public class Memory
    {
        // Instancias de MemoryCache para cada tipo de entidad
        private static readonly MemoryCache _clientesCache = new MemoryCache("ClientesCache");
        private static readonly MemoryCache _empleadosCache = new MemoryCache("EmpleadosCache");
        private static readonly MemoryCache _mantenimientosCache = new MemoryCache("MantenimientosCache");
        private static readonly MemoryCache _maquinariasCache = new MemoryCache("MaquinariasCache");

        // Duración de la caché en minutos (puedes ajustarlo según sea necesario)
        private static readonly CacheItemPolicy _cachePolicy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30) };

        // Métodos CRUD para la entidad Cliente
        public List<Cliente> ObtenerClientes(string search = null)
        {
            var clientes = _clientesCache.Select(c => (Cliente)c.Value);
            if (!string.IsNullOrEmpty(search))
            {
                clientes = clientes.Where(c => c.Identificacion.Contains(search));
            }
            return clientes.ToList();
        }

        public Cliente ObtenerClientePorId(string id) => _clientesCache.Get(id) as Cliente;

        public bool AgregarCliente(Cliente cliente)
        {
            if (_clientesCache.Contains(cliente.Identificacion.ToString())) return false;
            _clientesCache.Add(cliente.Identificacion.ToString(), cliente, _cachePolicy);
            return true;
        }

        public bool ActualizarCliente(Cliente cliente)
        {
            if (!_clientesCache.Contains(cliente.Identificacion.ToString())) return false;
            _clientesCache.Set(cliente.Identificacion.ToString(), cliente, _cachePolicy);
            return true;
        }

        public bool EliminarCliente(string id)
        {
            return _clientesCache.Remove(id.ToString()) != null;
        }

        // Métodos CRUD para la entidad Empleado
        public List<Empleado> ObtenerEmpleados(string search = null)
        {
            var empleados = _empleadosCache.Select(e => (Empleado)e.Value);
            if (!string.IsNullOrEmpty(search))
            {
                empleados = empleados.Where(e => e.Cedula.Contains(search));
            }
            return empleados.ToList();
        }

        public Empleado ObtenerEmpleadoPorId(string id) => _empleadosCache.Get(id) as Empleado;

        public bool AgregarEmpleado(Empleado empleado)
        {
            if (_empleadosCache.Contains(empleado.Cedula.ToString())) return false;
            _empleadosCache.Add(empleado.Cedula.ToString(), empleado, _cachePolicy);
            return true;
        }

        public bool ActualizarEmpleado(Empleado empleado)
        {
            if (!_empleadosCache.Contains(empleado.Cedula.ToString())) return false;
            _empleadosCache.Set(empleado.Cedula.ToString(), empleado, _cachePolicy);
            return true;
        }

        public bool EliminarEmpleado(string id)
        {
            return _empleadosCache.Remove(id) != null;
        }

        // Métodos CRUD para la entidad Mantenimiento
        public List<Mantenimiento> ObtenerMantenimientos(string search = null)
        {
            var mantenimientos = _mantenimientosCache.Select(m => (Mantenimiento)m.Value);
            if (!string.IsNullOrEmpty(search))
            {
                mantenimientos = mantenimientos.Where(m => m.IdMantenimiento.ToString().Contains(search));
            }
            return mantenimientos.ToList();
        }

        public Mantenimiento ObtenerMantenimientoPorId(string id) => _mantenimientosCache.Get(id) as Mantenimiento;

        public bool AgregarMantenimiento(Mantenimiento mantenimiento)
        {
            if (_mantenimientosCache.Contains(mantenimiento.IdMantenimiento.ToString())) return false;
            _mantenimientosCache.Add(mantenimiento.IdMantenimiento.ToString(), mantenimiento, _cachePolicy);
            return true;
        }

        public bool ActualizarMantenimiento(Mantenimiento mantenimiento)
        {
            if (!_mantenimientosCache.Contains(mantenimiento.IdMantenimiento.ToString())) return false;
            _mantenimientosCache.Set(mantenimiento.IdMantenimiento.ToString(), mantenimiento, _cachePolicy);
            return true;
        }

        public bool EliminarMantenimiento(string id)
        {
            return _mantenimientosCache.Remove(id) != null;
        }

        // Métodos CRUD para la entidad Maquinaria
        public List<Maquinaria> ObtenerMaquinarias(string search = null)
        {
            var maquinarias = _maquinariasCache.Select(m => (Maquinaria)m.Value);
            if (!string.IsNullOrEmpty(search))
            {
                maquinarias = maquinarias.Where(m => m.IdInventario.ToString().Contains(search));
            }
            return maquinarias.ToList();
        }

        public Maquinaria ObtenerMaquinariaPorId(string id) => _maquinariasCache.Get(id) as Maquinaria;

        public bool AgregarMaquinaria(Maquinaria maquinaria)
        {
            if (_maquinariasCache.Contains(maquinaria.IdInventario.ToString())) return false;
            _maquinariasCache.Add(maquinaria.IdInventario.ToString(), maquinaria, _cachePolicy);
            return true;
        }

        public bool ActualizarMaquinaria(Maquinaria maquinaria)
        {
            if (!_maquinariasCache.Contains(maquinaria.IdInventario.ToString())) return false;
            _maquinariasCache.Set(maquinaria.IdInventario.ToString(), maquinaria, _cachePolicy);
            return true;
        }

        public bool EliminarMaquinaria(string id)
        {
            return _maquinariasCache.Remove(id) != null;
        }
    }
}
