using Cooperchip.ItDeveloper.Mvc.Extensions.ViewComponents.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Cooperchip.ItDeveloper.Mvc.Extensions.ViewComponents.Cabecalho
{
    [ViewComponent(Name = "Cabecalho")]
    public class CabecalhoModulosViewComponents : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string titulo, string subtitulo)
        {

            Modulo model = new()
            {
                Titulo = titulo,
                Subtitulo = subtitulo
            };

            return await Task.FromResult(View(model));
        }
    }
}
