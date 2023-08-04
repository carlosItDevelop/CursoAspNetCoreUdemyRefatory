using Microsoft.AspNetCore.Mvc;

namespace Cooperchip.ItDeveloper.Mvc.Extensions.ViewComponents.EstadoPaciente
{
    [ViewComponent(Name = "StatusPaciente")]
    public class StatusPacienteViewComponents : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
