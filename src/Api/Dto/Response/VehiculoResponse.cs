using RentACar.src.Core.Domain.Enums;

namespace RentACar.src.Api.Dto.Response;

public abstract record VehiculoResponse(
    Guid Id,
    string Patente,
    string Marca,
    string Modelo,
    int Año,
    decimal ConsumoCombustible,
    bool TieneAireAcondicionado,
    bool TieneTransmisionAutomatica,
    EstadoVehiculo Estado,
    TipoCombustible TipoCombustible);