using System.ComponentModel.DataAnnotations;

namespace Proyecto_1__CRUD.Models
{
    public class Mantenimiento
    {
        [Key]
        [Display(Name = "ID de Mantenimiento")]
        public int IdMantenimiento { get; set; }

        [Required(ErrorMessage = "El ID del cliente es obligatorio.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "El ID del cliente debe tener exactamente 10 caracteres.")]
        [Display(Name = "ID del Cliente")]
        public string IdCliente { get; set; }

        [Required(ErrorMessage = "La fecha ejecutada es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Ejecutada")]
        public DateTime FechaEjecutado { get; set; }

        [Required(ErrorMessage = "La fecha agendada es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Agendada")]
        public DateTime FechaAgendado { get; set; }

        [Required(ErrorMessage = "La cantidad de m² de la propiedad es obligatoria.")]
        [Range(0, double.MaxValue, ErrorMessage = "La cantidad de m² debe ser un valor positivo.")]
        [Display(Name = "Cantidad de m² de la Propiedad")]
        public double M2Propiedad { get; set; }

        [Required(ErrorMessage = "La cantidad de m² de cerca viva es obligatoria.")]
        [Range(0, double.MaxValue, ErrorMessage = "La cantidad de m² de cerca viva debe ser un valor positivo.")]
        [Display(Name = "Cantidad de m² de Cerca Viva")]
        public double M2CercaViva { get; set; }

        [Display(Name = "Días sin Chapia")]
        public int DiasSinChapia { get; set; } // Autocalculado

        [Display(Name = "Fecha Siguiente Chapia")]
        public DateTime FechaSiguienteChapia { get; set; } // Autocalculado

        [Required(ErrorMessage = "El tipo de zacate es obligatorio.")]
        [Display(Name = "Tipo de Zacate")]
        public string TipoZacate { get; set; } // "San Agustín", "Toro", "Dulce", "Otro"

        [Required(ErrorMessage = "Debe indicar si se ha aplicado algún producto.")]
        [Display(Name = "Producto Aplicado")]
        public bool ProductoAplicado { get; set; } // Sí o No

        [Display(Name = "Producto Aplicado")]
        public string Producto { get; set; } // "Random", "Coyolillo", "Hoja Ancha", "Otro"

        [Required(ErrorMessage = "El costo de la chapia por m² es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El costo de la chapia por m² debe ser un valor positivo.")]
        [Display(Name = "Costo de Chapia por m²")]
        public double CostoChapiaPorM2 { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El costo de aplicación de producto por m² debe ser un valor positivo.")]
        [Display(Name = "Costo de Aplicación de Producto por m²")]
        public double CostoAplicacionProductoPorM2 { get; set; }

        [Required(ErrorMessage = "El IVA es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El IVA debe ser un valor positivo.")]
        [Display(Name = "IVA (%)")]
        public double IVA { get; set; } = 13.0;

        [Required(ErrorMessage = "El estado del mantenimiento es obligatorio.")]
        [Display(Name = "Estado del Mantenimiento")]
        public string Estado { get; set; } // "En proceso", "Facturado", "Agendado"

        // Cálculo del costo total (no mapeado a base de datos)
        [Display(Name = "Costo Total")]
        public double CostoTotal
        {
            get
            {
                double costoTotal = (M2Propiedad * CostoChapiaPorM2) + (M2CercaViva * CostoChapiaPorM2);

                if (ProductoAplicado && !string.IsNullOrEmpty(Producto))
                {
                    costoTotal += (M2Propiedad * CostoAplicacionProductoPorM2) + (M2CercaViva * CostoAplicacionProductoPorM2);
                }

                // Aplicar IVA
                costoTotal += (costoTotal * IVA) / 100;

                return costoTotal;
            }
        }
    }
}
