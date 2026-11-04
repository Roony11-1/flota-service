using RentACar.src.Core.Domain.Enums;

namespace RentACar.src.Api.Dto.Response;

public record VehiculoTurismoResponse(
    Guid Id,
    string Patente,
    string Marca,
    string Modelo,
    int Año,
    decimal ConsumoCombustible,
    bool TieneAireAcondicionado,
    bool TieneTransmisionAutomatica,
    EstadoVehiculo Estado,
    TipoCombustible TipoCombustible,
    int CapacidadPasajeros,
    int CapacidadMaleteroLitros) : VehiculoResponse(
    Id, Patente, Marca, Modelo, Año,
    ConsumoCombustible, TieneAireAcondicionado,
    TieneTransmisionAutomatica, Estado, TipoCombustible);