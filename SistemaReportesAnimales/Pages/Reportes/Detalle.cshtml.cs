using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaReportesAnimales.Models;
using SistemaReportesAnimales.Repositories;

namespace SistemaReportesAnimales.Pages.Reportes;

[Authorize]
public class DetalleModel : PageModel
{
    private readonly IReporteRepository _reporteRepository;

    public DetalleModel(IReporteRepository reporteRepository)
    {
        _reporteRepository = reporteRepository;
    }

    public Reporte Reporte { get; set; }

    public IActionResult OnGet(int id)
    {
        Reporte = _reporteRepository.ObtenerReportePorId(id);

        if (Reporte == null)
        {
            return NotFound();
        }

        return Page();
    }
}
