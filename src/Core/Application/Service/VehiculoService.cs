using RentACar.src.Core.Domain.Models;
using RentACar.src.Infrastructure.Persistence.Repository;

namespace RentACar.src.Core.Application.Service;

public class VehiculoService
{
    private readonly VehiculoRepository _vehiculoRepository;

    public VehiculoService(VehiculoRepository vehiculoRepository)
    {
        _vehiculoRepository = vehiculoRepository;
    }

    public Vehiculo Crear(Vehiculo vehiculo)
    {
        if (_vehiculoRepository.FindById(vehiculo.Id) is not null)
            throw new InvalidOperationException("Ya existe un vehículo con ese ID.");

        if (_vehiculoRepository.ExistePorPatente(vehiculo.Patente))
            throw new InvalidOperationException("Ya existe un vehículo con esa patente.");

        return _vehiculoRepository.Crear(vehiculo);
    }

    public Vehiculo Actualizar(Vehiculo vehiculo)
    {
        var existente = _vehiculoRepository.FindById(vehiculo.Id)
            ?? throw new KeyNotFoundException("Vehículo no encontrado.");

        if (_vehiculoRepository.ExistePorPatente(vehiculo.Patente, vehiculo.Id))
            throw new InvalidOperationException("Ya existe otro vehículo con esa patente.");

        return _vehiculoRepository.Actualizar(vehiculo);
    }

    public Vehiculo ObtenerPorId(Guid id)
    {
        return _vehiculoRepository.FindById(id)
            ?? throw new KeyNotFoundException("Vehículo no encontrado.");
    }

    public IReadOnlyList<Vehiculo> FindAll()
    {
        return _vehiculoRepository.FindAll();
    }

    public IReadOnlyList<Vehiculo> FindBySucursalId(Guid sucursalId)
    {
        return _vehiculoRepository.FindBySucursalId(sucursalId);
    }
}