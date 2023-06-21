using Cooperchip.ItDeveloper.Domain.Entities;

namespace Cooperchip.ItDeveloper.Mvc.Models
{
    public class EstadoPacienteViewModel
    {
        public Guid Id { get; set; }
        public string? Descricao { get; set; }
        public virtual ICollection<Paciente>? Pacientes { get; set; }
    }
}
