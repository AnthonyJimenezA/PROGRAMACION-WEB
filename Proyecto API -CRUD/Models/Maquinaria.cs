namespace Proyecto_API__CRUD.Models
{
    public class Maquinaria
    {

        public int IdInventario { get; set; }

        public string Descripcion { get; set; }

        public string Tipo { get; set; }

        public double HorasUsoActuales { get; set; }

        public double HorasUsoMaximoDia { get; set; }

        public double HorasUsoParaMantenimiento { get; set; }
    }
}
