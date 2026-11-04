using RentACar.src.Api.Dto.Request;
using RentACar.src.Api.Dto.Response;
using RentACar.src.Core.Domain.Models;

namespace RentACar.src.Api.Mapper;

public class SucursalMapper
{
    private readonly VehiculoMapper _vehiculoMapper;

    public SucursalMapper(VehiculoMapper vehiculoMapper)
    {
        _vehiculoMapper = vehiculoMapper;
    }

    public Sucursal FromRequest(SucursalRequest sucursalRequest)
    {
        return new Sucursal(sucursalRequest.Nombre);
    }

    public SucursalResponse ToResponse(Sucursal sucursal)
    {
        var vehiculos = sucursal.Vehiculos
            .Select(_vehiculoMapper.ToResponse)
            .ToList();

        return new SucursalResponse(sucursal.Id, sucursal.Nombre, vehiculos);
    }
}