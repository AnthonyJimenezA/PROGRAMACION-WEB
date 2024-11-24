using System.ComponentModel.DataAnnotations;

namespace Proyecto_API__CRUD.Models
{
    public class Empleado
    {
        [Key]
        public string Cedula { get; set; }

        public string NombreCompleto { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Lateralidad { get; set; }

        public DateTime FechaIngreso { get; set; }

        public decimal SalarioPorHora { get; set; }
    }
}
