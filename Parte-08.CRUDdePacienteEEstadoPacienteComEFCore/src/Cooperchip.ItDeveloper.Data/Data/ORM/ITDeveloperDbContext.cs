using Cooperchip.ItDeveloper.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ITDeveloperDbContext : DbContext
{
    public ITDeveloperDbContext(DbContextOptions<ITDeveloperDbContext> options) 
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

        //modelBuilder.ApplyConfiguration(new EstadoPacienteMap());
        //modelBuilder.ApplyConfiguration(new PacienteMap());

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ITDeveloperDbContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }

        base.OnModelCreating(modelBuilder);
    }

}
