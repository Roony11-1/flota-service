using RentACar.src.Core.Domain.Enums;

namespace RentACar.src.Api.Dto.Request;

public record VehiculoSedanRequest(
    string Patente,
    string Marca,
    string Modelo,
    int Año,
    decimal ConsumoCombustible,
    bool TieneAireAcondicionado,
    bool TieneTransmisionAutomatica,
    TipoCombustible TipoCombustible,
    int CapacidadPasajeros,
    int CantidadPuertas,
    int CapacidadMaleteroLitros) : VehiculoRequest(
    Patente, Marca, Modelo, Año, ConsumoCombustible,
    TieneAireAcondicionado, TieneTransmisionAutomatica, TipoCombustible);