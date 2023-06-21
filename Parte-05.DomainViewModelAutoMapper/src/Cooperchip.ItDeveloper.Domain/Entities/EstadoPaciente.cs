using Cooperchip.ItDeveloper.Domain.Entities.Base;
using System.Collections;

namespace Cooperchip.ItDeveloper.Domain.Entities
{
    public class EstadoPaciente : EntityBase
    {
        public string? Descricao { get; set; }
        public virtual ICollection<Paciente>? Pacientes { get; set; }
    }
}
