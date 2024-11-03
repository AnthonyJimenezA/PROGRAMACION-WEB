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
        public List<Cliente> ObtenerClientes() => _clientesCache.Select(c => (Cliente)c.Value).ToList();

        public Cliente ObtenerClientePorId(int id) => _clientesCache.Get(id.ToString()) as Cliente;

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

        public bool EliminarCliente(int id)
        {
            return _clientesCache.Remove(id.ToString()) != null;
        }

        // Métodos CRUD para la entidad Empleado
        public List<Empleado> ObtenerEmpleados() => _empleadosCache.Select(e => (Empleado)e.Value).ToList();

        public Empleado ObtenerEmpleadoPorId(int id) => _empleadosCache.Get(id.ToString()) as Empleado;

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

        public bool EliminarEmpleado(int id)
        {
            return _empleadosCache.Remove(id.ToString()) != null;
        }

        // Métodos CRUD para la entidad Mantenimiento
        public List<Mantenimiento> ObtenerMantenimientos() => _mantenimientosCache.Select(m => (Mantenimiento)m.Value).ToList();

        public Mantenimiento ObtenerMantenimientoPorId(int id) => _mantenimientosCache.Get(id.ToString()) as Mantenimiento;

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

        public bool EliminarMantenimiento(int id)
        {
            return _mantenimientosCache.Remove(id.ToString()) != null;
        }

        // Métodos CRUD para la entidad Maquinaria
        public List<Maquinaria> ObtenerMaquinarias() => _maquinariasCache.Select(m => (Maquinaria)m.Value).ToList();

        public Maquinaria ObtenerMaquinariaPorId(int id) => _maquinariasCache.Get(id.ToString()) as Maquinaria;

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

        public bool EliminarMaquinaria(int id)
        {
            return _maquinariasCache.Remove(id.ToString()) != null;
        }
    }
}
