using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaReportesAnimales.Repositories;

namespace SistemaReportesAnimales.Controllers;

[Authorize]
public class ReportesController : Controller
{
    private readonly IReporteRepository _reporteRepository;

    public ReportesController(IReporteRepository reporteRepository)
    {
        _reporteRepository = reporteRepository;
    }

    public IActionResult Index()
    {
        var reportes = _reporteRepository.ObtenerTodosLosReportes();
        return View(reportes);
    }

    public IActionResult Detalle(int id)
    {
        var reporte = _reporteRepository.ObtenerReportePorId(id);

        if (reporte == null)
        {
            return NotFound();
        }

        return View(reporte);
    }
}
