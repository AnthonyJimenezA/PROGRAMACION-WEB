using Newtonsoft.Json;
using Proyecto_1__CRUD.Models;
using System.Text;

namespace Proyecto_1__CRUD.Services
{
    public class ClienteService : IClienteService
    {
        private readonly HttpClient _httpClient;

        public ClienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5191/"); // Cambia esto por la URL de tu API
        }

        public async Task<List<Cliente>> ObtenerClientes()
        {
            var response = await _httpClient.GetAsync("api/Cliente");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Cliente>>(json);
            }
            return new List<Cliente>(); // Retorna una lista vacía si no hay éxito
        }

        public async Task<Cliente> ObtenerClientePorId(string identificacion)
        {
            var response = await _httpClient.GetAsync($"api/Cliente/{identificacion}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Cliente>(json);
            }
            return null; // Retorna null si no se encuentra el cliente
        }

        public async Task<bool> AgregarCliente(Cliente cliente)
        {
            var json = JsonConvert.SerializeObject(cliente);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Cliente", content);
            return response.IsSuccessStatusCode; // Retorna true si se agregó correctamente
        }

        public async Task<bool> ActualizarCliente(Cliente cliente)
        {
            var json = JsonConvert.SerializeObject(cliente);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Cliente/{cliente.Identificacion}", content);
            return response.IsSuccessStatusCode; // Retorna true si se actualizó correctamente
        }

        public async Task<bool> EliminarCliente(string identificacion)
        {
            var response = await _httpClient.DeleteAsync($"api/Cliente/{identificacion}");
            return response.IsSuccessStatusCode; // Retorna true si se eliminó correctamente
        }

        public async Task<List<Cliente>> BuscarClientesPorCedula(string searchTerm)
        {
            var response = await _httpClient.GetAsync($"api/Cliente/search?term={searchTerm}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Cliente>>(json);
            }
            return new List<Cliente>(); // Retorna una lista vacía si no hay éxito
        }
    }
}
