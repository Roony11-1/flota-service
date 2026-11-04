using Microsoft.EntityFrameworkCore;
using RentACar.src.Core.Domain.Models;
using RentACar.src.Infrastructure.Persistence;

namespace RentACar.src.Core.Application.Service;

public class VehiculoService(AppDbContext context)
{
    public async Task<Vehiculo> Crear(Vehiculo vehiculo)
    {
        if (await context.Vehiculos.AnyAsync(v => v.Id == vehiculo.Id))
            throw new InvalidOperationException(
                "Ya existe un vehículo con ese ID.");

        if (await context.Vehiculos.AnyAsync(v =>
            v.Patente == vehiculo.Patente))
            throw new InvalidOperationException(
                "Ya existe un vehículo con esa patente.");

        context.Vehiculos.Add(vehiculo);
        await context.SaveChangesAsync();

        return vehiculo;
    }

    public async Task<Vehiculo> Actualizar(Vehiculo vehiculo)
    {
        var existente = await context.Vehiculos.FindAsync(vehiculo.Id)
            ?? throw new KeyNotFoundException("Vehículo no encontrado.");

        if (await context.Vehiculos.AnyAsync(v =>
            v.Patente == vehiculo.Patente && v.Id != vehiculo.Id))
            throw new InvalidOperationException(
                "Ya existe otro vehículo con esa patente.");

        context.Entry(existente).CurrentValues.SetValues(vehiculo);

        await context.SaveChangesAsync();

        return existente;
    }

    public async Task<Vehiculo> FindById(Guid id)
    {
        return await context.Vehiculos.FindAsync(id)
            ?? throw new KeyNotFoundException("Vehículo no encontrado.");
    }

    public async Task<IReadOnlyList<Vehiculo>> FindAll()
    {
        return await context.Vehiculos.ToListAsync();
    }

    public async Task<IReadOnlyList<Vehiculo>> FindBySucursalId(Guid sucursalId)
    {
        return await context.Vehiculos
            .Where(v => v.SucursalId == sucursalId)
            .ToListAsync();
    }
}