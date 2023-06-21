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

        base.OnModelCreating(modelBuilder);
    }

}
