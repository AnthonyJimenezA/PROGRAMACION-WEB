using System.ComponentModel.DataAnnotations;

namespace Proyecto_1__CRUD.Models
{
    public class Maquinaria
    {
        [Key]
        [Display(Name = "ID de Inventario")]
        public int IdInventario { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(200, ErrorMessage = "La descripción no puede exceder los 200 caracteres.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El tipo de maquinaria es obligatorio.")]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; } // Ejemplos: Shindaiwa, Corta Setos, Motosierra, Rastrillo, Carretillo, Otros

        [Required(ErrorMessage = "Las horas de uso actuales son obligatorias.")]
        [Range(0, double.MaxValue, ErrorMessage = "Las horas de uso actuales deben ser un valor positivo.")]
        [Display(Name = "Horas de Uso Actuales")]
        public double HorasUsoActuales { get; set; }

        [Required(ErrorMessage = "Las horas de uso máximo al día son obligatorias.")]
        [Range(0, double.MaxValue, ErrorMessage = "Las horas de uso máximo al día deben ser un valor positivo.")]
        [Display(Name = "Horas de Uso Máximo al Día")]
        public double HorasUsoMaximoDia { get; set; }

        [Required(ErrorMessage = "Las horas de uso para mantenimiento son obligatorias.")]
        [Range(0, double.MaxValue, ErrorMessage = "Las horas de uso para mantenimiento deben ser un valor positivo.")]
        [Display(Name = "Horas de Uso para Mantenimiento")]
        public double HorasUsoParaMantenimiento { get; set; }
    }
}
