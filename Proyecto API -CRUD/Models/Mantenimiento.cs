using System.ComponentModel.DataAnnotations;

namespace Proyecto_API__CRUD.Models
{
    public class Mantenimiento
    {
        [Key]
        public int IdMantenimiento { get; set; }


        public string IdCliente { get; set; }

        public DateTime FechaEjecutado { get; set; }

        public DateTime FechaAgendado { get; set; }

        public decimal M2Propiedad { get; set; }

        public decimal M2CercaViva { get; set; }

        public int DiasSinChapia => (DateTime.Now - FechaEjecutado).Days;

        public DateTime FechaSiguienteChapia => CalcularFechaSiguienteChapia();

        public string PreferenciaFrecuencia { get; set; } 


        public string TipoZacate { get; set; } 

        public bool ProductoAplicado { get; set; }

        public string ProductoAplicadoDisplay
        {
            get
            {
                return ProductoAplicado ? "Sí" : "No";
            }
        }

        public string Producto { get; set; } 

        public decimal CostoChapiaPorM2 { get; set; }


        public decimal CostoAplicacionProductoPorM2 { get; set; }


        public decimal IVA { get; set; } = 13.0m;

        public string Estado { get; set; }


        public decimal CostoTotal
        {
            get
            {
                // Cálculo del costo base
                decimal costoTotal = (M2Propiedad + M2CercaViva) * CostoChapiaPorM2;

                if (ProductoAplicado && !string.IsNullOrEmpty(Producto))
                {
                    costoTotal += (M2Propiedad + M2CercaViva) * CostoAplicacionProductoPorM2;
                }

                // Aplicar IVA
                costoTotal += (costoTotal * IVA) / 100;

                // Aplicar descuento si corresponde
                decimal descuento = CalcularDescuento((double)M2Propiedad);
                costoTotal -= (costoTotal * descuento) / 100;

                return Math.Round(costoTotal, 2);
            }
        }

        private decimal CalcularDescuento(double m2Terreno)
        {
            if (m2Terreno >= 400 && m2Terreno <= 900)
                return 2;
            else if (m2Terreno >= 901 && m2Terreno <= 1500)
                return 3;
            else if (m2Terreno >= 1501 && m2Terreno <= 2000)
                return 4;
            else if (m2Terreno > 2000)
                return 5;
            else
                return 0; // No hay descuento para menos de 400 m2
        }

        private DateTime CalcularFechaSiguienteChapia()
        {
            int diasAdicionales = PreferenciaFrecuencia.ToLower() == "quincenal" ? 15 : 30;
            return FechaEjecutado.AddDays(diasAdicionales);
        }

    }
}
