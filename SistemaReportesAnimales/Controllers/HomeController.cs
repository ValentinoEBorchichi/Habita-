using Microsoft.AspNetCore.Mvc;

namespace SistemaReportesAnimales.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
