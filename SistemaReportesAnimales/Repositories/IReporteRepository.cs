using System.Collections.Generic;
using SistemaReportesAnimales.Models;

namespace SistemaReportesAnimales.Repositories;

public interface IReporteRepository
{
    List<Reporte> ObtenerTodosLosReportes();
    Reporte ObtenerReportePorId(int id);
    Autoridad ObtenerAutoridadDeReporte(int reporteId);
    List<Animal> ObtenerAnimalesDeReporte(int reporteId);
}
