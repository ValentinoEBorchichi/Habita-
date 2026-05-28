using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SistemaReportesAnimales.Models;

namespace SistemaReportesAnimales.Repositories;

public class AdoNetReporteRepository : IReporteRepository
{
    private readonly string _connectionString;

    public AdoNetReporteRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Reporte> ObtenerTodosLosReportes()
    {
        var reportes = new List<Reporte>();

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            string query = @"
                SELECT r.Id, r.FechaReporte, r.Ubicacion, 
                       u.Id as UsuarioId, u.NombreCompleto, u.Telefono as UsuarioTelefono,
                       a.Id as AutoridadId, a.Nombre as AutoridadNombre, a.Cargo, a.TelefonoContacto
                FROM Reportes r
                LEFT JOIN Usuarios u ON r.UsuarioId = u.Id
                LEFT JOIN Autoridades a ON r.AutoridadId = a.Id";

            SqlCommand cmd = new SqlCommand(query, conn);
            
            try 
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    var reporte = new Reporte
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FechaReporte = Convert.ToDateTime(reader["FechaReporte"]),
                        Ubicacion = reader["Ubicacion"].ToString()
                    };

                    if (reader["UsuarioId"] != DBNull.Value)
                    {
                        reporte.UsuarioReportante = new Usuario
                        {
                            Id = Convert.ToInt32(reader["UsuarioId"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            Telefono = reader["UsuarioTelefono"].ToString()
                        };
                    }

                    if (reader["AutoridadId"] != DBNull.Value)
                    {
                        reporte.AutoridadAsignada = new Autoridad
                        {
                            Id = Convert.ToInt32(reader["AutoridadId"]),
                            Nombre = reader["AutoridadNombre"].ToString(),
                            Cargo = reader["Cargo"].ToString(),
                            TelefonoContacto = reader["TelefonoContacto"].ToString()
                        };
                    }

                    reporte.AnimalesSueltos = ObtenerAnimalesDeReporteDirecto(conn, reporte.Id);
                    reportes.Add(reporte);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener reportes: " + ex.Message);
            }
        }

        return reportes;
    }

    public Reporte ObtenerReportePorId(int id)
    {
        Reporte reporte = null;

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            string query = @"
                SELECT r.Id, r.FechaReporte, r.Ubicacion, 
                       u.Id as UsuarioId, u.NombreCompleto, u.Telefono as UsuarioTelefono,
                       a.Id as AutoridadId, a.Nombre as AutoridadNombre, a.Cargo, a.TelefonoContacto
                FROM Reportes r
                LEFT JOIN Usuarios u ON r.UsuarioId = u.Id
                LEFT JOIN Autoridades a ON r.AutoridadId = a.Id
                WHERE r.Id = @Id";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    reporte = new Reporte
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FechaReporte = Convert.ToDateTime(reader["FechaReporte"]),
                        Ubicacion = reader["Ubicacion"].ToString()
                    };

                    if (reader["UsuarioId"] != DBNull.Value)
                    {
                        reporte.UsuarioReportante = new Usuario
                        {
                            Id = Convert.ToInt32(reader["UsuarioId"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            Telefono = reader["UsuarioTelefono"].ToString()
                        };
                    }

                    if (reader["AutoridadId"] != DBNull.Value)
                    {
                        reporte.AutoridadAsignada = new Autoridad
                        {
                            Id = Convert.ToInt32(reader["AutoridadId"]),
                            Nombre = reader["AutoridadNombre"].ToString(),
                            Cargo = reader["Cargo"].ToString(),
                            TelefonoContacto = reader["TelefonoContacto"].ToString()
                        };
                    }
                    
                    reporte.AnimalesSueltos = ObtenerAnimalesDeReporteDirecto(conn, reporte.Id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener reporte: " + ex.Message);
            }
        }

        return reporte;
    }

    public Autoridad ObtenerAutoridadDeReporte(int reporteId)
    {
        Autoridad autoridad = null;
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            string query = @"
                SELECT a.Id, a.Nombre, a.Cargo, a.TelefonoContacto
                FROM Reportes r
                INNER JOIN Autoridades a ON r.AutoridadId = a.Id
                WHERE r.Id = @ReporteId";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ReporteId", reporteId);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    autoridad = new Autoridad
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nombre = reader["Nombre"].ToString(),
                        Cargo = reader["Cargo"].ToString(),
                        TelefonoContacto = reader["TelefonoContacto"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener autoridad del reporte: " + ex.Message);
            }
        }
        return autoridad;
    }

    public List<Animal> ObtenerAnimalesDeReporte(int reporteId)
    {
        var animales = new List<Animal>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            try
            {
                conn.Open();
                animales = ObtenerAnimalesDeReporteDirecto(conn, reporteId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener animales del reporte: " + ex.Message);
            }
        }
        return animales;
    }

    private List<Animal> ObtenerAnimalesDeReporteDirecto(SqlConnection openConnection, int reporteId)
    {
        var animales = new List<Animal>();
        string query = "SELECT Id, Especie, Descripcion FROM Animales WHERE ReporteId = @ReporteId";
        
        using (SqlCommand cmd = new SqlCommand(query, openConnection))
        {
            cmd.Parameters.AddWithValue("@ReporteId", reporteId);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    animales.Add(new Animal
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Especie = reader["Especie"].ToString(),
                        Descripcion = reader["Descripcion"].ToString()
                    });
                }
            }
        }
        return animales;
    }
}
