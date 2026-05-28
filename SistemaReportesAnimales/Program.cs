using System;
using System.Collections.Generic;
using SistemaReportesAnimales.Repositories;
using SistemaReportesAnimales.Models;

namespace SistemaReportesAnimales;

class Program
{
    static void Main(string[] args)
    {
        IReporteRepository repository = null;
        
        bool salir = false;
        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("=================================================");
            Console.WriteLine("   SISTEMA DE CONTROL DE ANIMALES SUELTOS        ");
            Console.WriteLine("=================================================");
            Console.WriteLine("Seleccione el modo de ejecución:");
            Console.WriteLine("1. Versión con objetos en memoria");
            Console.WriteLine("2. Versión con acceso a datos (ADO.NET)");
            Console.WriteLine("3. Salir");
            Console.Write("Opcion: ");

            var opcionPrincipal = Console.ReadLine();
            
            switch (opcionPrincipal)
            {
                case "1":
                    repository = new MockReporteRepository();
                    MostrarMenuPrincipal(repository);
                    break;
                case "2":
                    string connectionString = "Server=(localdb)\\mssqllocaldb;Database=ControlAnimalesDB;Trusted_Connection=True;MultipleActiveResultSets=True;";
                    repository = new AdoNetReporteRepository(connectionString);
                    MostrarMenuPrincipal(repository);
                    break;
                case "3":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
                    System.Threading.Thread.Sleep(1000);
                    break;
            }
        }
    }

    static void MostrarMenuPrincipal(IReporteRepository repository)
    {
        bool volver = false;
        while (!volver)
        {
            Console.Clear();
            Console.WriteLine("--- MENÚ DE OPCIONES ---");
            Console.WriteLine("1. Visualizar el listado de reportes");
            Console.WriteLine("2. Ver la autoridad asignada a cada reporte");
            Console.WriteLine("3. Acceder al detalle de un reporte");
            Console.WriteLine("4. Visualizar el listado de animales de cada reporte");
            Console.WriteLine("5. Volver al menú anterior");
            Console.Write("Opcion: ");

            var opcion = Console.ReadLine();

            Console.WriteLine("\n-------------------------------------------------");
            
            try 
            {
                switch (opcion)
                {
                    case "1":
                        MostrarTodosLosReportes(repository);
                        break;
                    case "2":
                        MostrarAutoridadesDeReportes(repository);
                        break;
                    case "3":
                        MostrarDetalleDeReporte(repository);
                        break;
                    case "4":
                        MostrarAnimalesDeReportes(repository);
                        break;
                    case "5":
                        volver = true;
                        continue;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nOcurrió un error: {ex.Message}");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }

    static void MostrarTodosLosReportes(IReporteRepository repository)
    {
        Console.WriteLine("LISTADO DE REPORTES:");
        var reportes = repository.ObtenerTodosLosReportes();
        
        if (reportes.Count == 0)
        {
            Console.WriteLine("No hay reportes disponibles.");
            return;
        }

        foreach (var r in reportes)
        {
            Console.WriteLine($"ID: {r.Id} | Fecha: {r.FechaReporte.ToString("dd/MM/yyyy HH:mm")} | Ubicación: {r.Ubicacion}");
        }
    }

    static void MostrarAutoridadesDeReportes(IReporteRepository repository)
    {
        Console.WriteLine("AUTORIDAD ASIGNADA A CADA REPORTE:");
        var reportes = repository.ObtenerTodosLosReportes();
        
        if (reportes.Count == 0)
        {
            Console.WriteLine("No hay reportes disponibles.");
            return;
        }

        foreach (var r in reportes)
        {
            var autoridad = repository.ObtenerAutoridadDeReporte(r.Id);
            string nombreAutoridad = autoridad != null ? $"{autoridad.Nombre} ({autoridad.Cargo})" : "Sin asignar";
            Console.WriteLine($"Reporte #{r.Id} (Ubicación: {r.Ubicacion}) -> Autoridad: {nombreAutoridad}");
        }
    }

    static void MostrarDetalleDeReporte(IReporteRepository repository)
    {
        Console.Write("Ingrese el ID del reporte a consultar: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var reporte = repository.ObtenerReportePorId(id);
            if (reporte != null)
            {
                Console.WriteLine("\n--- DETALLE DEL REPORTE ---");
                Console.WriteLine($"ID: {reporte.Id}");
                Console.WriteLine($"Fecha: {reporte.FechaReporte}");
                Console.WriteLine($"Ubicación: {reporte.Ubicacion}");
                Console.WriteLine($"Reportante: {reporte.UsuarioReportante?.NombreCompleto ?? "Desconocido"} (Tel: {reporte.UsuarioReportante?.Telefono ?? "N/A"})");
                Console.WriteLine($"Autoridad a cargo: {reporte.AutoridadAsignada?.Nombre ?? "Sin asignar"}");
                
                Console.WriteLine("Animales Involucrados:");
                if (reporte.AnimalesSueltos != null && reporte.AnimalesSueltos.Count > 0)
                {
                    foreach (var a in reporte.AnimalesSueltos)
                    {
                        Console.WriteLine($" - {a.Especie}: {a.Descripcion}");
                    }
                }
                else
                {
                    Console.WriteLine(" - Ninguno registrado.");
                }
            }
            else
            {
                Console.WriteLine($"No se encontró un reporte con ID {id}.");
            }
        }
        else
        {
            Console.WriteLine("ID inválido.");
        }
    }

    static void MostrarAnimalesDeReportes(IReporteRepository repository)
    {
        Console.WriteLine("LISTADO DE ANIMALES POR REPORTE:");
        var reportes = repository.ObtenerTodosLosReportes();
        
        if (reportes.Count == 0)
        {
            Console.WriteLine("No hay reportes disponibles.");
            return;
        }

        foreach (var r in reportes)
        {
            Console.WriteLine($"\nReporte #{r.Id} - {r.Ubicacion}");
            var animales = repository.ObtenerAnimalesDeReporte(r.Id);
            
            if (animales.Count > 0)
            {
                foreach (var a in animales)
                {
                    Console.WriteLine($"  -> [ID: {a.Id}] Especie: {a.Especie} | Descripción: {a.Descripcion}");
                }
            }
            else
            {
                Console.WriteLine("  -> No hay animales registrados en este reporte.");
            }
        }
        Console.WriteLine("=================================================");
        Console.WriteLine("   SISTEMA DE CONTROL DE ANIMALES SUELTOS        ");
        Console.WriteLine("=================================================");
        Console.WriteLine("Estado actual: Modelos de dominio inicializados.");
        Console.WriteLine("Presione cualquier tecla para salir...");
        Console.ReadKey();
    }
}
