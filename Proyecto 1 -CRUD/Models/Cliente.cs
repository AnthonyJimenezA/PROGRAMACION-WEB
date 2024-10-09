using System.ComponentModel.DataAnnotations;

namespace Proyecto_1__CRUD.Models
{
    public class Cliente
    {
        [Required(ErrorMessage = "La identificación es obligatoria.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "La identificación debe tener exactamente 10 caracteres.")]
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre completo no puede exceder los 100 caracteres.")]
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "La provincia es obligatoria.")]
        [StringLength(50, ErrorMessage = "La provincia no puede exceder los 50 caracteres.")]
        [Display(Name = "Provincia")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "El cantón es obligatorio.")]
        [StringLength(50, ErrorMessage = "El cantón no puede exceder los 50 caracteres.")]
        [Display(Name = "Cantón")]
        public string Canton { get; set; }

        [Required(ErrorMessage = "El distrito es obligatorio.")]
        [StringLength(50, ErrorMessage = "El distrito no puede exceder los 50 caracteres.")]
        [Display(Name = "Distrito")]
        public string Distrito { get; set; }

        [Required(ErrorMessage = "La dirección exacta es obligatoria.")]
        [StringLength(200, ErrorMessage = "La dirección exacta no puede exceder los 200 caracteres.")]
        [Display(Name = "Dirección Exacta")]
        public string DireccionExacta { get; set; }

        [Required(ErrorMessage = "La preferencia de mantenimiento en invierno es obligatoria.")]
        [Display(Name = "Preferencia de Mantenimiento en Invierno")]
        public string PreferenciaInvierno { get; set; } // "Quincenal" o "Mensual"

        [Required(ErrorMessage = "La preferencia de mantenimiento en verano es obligatoria.")]
        [Display(Name = "Preferencia de Mantenimiento en Verano")]
        public string PreferenciaVerano { get; set; } // "Quincenal" o "Mensual"
    }
}
