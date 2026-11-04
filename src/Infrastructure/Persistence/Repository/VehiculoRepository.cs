using RentACar.src.Core.Domain.Models;

namespace RentACar.src.Infrastructure.Persistence.Repository;

public class VehiculoRepository
{
    private readonly List<Vehiculo> _vehiculos = [];

    public Vehiculo Crear(Vehiculo vehiculo)
    {
        _vehiculos.Add(vehiculo);

        return vehiculo;
    }

    public Vehiculo Actualizar(Vehiculo vehiculo)
    {
        var index = _vehiculos.FindIndex(v => v.Id == vehiculo.Id);

        if (index == -1)
            throw new KeyNotFoundException("Vehículo no encontrado.");

        _vehiculos[index] = vehiculo;

        return vehiculo;
    }

    public Vehiculo? FindById(Guid id)
    {
        return _vehiculos.FirstOrDefault(v => v.Id == id);
    }

    public IReadOnlyList<Vehiculo> FindAll()
    {
        return _vehiculos.AsReadOnly();
    }

    public IReadOnlyList<Vehiculo> FindBySucursalId(Guid sucursalId)
    {
        return _vehiculos
            .Where(v => v.SucursalId == sucursalId)
            .ToList()
            .AsReadOnly();
    }

    public bool ExistePorPatente(string patente, Guid? excluirId = null)
    {
        return _vehiculos.Any(v =>
            v.Id != excluirId &&
            string.Equals(
                v.Patente,
                patente.Trim(),
                StringComparison.OrdinalIgnoreCase));
    }
}