using System.Text.Json.Serialization;
using RentACar.src.Core.Domain.Enums;

namespace RentACar.src.Api.Dto.Request;


[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(VehiculoSedanRequest), "sedan")]
[JsonDerivedType(typeof(VehiculoTurismoRequest), "turismo")]
public abstract record VehiculoRequest(
    string Patente,
    string Marca,
    string Modelo,
    int Año,
    decimal ConsumoCombustible,
    bool TieneAireAcondicionado,
    bool TieneTransmisionAutomatica,
    TipoCombustible TipoCombustible);