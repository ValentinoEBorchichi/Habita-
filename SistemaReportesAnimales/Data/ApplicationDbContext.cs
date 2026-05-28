using Microsoft.EntityFrameworkCore;
using SistemaReportesAnimales.Models;

namespace SistemaReportesAnimales.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Autoridad> Autoridades { get; set; }
    public DbSet<Reporte> Reportes { get; set; }
    public DbSet<Animal> Animales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuraciones adicionales si son necesarias
        modelBuilder.Entity<Usuario>().ToTable("Usuarios");
        modelBuilder.Entity<Autoridad>().ToTable("Autoridades");
        modelBuilder.Entity<Reporte>().ToTable("Reportes");
        modelBuilder.Entity<Animal>().ToTable("Animales");
    }
}
