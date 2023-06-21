using Cooperchip.ItDeveloper.Data.Mappings;
using Cooperchip.ItDeveloper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options){}

    public DbSet<Paciente> Paciente { get; set; }
    public DbSet<EstadoPaciente> EstadoPaciente { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
        {
            property.SetColumnType("varchar(90)");
        }

        modelBuilder.ApplyConfiguration(new EstadoPacienteMap());
        modelBuilder.ApplyConfiguration(new PacienteMap());

        base.OnModelCreating(modelBuilder);
    }

}
