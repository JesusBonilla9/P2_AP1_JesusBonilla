using Microsoft.EntityFrameworkCore;
using P2_AP1_JesusBonilla.DAL;
using P2_AP1_JesusBonilla.Models;
using System.Linq.Expressions;

namespace P2_AP1_JesusBonilla.Services
{
    public class RegistrosServices(IDbContextFactory<Contexto> DbFactory)
    {
        public async Task<List<Registros>> Listar(Expression<Func<Registros, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Registros.Where(criterio).AsNoTracking().ToListAsync();
        }
    }
}
