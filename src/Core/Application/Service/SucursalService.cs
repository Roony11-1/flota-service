using Microsoft.EntityFrameworkCore;
using RentACar.src.Core.Domain.Models;
using RentACar.src.Infrastructure.Persistence;

namespace RentACar.src.Core.Application.Service;

public class SucursalService(AppDbContext context)
{
    public async Task<Sucursal> Crear(Sucursal sucursal)
    {
        if (await context.Sucursales.AnyAsync(s => s.Id == sucursal.Id))
            throw new InvalidOperationException(
                "Ya existe una sucursal con ese ID.");

        if (await context.Sucursales.AnyAsync(s =>
            s.Nombre.ToLower() == sucursal.Nombre.ToLower()))
            throw new InvalidOperationException(
                "Ya existe una sucursal con ese nombre.");

        context.Sucursales.Add(sucursal);
        await context.SaveChangesAsync();

        return sucursal;
    }

    public async Task<Sucursal> AgregarVehiculos(
        Guid sucursalId,
        IEnumerable<Guid> vehiculosId)
    {
        Sucursal sucursal = await context.Sucursales
            .Include(s => s.Vehiculos)
            .FirstOrDefaultAsync(s => s.Id == sucursalId)
            ?? throw new KeyNotFoundException("Sucursal no encontrada.");

        List<Vehiculo> vehiculos = await context.Vehiculos
            .Where(v => vehiculosId.Contains(v.Id))
            .ToListAsync();

        if (vehiculos.Count != vehiculosId.Distinct().Count())
            throw new KeyNotFoundException(
                "Uno o más vehículos no fueron encontrados.");

        sucursal.AñadirVehiculos(vehiculos);

        await context.SaveChangesAsync();

        return sucursal;
    }

    public async Task<IReadOnlyList<Sucursal>> FindAll()
    {
        return await context.Sucursales
            .Include(s => s.Vehiculos)
            .ToListAsync();
    }

    public async Task<Sucursal> FindById(Guid sucursalId)
    {
        return await context.Sucursales
            .Include(s => s.Vehiculos)
            .FirstOrDefaultAsync(s => s.Id == sucursalId)
            ?? throw new KeyNotFoundException("Sucursal no encontrada.");
    }
}