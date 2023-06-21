using Microsoft.AspNetCore.Mvc;
using System.Collections;

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

        //[HttpGet("detalhes-de-paciente/{id:guid}")]
        [HttpGet("detalhes-de-paciente/{id}")]
        public IActionResult DetalhesDePaciente(string id)
        {
            var paciente = ObterPaciente(id);
            if(paciente == null) return NotFound();

            return View(paciente);
        }

        //[Route("adicionar-paciente")]
        [HttpGet("adicionar-paciente")]
        public IActionResult AdicionarPaciente()
        {
            return View();
        }

        [HttpPost("adicionar-paciente")]
        public IActionResult AdicionarPaciente(Paciente model)
        {
            var paciente = model;

            return RedirectToAction(nameof(Index));
        }

        #region: Obter Pacientes
        private List<Paciente> ObterPacientes()
        {
            var pacientes = new List<Paciente>()
            {
                new Paciente
                {
                    Nome = "Ricardo de Souza",
                    Cpf = "48878965987",
                    Telefones = new List<Telefone>()
                    {
                        new Telefone() { Id = Guid.NewGuid(), TipoDeTelefone = "Residencial", Numero = "2145684478"},
                        new Telefone() { Id = Guid.NewGuid(), TipoDeTelefone = "Comercial", Numero = "1190084451"},
                        new Telefone() { Id = Guid.NewGuid(), TipoDeTelefone = "Celular", Numero = "6495604403"}
                    }
                },
                new Paciente
                {
                    Nome = "José Marinho",
                    Cpf = "78878965900",
                    Telefones = new List<Telefone>()
                    {
                        new Telefone() { Id = Guid.NewGuid(), TipoDeTelefone = "Residencial", Numero = "2198684401"},
                        new Telefone() { Id = Guid.NewGuid(), TipoDeTelefone = "Comercial", Numero = "1199084402"},
                        new Telefone() { Id = Guid.NewGuid(), TipoDeTelefone = "Celular", Numero = "6499604403"}
                    }
                },
                new Paciente
                {
                    Nome = "Maria do Carmo",
                    Cpf = "77878965901",
                    Telefones = new List<Telefone>()
                    {
                        new Telefone() { Id = Guid.NewGuid(), TipoDeTelefone = "Residencial", Numero = "2195684411"},
                        new Telefone() { Id = Guid.NewGuid(), TipoDeTelefone = "Comercial", Numero = "1195084422"},
                        new Telefone() { Id = Guid.NewGuid(), TipoDeTelefone = "Celular", Numero = "6495604433"}
                    }
                }
            };
            return pacientes;
        }

        private Paciente? ObterPaciente(string id)
        {
            var pacientes = ObterPacientes();
            if(id != string.Empty && pacientes != null) // O que há de errado com esta linha? (Lista pode ser vazia, mas não nula.)
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

    // Com construtor
    public class Paciente
    {
        public Paciente()
        {
            Id = Guid.NewGuid();
            Telefones = new List<Telefone>();
        }

        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public ICollection<Telefone>? Telefones { get; set; }
    }

    // Sem Construtor
    public class Telefone
    {
        public Guid Id { get; set; }
        public string? TipoDeTelefone { get; set; }
        public string? Numero { get; set; }
    }

}
