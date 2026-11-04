using RentACar.src.Core.Domain.Enums;

namespace RentACar.src.Core.Domain.Models;

public class VehiculoTurismo(
    string patente,
    string marca,
    string modelo,
    int año,
    decimal consumoCombustible,
    bool tieneAireAcondicionado,
    bool tieneTransmisionAutomatica,
    int capacidadPasajeros,
    int capacidadMaleteroLitros,
    TipoCombustible tipoCombustible)
    : Vehiculo(
        patente,
        marca,
        modelo,
        año,
        consumoCombustible,
        tieneAireAcondicionado,
        tieneTransmisionAutomatica,
        tipoCombustible)
{
    public int CapacidadPasajeros { get; private set; } = capacidadPasajeros;
    public int CapacidadMaleteroLitros { get; private set; } = capacidadMaleteroLitros;
}