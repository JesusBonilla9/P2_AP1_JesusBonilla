using Microsoft.EntityFrameworkCore;
using P2_AP1_JesusBonilla.DAL;
using P2_AP1_JesusBonilla.Models;
using System.Linq.Expressions;

namespace P2_AP1_JesusBonilla.Services;

public class PedidosServices(IDbContextFactory<Contexto> DbFactory)
{
    public enum TipoOperacion
    {
        Suma = 1,
        Resta = 2
    }

    public async Task<bool> Guardar(Pedidos pedido)
    {
        if (!await Existe(pedido.PedidoId))
            return await Insertar(pedido);
        else
            return await Modificar(pedido);
    }

    public async Task<bool> Existe(int pedidoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Pedidos.AnyAsync(p => p.PedidoId == pedidoId);
    }

    private async Task<bool> Insertar(Pedidos pedido)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Pedidos.Add(pedido);
        await AfectarProductos(pedido.Detalles.ToArray(), TipoOperacion.Resta);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Pedidos pedido)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var original = await contexto.Pedidos
            .Include(p => p.Detalles)
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.PedidoId == pedido.PedidoId);

        if (original == null)
            return false;

        await AfectarProductos(original.Detalles.ToArray(), TipoOperacion.Suma);
        contexto.PedidosDetalles.RemoveRange(original.Detalles);
        contexto.Update(pedido);
        await AfectarProductos(pedido.Detalles.ToArray(), TipoOperacion.Resta);
        return await contexto.SaveChangesAsync() > 0;
    }
    private async Task AfectarProductos(PedidosDetalle[] detalles, TipoOperacion operacion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        foreach (var item in detalles)
        {
            var producto = await contexto.Componentes.SingleAsync(p => p.ComponenteId == item.ComponenteId);
            if (operacion == TipoOperacion.Resta)
                producto.Existencia -= item.Cantidad;
            else
                producto.Existencia += item.Cantidad;
            await contexto.SaveChangesAsync();
        }
    }
    public async Task<Pedidos?> Buscar(int pedidoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Pedidos
            .Include(p => p.Detalles)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PedidoId == pedidoId);
    }

    public async Task<bool> Eliminar(int pedidoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var pedido = await Buscar(pedidoId);
        if (pedido == null)
            return false;

        await AfectarProductos(pedido.Detalles.ToArray(), TipoOperacion.Suma);
        contexto.PedidosDetalles.RemoveRange(pedido.Detalles);
        contexto.Pedidos.Remove(pedido);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Pedidos>> Listar(Expression<Func<Pedidos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Pedidos
            .Where(criterio)
            .Include(p => p.Detalles)
            .AsNoTracking()
            .ToListAsync();
    }
}

