namespace RentACar.src.Core.Domain.Models;

public class Sucursal
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Nombre { get; private set; } = string.Empty;
    public List<Vehiculo> Vehiculos { get; private set; } = [];

    public Sucursal(string nombre)
    {
        CambiarNombre(nombre);
    }

    /// <summary>
    /// Modifica el nombre de la sucursal.
    /// </summary>
    /// <param name="nombre">Nuevo nombre de la sucursal.</param>
    /// <exception cref="ArgumentException">
    /// Se produce cuando el nombre está vacío o supera los 100 caracteres.
    /// </exception>
    public void CambiarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre no puede estar vacío");

        if (nombre.Length > 100)
            throw new ArgumentException("El nombre noo puede superar los 100 caracteres");

        Nombre = nombre.Trim();
    }

    /// <summary>
    /// Añade un vehículo a la sucursal y establece su asociación con ella.
    /// </summary>
    /// <param name="vehiculo">Vehículo que se desea incorporar.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce cuando el vehículo es null.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce cuando el vehículo ya está agregado o pertenece a otra sucursal.
    /// </exception>
    public void AñadirVehiculo(Vehiculo vehiculo)
    {
        ArgumentNullException.ThrowIfNull(vehiculo);

        if (Vehiculos.Any(v => v.Id == vehiculo.Id))
            throw new InvalidOperationException("El vehículo ya está agregado a esta sucursal.");

        vehiculo.AsignarASucursal(Id);
        Vehiculos.Add(vehiculo);
    }

    /// <summary>
    /// Añade múltiples vehículos a la sucursal.
    /// </summary>
    /// <param name="vehiculos">Colección de vehículos que se desean incorporar.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce cuando la colección es null.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Se produce cuando alguno de los vehículos ya está agregado o pertenece a otra sucursal.
    /// </exception>
    public void AñadirVehiculos(IEnumerable<Vehiculo> vehiculos)
    {
        foreach (var vehiculo in vehiculos)
            AñadirVehiculo(vehiculo);
    }
}