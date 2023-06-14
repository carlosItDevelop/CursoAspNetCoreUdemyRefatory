
using Microsoft.AspNetCore.Mvc;

namespace Cooperchip.ItDeveloper.Mvc.Controllers
{
    [Route("gestao-de-paciente")]
    [Route("gestao-de-pacientes")]
    public class PacienteController : BaseController
    {
        [Route("pacientes")]
        [Route("obter-pacientes")]
        public IActionResult Index()
        {
            var pacientes = ObterPacientes();

            return View(pacientes);
        }

        [HttpGet("detalhe-de-paciente/{id}")]
        public IActionResult DetalheDePaciente(string id)
        {
            var paciente = ObterPaciente(id);
            if(paciente == null) return NotFound();

            return View(paciente);
        }


        //[Route("adicionar-paciente")]
        [HttpPost("adicionar-paciente")]
        public async Task<IActionResult> AdicionarPaciente()
        {
            return View();
        }

        #region: Lista de Paciente
        private List<Paciente> ObterPacientes()
        {
            var pacientes = new List<Paciente>()
            {
                new Paciente
                {
                    Nome = "Ricardo de Souza",
                    Cpf = "48857865898",
                    Telefones = new List<Telefone>()
                    {
                        new Telefone { Id = Guid.NewGuid(), TipoDetelefone = "Residencial", Numero = "21989655487" },
                        new Telefone { Id = Guid.NewGuid(), TipoDetelefone = "Comercial", Numero = "11979655480" },
                        new Telefone { Id = Guid.NewGuid(), TipoDetelefone = "Celular", Numero = "64969655481" },
                    }
                },
                new Paciente
                {
                    Nome = "José Mariano",
                    Cpf = "78857865800",
                    Telefones = new List<Telefone>()
                    {
                        new Telefone { Id = Guid.NewGuid(), TipoDetelefone = "Residencial", Numero = "21889655487" },
                        new Telefone { Id = Guid.NewGuid(), TipoDetelefone = "Comercial", Numero = "11779655480" },
                        new Telefone { Id = Guid.NewGuid(), TipoDetelefone = "Celular", Numero = "71669655481" },
                    }
                },
                new Paciente
                {
                    Nome = "Maria do Carmo",
                    Cpf = "75857865000",
                    Telefones = new List<Telefone>()
                    {
                        new Telefone { Id = Guid.NewGuid(), TipoDetelefone = "Residencial", Numero = "21909655483" },
                        new Telefone { Id = Guid.NewGuid(), TipoDetelefone = "Comercial", Numero = "11919655484" },
                        new Telefone { Id = Guid.NewGuid(), TipoDetelefone = "Celular", Numero = "64929655485" },
                    }
                },
            };
            return pacientes;
        }

        private Paciente? ObterPaciente(string id)
        {
            var pacientes = ObterPacientes();
            if (id != string.Empty && pacientes != null) // O que há de errado com esta linha? (Lista pode ser vazia, mas não nula.)
            {
                var paciente = pacientes.FirstOrDefault(p => p.Nome.Contains(id));
                if (paciente != null)
                {
                    return paciente;
                }
            }
            return null;
        }

        #endregion

    }

    public class Paciente
    {
        public Paciente()
        {
            Id = Guid.NewGuid();
            Telefones = new HashSet<Telefone>();
        }

        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public ICollection<Telefone> Telefones { get; set; }
    }

    public class Telefone
    {
        public Guid Id { get; set; }
        public string? TipoDetelefone { get; set; }
        public string? Numero { get; set; }
    }

}
