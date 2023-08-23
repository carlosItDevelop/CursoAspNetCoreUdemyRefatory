using Cooperchip.ItDeveloper.Domain.Entities;
using Cooperchip.ItDeveloper.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cooperchip.ItDeveloper.Mvc.Controllers
{

    public class PacienteController : BaseController
    {
        private readonly ILogger _logger;
        private readonly ITDeveloperDbContext _context;

        public PacienteController(ITDeveloperDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await MapperListOfModelsToListOfViewModels(await ObterTodos()));
        }

        [HttpGet]
        public async Task<IActionResult> ObterPacientesPorEstadopaciente(Guid estadoPacienteId)
        {
            return View(await MapperListOfModelsToListOfViewModels(await ObterTodosPorEPaciente(estadoPacienteId)));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.EstadoPaciente = new SelectList(await ListaEstadoPaciente(), "Id", "Descricao");
            return await Task.FromResult(View());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PacienteViewModel pacienteViewModel)
        {
            if (ModelState.IsValid)
            { 
                var paciente = new Paciente()
                {
                    Ativo = pacienteViewModel.Ativo,
                    Cpf = pacienteViewModel.Cpf,
                    DataInternacao = pacienteViewModel.DataInternacao,
                    DataNascimento = pacienteViewModel.DataNascimento,
                    Email = pacienteViewModel.Email,
                    EstadoPaciente = pacienteViewModel.EstadoPaciente,
                    EstadoPacienteId = pacienteViewModel.EstadoPacienteId,
                    Id = pacienteViewModel.Id,
                    Nome = pacienteViewModel.Nome,
                    Rg = pacienteViewModel.Rg,
                    RgDataEmissao = pacienteViewModel.RgDataEmissao,
                    RgOrgao = pacienteViewModel.RgOrgao,
                    Sexo = pacienteViewModel.Sexo,
                    TipoPaciente = pacienteViewModel.TipoPaciente,
                    Motivo = pacienteViewModel.Motivo
                };

                try
                {
                    _context.Set<Paciente>().Add(paciente);
                    await _context.SaveChangesAsync();
                    TempData ["Sucesso"] = "Registro Cadastrado com Sucesso!";
                    return Redirect(nameof(Index));

                } catch (Exception)
                {
                    return View(pacienteViewModel);
                }
            }
            ViewBag.EstadoPaciente = new SelectList(await ListaEstadoPaciente(), "Id", "Descricao");
            return View(pacienteViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            return View(await MapperOfModelToViewModel(await ObterPorId(id)));
        }

        #region: Mapper de Model para ViewModel
        private async Task<PacienteViewModel> MapperOfModelToViewModel(Paciente? item)
        {
            PacienteViewModel viewModel = new()
            {
                Ativo = item.Ativo,
                Cpf = item.Cpf,
                DataInternacao = item.DataInternacao,
                DataNascimento = item.DataNascimento,
                Email = item.Email,
                EstadoPaciente = item.EstadoPaciente,
                EstadoPacienteId = item.EstadoPacienteId,
                Id = item.Id,
                Nome = item.Nome,
                Rg = item.Rg,
                RgDataEmissao = item.RgDataEmissao,
                RgOrgao = item.RgOrgao,
                Sexo = item.Sexo,
                TipoPaciente = item.TipoPaciente,
                Motivo = item.Motivo
            };

            return await Task.FromResult(viewModel);

        }
        #endregion

        #region: Mapper de List<Model> para List<ViewModel>
        private async Task<List<PacienteViewModel>> MapperListOfModelsToListOfViewModels(List<Paciente>? pacientes)
        {
            List<PacienteViewModel> listView = new();

            foreach (var item in pacientes)
            {
                listView.Add(new PacienteViewModel
                {
                    Ativo = item.Ativo,
                    Cpf = item.Cpf,
                    DataInternacao = item.DataInternacao,
                    DataNascimento = item.DataNascimento,
                    Email = item.Email,
                    EstadoPaciente = item.EstadoPaciente,
                    EstadoPacienteId = item.EstadoPacienteId,
                    Id = item.Id,
                    Nome = item.Nome,
                    Rg = item.Rg,
                    RgDataEmissao = item.RgDataEmissao,
                    RgOrgao = item.RgOrgao,
                    Sexo = item.Sexo,
                    TipoPaciente = item.TipoPaciente,
                    Motivo = item.Motivo
                });
            }

            return await Task.FromResult(listView);
        }
        #endregion

        #region: Retorna uma lista de todos os Pacientes cadastrados
        private async Task<List<Paciente>> ObterTodos()
        {
            return await _context.Paciente
                .Include(x => x.EstadoPaciente)
                .AsNoTracking().ToListAsync();
        }
        #endregion

        #region: Retorna uma lista de todos os EstadosDePacientes cadastrados
        private async Task<List<EstadoPaciente>> ListaEstadoPaciente()
        {
            return await _context.EstadoPaciente
                .AsNoTracking().ToListAsync();
        }
        #endregion

        #region: Retorna um Paciente cadastrado, filtrado pelo seu Id
        private async Task<Paciente> ObterPorId(Guid id)
        {
            var paciente = await _context.Paciente
                .Include(x => x.EstadoPaciente)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return await Task.FromResult(paciente);

        }
        #endregion

        #region: Obtem todos os Paciente, filtrado por um único EstadoPaciente
        private async Task<List<Paciente>> ObterTodosPorEPaciente(Guid estadoPacienteId)
        {
            return await _context.Paciente
                .Include(ep => ep.EstadoPaciente)
                .AsNoTracking()
                .Where(x => x.EstadoPaciente.Id == estadoPacienteId)
                .OrderBy(order => order.Nome)
                .ToListAsync();
        }
        #endregion

        #region: Retorna um booleano indicando se tem paciente Paciente

        private bool TemPaciente(Guid pacienteId)
        {
            return _context.Paciente.Any(x => x.Id == pacienteId);
        }

        #endregion

    }

}
