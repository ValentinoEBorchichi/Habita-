using System;
using System.Collections.Generic;

namespace SistemaReportesAnimales.Models;

public class Reporte
{
    public int Id { get; set; }
    public DateTime FechaReporte { get; set; }
    public string Ubicacion { get; set; }
    
    public Usuario UsuarioReportante { get; set; }
    public Autoridad AutoridadAsignada { get; set; }
    public List<Animal> AnimalesSueltos { get; set; } = new List<Animal>();
}
