using Newtonsoft.Json;
using Proyecto_1__CRUD.Models;
using System.Text;

namespace Proyecto_1__CRUD.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly HttpClient _httpClient;

        public EmpleadoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5191/"); // Cambia esto por la URL de tu API
        }

        public async Task<List<Empleado>> ObtenerEmpleados()
        {
            var response = await _httpClient.GetAsync("api/Empleado");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Empleado>>(json);
            }
            return new List<Empleado>(); // Retorna una lista vacía si no hay éxito
        }

        public async Task<Empleado> ObtenerEmpleadoPorCedula(string cedula)
        {
            var response = await _httpClient.GetAsync($"api/Empleado/{cedula}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Empleado>(json);
            }
            return null; // Retorna null si no se encuentra el empleado
        }

        public async Task<bool> AgregarEmpleado(Empleado empleado)
        {
            var json = JsonConvert.SerializeObject(empleado);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Empleado", content);
            return response.IsSuccessStatusCode; // Retorna true si se agregó correctamente
        }

        public async Task<bool> ActualizarEmpleado(Empleado empleado)
        {
            var json = JsonConvert.SerializeObject(empleado);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Empleado/{empleado.Cedula}", content);
            return response.IsSuccessStatusCode; // Retorna true si se actualizó correctamente
        }

        public async Task<bool> EliminarEmpleado(string cedula)
        {
            var response = await _httpClient.DeleteAsync($"api/Empleado/{cedula}");
            return response.IsSuccessStatusCode; // Retorna true si se eliminó correctamente
        }

        public async Task<List<Empleado>> BuscarEmpleadosPorCedula(string searchTerm)
        {
            var response = await _httpClient.GetAsync($"api/Empleado/search?term={searchTerm}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Empleado>>(json);
            }
            return new List<Empleado>(); // Retorna una lista vacía si no hay éxito
        }

    }
}
