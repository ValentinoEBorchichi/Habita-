using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaReportesAnimales.Data;
using SistemaReportesAnimales.Models;

namespace SistemaReportesAnimales.Pages.Reportes;

[Authorize] // Este decorador es la clave para proteger la página
public class ListadoModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public ListadoModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Reporte> Reportes { get; set; } = new List<Reporte>();

    public async Task OnGetAsync()
    {
        Reportes = await _context.Reportes.ToListAsync();
    }
}
