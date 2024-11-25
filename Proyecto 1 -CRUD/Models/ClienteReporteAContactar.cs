using Newtonsoft.Json;

namespace Proyecto_1__CRUD.Models
{
    public class ClienteReporteAContactar
    {
        public string ClienteId { get; set; }
        public string Nombre { get; set; }
        public int MantenimientoId { get; set; }

        [JsonProperty("dateSiguienteChapia")]
        public DateTime FechaSiguienteChapia { get; set; }
    }

}
