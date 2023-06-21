using Cooperchip.ItDeveloper.Domain.Emums;
using Cooperchip.ItDeveloper.Domain.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cooperchip.ItDeveloper.Mvc.Models
{
    public class PacienteViewModel
    {
        [Key]
        [DisplayName(displayName: "Id do Paciente")]
        public Guid Id { get; set; }

        public Guid EstadoPacienteId { get; set; }
        public virtual EstadoPaciente? EstadoPaciente { get; set; }

        public string? Nome { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.DateTime, ErrorMessage = "O campo {0} está inválido")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Data de Internação")]
        [DataType(DataType.DateTime, ErrorMessage = "O campo {0} está inválido")]
        public DateTime DataInternacao { get; set; }

        public string? Email { get; set; }
        public bool Ativo { get; set; }
        public string? Cpf { get; set; }
        public TipoPaciente TipoPaciente { get; set; }
        public Sexo Sexo { get; set; }
        public string? Rg { get; set; }
        public string? RgOrgao { get; set; }

        [Display(Name = "Data de Emissão da RG")]
        [DataType(DataType.DateTime, ErrorMessage = "O campo {0} está inválido")]
        public DateTime RgDataEmissao { get; set; }

        public string? Motivo { get; set; }

        public override string ToString()
        {
            return Id.ToString() + "  -  " + Nome;
        }
    }
}
