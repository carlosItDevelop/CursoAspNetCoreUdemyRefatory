using System.Collections;

namespace Cooperchip.ItDeveloper.Domain.Entities
{
    public class EstadoPaciente
    {
        public Guid Id { get; set; }
        public string? Descricao { get; set; }
        public virtual ICollection<Paciente>? Pacientes { get; set; }
    }
}
