using System;
using System.Collections.Generic;
using System.Linq;
using SistemaReportesAnimales.Models;

namespace SistemaReportesAnimales.Repositories;

public class MockReporteRepository : IReporteRepository
{
    private readonly List<Reporte> _reportes;

    public MockReporteRepository()
    {
        var usuario1 = new Usuario { Id = 1, NombreCompleto = "Juan Perez", Telefono = "11-1234-5678" };
        var usuario2 = new Usuario { Id = 2, NombreCompleto = "Maria Gomez", Telefono = "11-8765-4321" };

        var autoridad1 = new Autoridad { Id = 1, Nombre = "Oficial Rodriguez", Cargo = "Control Animal", TelefonoContacto = "103" };
        var autoridad2 = new Autoridad { Id = 2, Nombre = "Inspectora Silva", Cargo = "Zoonosis", TelefonoContacto = "105" };

        var animal1 = new Animal { Id = 1, Especie = "Perro", Descripcion = "Cruza, color negro, tamaño mediano" };
        var animal2 = new Animal { Id = 2, Especie = "Caballo", Descripcion = "Color marrón, sin marcas, suelto en la ruta" };
        var animal3 = new Animal { Id = 3, Especie = "Vaca", Descripcion = "Raza Holando, suelta en el parque" };
        var animal4 = new Animal { Id = 4, Especie = "Gato", Descripcion = "Gato montés en zona urbana" };

        _reportes = new List<Reporte>
        {
            new Reporte
            {
                Id = 1,
                FechaReporte = DateTime.Now.AddDays(-2),
                Ubicacion = "Ruta Nacional 3, Km 35",
                UsuarioReportante = usuario1,
                AutoridadAsignada = autoridad1,
                AnimalesSueltos = new List<Animal> { animal2, animal3 }
            },
            new Reporte
            {
                Id = 2,
                FechaReporte = DateTime.Now.AddDays(-1),
                Ubicacion = "Plaza Central, San Justo",
                UsuarioReportante = usuario2,
                AutoridadAsignada = autoridad2,
                AnimalesSueltos = new List<Animal> { animal1 }
            },
            new Reporte
            {
                Id = 3,
                FechaReporte = DateTime.Now.AddHours(-5),
                Ubicacion = "Avenida Rivadavia y Moreno",
                UsuarioReportante = usuario1,
                AutoridadAsignada = autoridad1,
                AnimalesSueltos = new List<Animal> { animal4 }
            }
        };
    }

    public List<Reporte> ObtenerTodosLosReportes()
    {
        return _reportes;
    }

    public Reporte ObtenerReportePorId(int id)
    {
        return _reportes.FirstOrDefault(r => r.Id == id);
    }

    public Autoridad ObtenerAutoridadDeReporte(int reporteId)
    {
        var reporte = ObtenerReportePorId(reporteId);
        return reporte?.AutoridadAsignada;
    }

    public List<Animal> ObtenerAnimalesDeReporte(int reporteId)
    {
        var reporte = ObtenerReportePorId(reporteId);
        return reporte != null ? reporte.AnimalesSueltos : new List<Animal>();
    }
}
