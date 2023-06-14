using System.ComponentModel;

namespace Cooperchip.ItDeveloper.Domain.Emums
{
    public enum TipoPaciente
    {
        [Description("Conveniado")] Conveniado = 1,
        [Description("Particular")] Particular,
        [Description("Outros")] Outros
    }

}
