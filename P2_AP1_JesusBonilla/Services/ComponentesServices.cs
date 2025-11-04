using Microsoft.EntityFrameworkCore;
using P2_AP1_JesusBonilla.DAL;
using P2_AP1_JesusBonilla.Models;
using System.Linq.Expressions;

namespace P2_AP1_JesusBonilla.Services
{
    public class ComponentesServices(IDbContextFactory<Contexto> DbFactory)
    {
        public async Task<Componente?> Buscar(int componenteId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Componentes
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ComponenteId == componenteId);
        }

        public async Task<List<Componente>> ListarComponentes(Expression<Func<Componente, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Componentes
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
