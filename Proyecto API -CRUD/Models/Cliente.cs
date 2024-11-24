using System.ComponentModel.DataAnnotations;

namespace Proyecto_API__CRUD.Models
{
    public class Cliente
    {
        [Key]
        public string Identificacion { get; set; }

        public string NombreCompleto { get; set; }

        public string Provincia { get; set; }


        public string Canton { get; set; }


        public string Distrito { get; set; }


        public string DireccionExacta { get; set; }

        public string PreferenciaInvierno { get; set; } 

        public string PreferenciaVerano { get; set; } 
    }
}
