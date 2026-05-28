using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaReportesAnimales.Models;
using SistemaReportesAnimales.Repositories;

namespace SistemaReportesAnimales.Pages.Reportes;

[Authorize]
public class ListadoModel : PageModel
{
    private readonly IReporteRepository _reporteRepository;

    public ListadoModel(IReporteRepository reporteRepository)
    {
        _reporteRepository = reporteRepository;
    }

    public List<Reporte> Reportes { get; set; } = new List<Reporte>();

    public void OnGet()
    {
        Reportes = _reporteRepository.ObtenerTodosLosReportes();
    }
}
