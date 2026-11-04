using RentACar.src.Core.Domain.Models;
using RentACar.src.Infrastructure.Persistence.Repository;

namespace RentACar.src.Core.Application.Service;

public class SucursalService
{
    private readonly SucursalRepository _sucursalRepository;
    private readonly VehiculoRepository _vehiculoRepository;

    public SucursalService(SucursalRepository sucursalRepository, VehiculoRepository vehiculoRepository)
    {
        _sucursalRepository = sucursalRepository;
        _vehiculoRepository = vehiculoRepository;
    }

    public Sucursal Crear(Sucursal sucursal)
    {
        if (_sucursalRepository.FindById(sucursal.Id) is not null)
            throw new InvalidOperationException("Ya existe una sucursal con ese ID.");

        if (_sucursalRepository.ExistePorNombre(sucursal.Nombre))
            throw new InvalidOperationException("Ya existe una sucursal con ese nombre.");

        return _sucursalRepository.Crear(sucursal);
    }

    public Sucursal AgregarVehiculos(Guid sucursalId, IEnumerable<Guid> vehiculosId)
    {
        Sucursal sucursal = _sucursalRepository.FindById(sucursalId)
            ?? throw new KeyNotFoundException("Sucursal no encontrada");

        List<Vehiculo> vehiculos = [];

        foreach (Guid vehiculoId in vehiculosId)
        {
            Vehiculo vehiculo = _vehiculoRepository.FindById(vehiculoId)
                ?? throw new KeyNotFoundException($"Vehiculo {vehiculoId} no encontrado");

            vehiculos.Add(vehiculo);
        }

        sucursal.AñadirVehiculos(vehiculos);

        return _sucursalRepository.Actualizar(sucursal);
    }
    

    public IReadOnlyList<Sucursal> FindAll()
    {
        return _sucursalRepository.FindAll();
    }
}