using Microsoft.EntityFrameworkCore;

namespace Cooperchip.ItDeveloper.Mvc.Extensions.ViewComponents.Helpers
{
    public static class Util
    {
        public static int TotReg(ITDeveloperDbContext ctx)
        {
            return (from paciente in ctx.Paciente.AsNoTracking() select paciente).Count();
        }

        public static decimal GetNumRegEstado(ITDeveloperDbContext ctx, string estado)
        {
            return ctx.Paciente.AsNoTracking()
                .Count(x => x.EstadoPaciente.Descricao.Contains(estado));
        }



    }
}
