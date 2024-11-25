using Proyecto_1__CRUD.Models;

namespace Proyecto_1__CRUD.Services
{
    public interface IReportesService
    {
        Task<List<ClienteReporteAContactar>> ObtenerClientesAContactar();
        Task<List<ClienteReporteSinMantenimiento>> ObtenerClientesSinMantenimiento();
    }
}
