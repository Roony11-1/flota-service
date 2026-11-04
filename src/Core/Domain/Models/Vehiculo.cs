using RentACar.src.Core.Domain.Enums;

namespace RentACar.src.Core.Domain.Models;

public abstract class Vehiculo(
    string patente,
    string marca,
    string modelo,
    int año,
    decimal consumoCombustible,
    bool tieneAireAcondicionado,
    bool tieneTransmisionAutomatica,
    TipoCombustible tipoCombustible)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid? SucursalId { get; private set; }
    public string Patente { get; private set; } = patente;
    public string Marca { get; private set; } = marca;
    public string Modelo { get; private set; } = modelo;
    public int Año { get; private set; } = año;
    public decimal ConsumoCombustible { get; private set; } = consumoCombustible;
    public bool TieneAireAcondicionado { get; private set; } = tieneAireAcondicionado;
    public bool TieneTransmisionAutomatica { get; private set; } = tieneTransmisionAutomatica;
    public EstadoVehiculo Estado { get; private set; } = EstadoVehiculo.Disponible;
    public TipoCombustible TipoCombustible { get; private set; } = tipoCombustible;

    /// <summary>
    /// Crea la relación con la sucursal.
    /// </summary>
    /// <param name="sucursalId">Id de la sucursal a asignar.</param>
    /// <exception cref="InvalidOperationException">
    /// Se produce cuando la sucursal ya tiene una sucursal asignada
    /// </exception>
    public void AsignarASucursal(Guid sucursalId)
    {
        if (SucursalId.HasValue)
            throw new InvalidOperationException("El vehículo ya pertenece a una sucursal.");

        SucursalId = sucursalId;
    }

    /// <summary>
    /// Desvincula el vehículo de su sucursal actual.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Se produce cuando el vehículo no pertenece a ninguna sucursal.
    /// </exception>
    public void DesasignarDeSucursal()
    {
        if (!SucursalId.HasValue)
            throw new InvalidOperationException("El vehículo no pertenece a ninguna sucursal.");

        SucursalId = null;
    }

    /// <summary>
    /// Traslada el vehículo a otra sucursal.
    /// </summary>
    /// <param name="sucursalId">Identificador de la sucursal de destino.</param>
    /// <exception cref="ArgumentException">
    /// Se produce cuando el identificador de destino es inválido.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce cuando el vehículo ya pertenece a la sucursal indicada.
    /// </exception>
    public void TraspasarASucursal(Guid sucursalId)
    {
        if (sucursalId == Guid.Empty)
            throw new ArgumentException("El identificador de sucursal no puede estar vacío.");

        if (SucursalId == sucursalId)
            throw new InvalidOperationException("El vehículo ya pertenece a esta sucursal.");

        SucursalId = sucursalId;
    }
}