using Cooperchip.ItDeveloper.Domain.Entities;
using Cooperchip.ItDeveloper.Mvc.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cooperchip.ItDeveloper.Mvc.Controllers
{

    public class PacienteController : BaseController
    {
        private readonly ITDeveloperDbContext _context;

        public PacienteController(ITDeveloperDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await MapperListOfModelsToListOfViewModels(await ObterTodos()));
        }

        [HttpGet]
        public async Task<IActionResult> ReportForEstadoPaciente(Guid estadoPacienteId)
        {

            return View(await MapperListOfModelsToListOfViewModels(await ObterTodosPorEPaciente(estadoPacienteId)));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            return View(await MapperOfModelToViewModel(await ObterPorId(id)));
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
                Paciente? paciente = await MapperOfTheViewModelToModel(pacienteViewModel);

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
        public async Task<IActionResult> Edit(Guid id)
        {
            Paciente? paciente = await _context.Set<Paciente>()
                .Include(x => x.EstadoPaciente)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if(paciente is null)
            {
                return NotFound();
            }

            ViewBag.EstadoPaciente = new SelectList(await ListaEstadoPaciente(), "Id", "Descricao");

            return View(await MapperOfModelToViewModel(paciente));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PacienteViewModel pacienteVM)
        {
            if (id != pacienteVM.Id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    var paciente = await MapperOfTheViewModelToModel(pacienteVM);
                    paciente.Id = pacienteVM.Id;

                    _context.Entry(paciente).State = EntityState.Modified;
                    //_context.Set<Paciente>().Update(paciente);


                    await _context.SaveChangesAsync();

                    TempData ["Sucesso"] = "Registro Atualizado com Sucesso!";
                    return RedirectToAction(nameof(Index));

                } catch (DbUpdateConcurrencyException)
                {
                    if (!TemPaciente(pacienteVM.Id))
                    {
                        return NotFound("Paciente não encontrado!");
                    } else
                    {
                        TempData ["Error"] = "Conflito ao Atualizar Paciente. Contate o Administrador do Sistema!";
                        return View(pacienteVM);
                    }
                }
            }

            ViewBag.EstadoPaciente = new SelectList(await ListaEstadoPaciente(), "Id", "Descricao");
            return View(pacienteVM);

        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            Paciente? paciente = await _context.Set<Paciente>()
                .Include(x => x.EstadoPaciente)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (paciente is null)
            {
                return NotFound();
            }

            return View(await MapperOfModelToViewModel(paciente));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            Paciente? paciente = await _context.Set<Paciente>()
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (paciente is null)
            {
                TempData ["Error"] = $"Registro com ID: {id}, não foi encontrado!";
                return NotFound();
            }

            _context.Entry(paciente).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            TempData ["Sucesso"] = "Registro Deletado com Sucesso!";

            return RedirectToAction(nameof(Index));
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
            var lista = await _context.Paciente
                .Include(x => x.EstadoPaciente)
                .AsNoTracking().ToListAsync();

            return await Task.FromResult(lista);
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
        private async Task<Paciente?> ObterPorId(Guid id)
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

            var lista = await _context.Paciente
                .Include(ep => ep.EstadoPaciente)
                .AsNoTracking()
                .Where(x => x.EstadoPaciente.Id == estadoPacienteId)
                .OrderBy(order => order.Nome)
                .ToListAsync();

            return await Task.FromResult(lista);
        }
        #endregion


        #region: Mapper Of The ViewModel To Model
        private async Task<Paciente> MapperOfTheViewModelToModel(PacienteViewModel pacienteViewModel)
        {
            var paciente = new Paciente()
            {
                //Id = Guid.NewGuid(), // Herda de EntityBase

                Ativo = pacienteViewModel.Ativo,
                Cpf = pacienteViewModel.Cpf,
                DataInternacao = pacienteViewModel.DataInternacao,
                DataNascimento = pacienteViewModel.DataNascimento,
                Email = pacienteViewModel.Email,
                EstadoPaciente = pacienteViewModel.EstadoPaciente,
                EstadoPacienteId = pacienteViewModel.EstadoPacienteId,
                Nome = pacienteViewModel.Nome,
                Rg = pacienteViewModel.Rg,
                RgDataEmissao = pacienteViewModel.RgDataEmissao,
                RgOrgao = pacienteViewModel.RgOrgao,
                Sexo = pacienteViewModel.Sexo,
                TipoPaciente = pacienteViewModel.TipoPaciente,
                Motivo = pacienteViewModel.Motivo
            };
            return await Task.FromResult(paciente);
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
