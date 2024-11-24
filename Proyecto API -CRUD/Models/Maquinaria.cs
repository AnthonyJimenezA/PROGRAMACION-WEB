using System.ComponentModel.DataAnnotations;

namespace Proyecto_API__CRUD.Models
{
    public class Maquinaria
    {
        [Key]
        public int IdInventario { get; set; }

        public string Descripcion { get; set; }

        public string Tipo { get; set; }

        public decimal HorasUsoActuales { get; set; }

        public decimal HorasUsoMaximoDia { get; set; }

        public decimal HorasUsoParaMantenimiento { get; set; }
    }
}
