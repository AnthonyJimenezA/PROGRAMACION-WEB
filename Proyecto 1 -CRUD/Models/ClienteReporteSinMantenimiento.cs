using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace Proyecto_1__CRUD.Models
{
    public class ClienteReporteSinMantenimiento
    {
        public string ClienteId { get; set; }
        public string Nombre { get; set; }
        public int MantenimientoId { get; set; }

        [JsonProperty("dateSiguienteChapia")]
        public DateTime FechaAgendado { get; set; }
    }
}
