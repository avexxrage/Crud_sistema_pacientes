using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Domain.Entities;

namespace SistemaPacientes.Infrastructure.Persistence;

/// <summary>
/// Contexto de Entity Framework Core (enfoque Code First sobre SQLite).
/// Centraliza los DbSet y la configuración Fluent API de las 12 tablas.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Especialidad> Especialidades => Set<Especialidad>();
    public DbSet<Consultorio> Consultorios => Set<Consultorio>();
    public DbSet<Medico> Medicos => Set<Medico>();
    public DbSet<Paciente> Pacientes => Set<Paciente>();
    public DbSet<Cita> Citas => Set<Cita>();
    public DbSet<Consulta> Consultas => Set<Consulta>();
    public DbSet<SignoVital> SignosVitales => Set<SignoVital>();
    public DbSet<Diagnostico> Diagnosticos => Set<Diagnostico>();
    public DbSet<Tratamiento> Tratamientos => Set<Tratamiento>();
    public DbSet<Medicamento> Medicamentos => Set<Medicamento>();
    public DbSet<Receta> Recetas => Set<Receta>();
    public DbSet<RecetaDetalle> RecetaDetalles => Set<RecetaDetalle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Especialidad 1..* Medico
        modelBuilder.Entity<Medico>()
            .HasOne(m => m.Especialidad)
            .WithMany(e => e.Medicos)
            .HasForeignKey(m => m.EspecialidadId)
            .OnDelete(DeleteBehavior.Restrict);

        // Cita -> Paciente / Medico / Consultorio (Restrict para evitar rutas de borrado múltiples)
        modelBuilder.Entity<Cita>()
            .HasOne(c => c.Paciente).WithMany(p => p.Citas)
            .HasForeignKey(c => c.PacienteId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Cita>()
            .HasOne(c => c.Medico).WithMany(m => m.Citas)
            .HasForeignKey(c => c.MedicoId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Cita>()
            .HasOne(c => c.Consultorio).WithMany(co => co.Citas)
            .HasForeignKey(c => c.ConsultorioId).OnDelete(DeleteBehavior.Restrict);

        // Cita 1..1 Consulta
        modelBuilder.Entity<Consulta>()
            .HasOne(c => c.Cita).WithOne(ci => ci.Consulta)
            .HasForeignKey<Consulta>(c => c.CitaId).OnDelete(DeleteBehavior.Cascade);

        // Consulta 1..* SignoVital
        modelBuilder.Entity<SignoVital>()
            .HasOne(s => s.Consulta).WithMany(c => c.SignosVitales)
            .HasForeignKey(s => s.ConsultaId).OnDelete(DeleteBehavior.Cascade);

        // Consulta 1..* Diagnostico
        modelBuilder.Entity<Diagnostico>()
            .HasOne(d => d.Consulta).WithMany(c => c.Diagnosticos)
            .HasForeignKey(d => d.ConsultaId).OnDelete(DeleteBehavior.Cascade);

        // Diagnostico 1..* Tratamiento
        modelBuilder.Entity<Tratamiento>()
            .HasOne(t => t.Diagnostico).WithMany(d => d.Tratamientos)
            .HasForeignKey(t => t.DiagnosticoId).OnDelete(DeleteBehavior.Cascade);

        // Consulta 1..1 Receta
        modelBuilder.Entity<Receta>()
            .HasOne(r => r.Consulta).WithOne(c => c.Receta)
            .HasForeignKey<Receta>(r => r.ConsultaId).OnDelete(DeleteBehavior.Cascade);

        // Receta 1..* RecetaDetalle
        modelBuilder.Entity<RecetaDetalle>()
            .HasOne(rd => rd.Receta).WithMany(r => r.Detalles)
            .HasForeignKey(rd => rd.RecetaId).OnDelete(DeleteBehavior.Cascade);

        // Medicamento 1..* RecetaDetalle
        modelBuilder.Entity<RecetaDetalle>()
            .HasOne(rd => rd.Medicamento).WithMany(m => m.RecetaDetalles)
            .HasForeignKey(rd => rd.MedicamentoId).OnDelete(DeleteBehavior.Restrict);

        // Índices únicos de negocio
        modelBuilder.Entity<Paciente>().HasIndex(p => p.Cedula).IsUnique();
        modelBuilder.Entity<Medico>().HasIndex(m => m.CedulaProfesional).IsUnique();
        modelBuilder.Entity<Consulta>().HasIndex(c => c.CitaId).IsUnique();
    }
}
