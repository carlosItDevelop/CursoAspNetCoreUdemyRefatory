using Cooperchip.ItDeveloper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cooperchip.ItDeveloper.Data.Mappings
{
    public class EstadoPacienteMap : IEntityTypeConfiguration<EstadoPaciente>
    {
        public void Configure(EntityTypeBuilder<EstadoPaciente> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Descricao)
                .HasColumnType("varchar(30)")
                .IsRequired().HasColumnName("Descricao");

            builder.ToTable(nameof(EstadoPaciente));

            builder.HasMany(p => p.Pacientes)
                .WithOne(p => p.EstadoPaciente);
                //.OnDelete(DeleteBehavior.NoAction);

        }
    }
}
