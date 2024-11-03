namespace Proyecto_API__CRUD.Models
{
    public class Empleado
    {

        public string Cedula { get; set; }

        public string NombreCompleto { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Lateralidad { get; set; }

        public DateTime FechaIngreso { get; set; }

        public double SalarioPorHora { get; set; }
    }
}
