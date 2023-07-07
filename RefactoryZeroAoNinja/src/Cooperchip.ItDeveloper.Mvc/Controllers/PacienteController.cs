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
            return View();
        }

        //[HttpGet("detalhes-de-paciente/{id:guid}")]
        [HttpGet("detalhes-de-paciente/{id}")]
        public IActionResult DetalhesDePaciente(string id)
        {
            return View();
        }



    }


}
