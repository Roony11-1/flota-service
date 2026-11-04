using RentACar.src.Api.Dto.Request;
using RentACar.src.Api.Dto.Response;
using RentACar.src.Core.Domain.Models;

namespace RentACar.src.Api.Mapper;

public class VehiculoMapper
{
    public VehiculoResponse ToResponse(Vehiculo vehiculo)
    {
        return vehiculo switch
        {
            VehiculoSedan sedan => new VehiculoSedanResponse(
                sedan.Id,
                sedan.Patente,
                sedan.Marca,
                sedan.Modelo,
                sedan.Año,
                sedan.ConsumoCombustible,
                sedan.TieneAireAcondicionado,
                sedan.TieneTransmisionAutomatica,
                sedan.Estado,
                sedan.TipoCombustible,
                sedan.CapacidadPasajeros,
                sedan.CantidadPuertas,
                sedan.CapacidadMaleteroLitros),
            VehiculoTurismo turismo => new VehiculoTurismoResponse(
                turismo.Id,
                turismo.Patente,
                turismo.Marca,
                turismo.Modelo,
                turismo.Año,
                turismo.ConsumoCombustible,
                turismo.TieneAireAcondicionado,
                turismo.TieneTransmisionAutomatica,
                turismo.Estado,
                turismo.TipoCombustible,
                turismo.CapacidadPasajeros,
                turismo.CapacidadMaleteroLitros),
            _ => throw new NotSupportedException($"Tipo de vehículo no soportado: {vehiculo.GetType().Name}")
        };
    }

    public Vehiculo FromRequest(VehiculoRequest request)
    {
        return request switch
        {
            VehiculoSedanRequest sedan => new VehiculoSedan(
                sedan.Patente,
                sedan.Marca,
                sedan.Modelo,
                sedan.Año,
                sedan.ConsumoCombustible,
                sedan.TieneAireAcondicionado,
                sedan.TieneTransmisionAutomatica,
                sedan.CapacidadPasajeros,
                sedan.CantidadPuertas,
                sedan.CapacidadMaleteroLitros,
                sedan.TipoCombustible),
            VehiculoTurismoRequest turismo => new VehiculoTurismo(
                turismo.Patente,
                turismo.Marca,
                turismo.Modelo,
                turismo.Año,
                turismo.ConsumoCombustible,
                turismo.TieneAireAcondicionado,
                turismo.TieneTransmisionAutomatica,
                turismo.CapacidadPasajeros,
                turismo.CapacidadMaleteroLitros,
                turismo.TipoCombustible),
            _ => throw new NotSupportedException($"Tipo de vehículo no soportado: {request.GetType().Name}")
        };
    }
}