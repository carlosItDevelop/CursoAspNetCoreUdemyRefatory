using Cooperchip.ItDeveloper.Domain.Entities;
using Cooperchip.ItDeveloper.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Cooperchip.ItDeveloper.Mvc.Controllers
{

    public class PacienteController : BaseController
    {
        private readonly ILogger _logger;
        private readonly ITDeveloperDbContext _context;

        public PacienteController(ITDeveloperDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PacienteViewModel> listView = await MapperModelsToViewModels();

            return View(listView);
        }




        #region: Mapper de List<Model> para List<ViewModel>
        private async Task<List<PacienteViewModel>> MapperModelsToViewModels()
        {
            var pacientes = await _context.Paciente
                .Include(x => x.EstadoPaciente)
                .AsNoTracking().ToListAsync();

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

            return listView;
        }
        #endregion
    }

}
