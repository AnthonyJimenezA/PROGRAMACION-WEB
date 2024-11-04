using Newtonsoft.Json;
using Proyecto_1__CRUD.Models;
using System.Text;

namespace Proyecto_1__CRUD.Services
{
    public class MantenimientoService : IMantenimientoService
    {
        private readonly HttpClient _httpClient;

        public MantenimientoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5191/"); // Cambia esto por la URL de tu API
        }

        public async Task<List<Mantenimiento>> ObtenerMantenimientos()
        {
            var response = await _httpClient.GetAsync("api/Mantenimiento");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Mantenimiento>>(json);
            }
            return new List<Mantenimiento>(); // Retorna una lista vacía si no hay éxito
        }

        public async Task<Mantenimiento> ObtenerMantenimientoPorId(int idMantenimiento)
        {
            var response = await _httpClient.GetAsync($"api/Mantenimiento/{idMantenimiento}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Mantenimiento>(json);
            }
            return null; // Retorna null si no se encuentra el mantenimiento
        }

        public async Task<bool> AgregarMantenimiento(Mantenimiento mantenimiento)
        {
            var json = JsonConvert.SerializeObject(mantenimiento);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Mantenimiento", content);
            return response.IsSuccessStatusCode; // Retorna true si se agregó correctamente
        }

        public async Task<bool> ActualizarMantenimiento(Mantenimiento mantenimiento)
        {
            var json = JsonConvert.SerializeObject(mantenimiento);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Mantenimiento/{mantenimiento.IdMantenimiento}", content);
            return response.IsSuccessStatusCode; // Retorna true si se actualizó correctamente
        }

        public async Task<bool> EliminarMantenimiento(int idMantenimiento)
        {
            var response = await _httpClient.DeleteAsync($"api/Mantenimiento/{idMantenimiento}");
            return response.IsSuccessStatusCode; // Retorna true si se eliminó correctamente
        }

        public async Task<List<Mantenimiento>> BuscarMantenimientosPorId(string searchTerm)
        {
            var response = await _httpClient.GetAsync($"api/Mantenimiento/search?term={searchTerm}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Mantenimiento>>(json);
            }
            return new List<Mantenimiento>(); // Retorna una lista vacía si no hay éxito
        }
    }
}
