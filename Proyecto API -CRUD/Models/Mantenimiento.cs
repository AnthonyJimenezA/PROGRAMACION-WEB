namespace Proyecto_API__CRUD.Models
{
    public class Mantenimiento
    {

        public int IdMantenimiento { get; set; }


        public string IdCliente { get; set; }

        public DateTime FechaEjecutado { get; set; }

        public DateTime FechaAgendado { get; set; }

        public double M2Propiedad { get; set; }

        public double M2CercaViva { get; set; }

        public int DiasSinChapia { get; set; } 


        public DateTime FechaSiguienteChapia { get; set; } 

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

        public double CostoChapiaPorM2 { get; set; }


        public double CostoAplicacionProductoPorM2 { get; set; }


        public double IVA { get; set; } = 13.0;

        public string Estado { get; set; } 


        public double CostoTotal
        {
            get
            {
                double costoTotal = (M2Propiedad + M2CercaViva) * CostoChapiaPorM2;

                if (ProductoAplicado && !string.IsNullOrEmpty(Producto))
                {
                    costoTotal += (M2Propiedad + M2CercaViva) * CostoAplicacionProductoPorM2;
                }

                // Aplicar IVA
                costoTotal += (costoTotal * IVA) / 100;

                return costoTotal;
            }
        }
    }
}
