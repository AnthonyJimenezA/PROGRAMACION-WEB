using Newtonsoft.Json;
using Proyecto_1__CRUD.Models;
using System.Text;

namespace Proyecto_1__CRUD.Services
{
    public class MaquinariaService : IMaquinariaService
    {
        private readonly HttpClient _httpClient;

        public MaquinariaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5191/"); // Cambia esto por la URL de tu API
        }

        public async Task<List<Maquinaria>> ObtenerMaquinarias()
        {
            var response = await _httpClient.GetAsync("api/Maquinaria");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Maquinaria>>(json);
            }
            return new List<Maquinaria>(); // Retorna una lista vacía si no hay éxito
        }

        public async Task<Maquinaria> ObtenerMaquinariaPorId(string idInventario)
        {
            var response = await _httpClient.GetAsync($"api/Maquinaria/{idInventario}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Maquinaria>(json);
            }
            return null; // Retorna null si no se encuentra la maquinaria
        }

        public async Task<bool> AgregarMaquinaria(Maquinaria maquinaria)
        {
            var json = JsonConvert.SerializeObject(maquinaria);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Maquinaria", content);
            return response.IsSuccessStatusCode; // Retorna true si se agregó correctamente
        }

        public async Task<bool> ActualizarMaquinaria(Maquinaria maquinaria)
        {
            var json = JsonConvert.SerializeObject(maquinaria);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Maquinaria/{maquinaria.IdInventario}", content);
            return response.IsSuccessStatusCode; // Retorna true si se actualizó correctamente
        }

        public async Task<bool> EliminarMaquinaria(string idInventario)
        {
            var response = await _httpClient.DeleteAsync($"api/Maquinaria/{idInventario}");
            return response.IsSuccessStatusCode; // Retorna true si se eliminó correctamente
        }

        public async Task<List<Maquinaria>> BuscarMaquinariasPorId(string searchTerm)
        {
            var response = await _httpClient.GetAsync($"api/Maquinaria/search?term={searchTerm}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Maquinaria>>(json);
            }
            return new List<Maquinaria>(); // Retorna una lista vacía si no hay éxito
        }
    }
}
