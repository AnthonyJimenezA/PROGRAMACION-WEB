using Newtonsoft.Json;
using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public class ReportesService : IReportesService
    {
        private readonly HttpClient _httpClient;

        public ReportesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5191/"); // Cambia esto por la URL de tu API
        }

        public async Task<List<ClienteReporteAContactar>> ObtenerClientesAContactar()
        {
            var response = await _httpClient.GetAsync("api/reportes/clientes-a-contactar");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ClienteReporteAContactar>>(json);
            }
            return new List<ClienteReporteAContactar>(); // Retorna una lista vacía si no hay éxito
        }

        public async Task<List<ClienteReporteSinMantenimiento>> ObtenerClientesSinMantenimiento()
        {
            var response = await _httpClient.GetAsync("api/reportes/clientes-sin-mantenimiento");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ClienteReporteSinMantenimiento>>(json);
            }
            return new List<ClienteReporteSinMantenimiento>(); // Retorna una lista vacía si no hay éxito
        }
    }
}
