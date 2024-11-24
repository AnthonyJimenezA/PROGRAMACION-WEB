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

        public DbSet<Proyecto_API__CRUD.Models.Cliente> Cliente { get; set; } = default!;

        public DbSet<Proyecto_API__CRUD.Models.Empleado> Empleado { get; set; } = default!;

        public DbSet<Proyecto_API__CRUD.Models.Maquinaria> Maquinaria { get; set; } = default!;
    }
}
