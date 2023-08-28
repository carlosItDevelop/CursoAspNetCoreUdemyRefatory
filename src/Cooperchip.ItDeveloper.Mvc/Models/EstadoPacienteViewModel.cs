using Cooperchip.ItDeveloper.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cooperchip.ItDeveloper.Mvc.Models
{
    public class EstadoPacienteViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        [StringLength(30, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres!", MinimumLength = 3)]
        public string? Descricao { get; set; }

        public virtual ICollection<Paciente>? Pacientes { get; set; }
    }
}
