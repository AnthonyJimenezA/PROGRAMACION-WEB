using System;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_1__CRUD.Models
{
    public class Empleado
    {
        [Required(ErrorMessage = "La cédula es Obligatoria")]
        [StringLength(10,MinimumLength = 10, ErrorMessage = "La cédula debe tener exactamente 10 caracteres.")]
        [Display(Name = "Cédula")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre completo no puede exceder los 50 caracteres.")]
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La lateralidad es obligatoria.")]
        [Display(Name = "Lateralidad")]
        public string Lateralidad { get; set; } // "Diestro" o "Zurdo" "Ambidiestro"

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Ingreso")]
        public DateTime FechaIngreso { get; set; }

        [Required(ErrorMessage = "El salario por hora es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El salario por hora debe ser un valor positivo.")]
        [Display(Name = "Salario por Hora")]
        public double SalarioPorHora { get; set; }
    }
}
