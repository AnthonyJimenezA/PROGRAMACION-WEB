using Microsoft.EntityFrameworkCore;
using Proyecto_API__CRUD.Models;

namespace Proyecto_API__CRUD.Data
{
    public class Proyecto_API__CRUDContext : DbContext
    {
        public Proyecto_API__CRUDContext (DbContextOptions<Proyecto_API__CRUDContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Cliente { get; set; } = default!;

        public DbSet<Empleado> Empleado { get; set; } = default!;

        public DbSet<Maquinaria> Maquinaria { get; set; } = default!;
        
        public DbSet<Mantenimiento> Mantenimiento { get; set; } = default!;
    }
}
