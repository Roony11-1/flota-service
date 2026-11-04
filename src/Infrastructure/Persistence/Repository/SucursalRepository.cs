using RentACar.src.Core.Domain.Models;

namespace RentACar.src.Infrastructure.Persistence.Repository;

public class SucursalRepository
{
    private readonly List<Sucursal> _sucursales = [];

    public Sucursal Crear(Sucursal sucursal)
    {
        _sucursales.Add(sucursal);

        return sucursal;
    }

    public Sucursal Actualizar(Sucursal sucursal)
    {
        var index = _sucursales.FindIndex(s => s.Id == sucursal.Id);

        if (index == -1)
            throw new KeyNotFoundException("Sucursal no encontrada.");

        _sucursales[index] = sucursal;

        return sucursal;
    }

    public Sucursal? FindById(Guid id)
    {
        return _sucursales.FirstOrDefault(s => s.Id == id);
    }

    public IReadOnlyList<Sucursal> FindAll()
    {
        return _sucursales.AsReadOnly();
    }

    public bool Existe(Guid id)
    {
        return _sucursales.Any(s => s.Id == id);
    }

    public void Eliminar(Guid id)
    {
        var sucursal = FindById(id);

        if (sucursal is null)
            throw new KeyNotFoundException("Sucursal no encontrada.");

        _sucursales.Remove(sucursal);
    }

    public bool ExistePorNombre(string nombre)
    {
        return _sucursales.Any(s =>
            string.Equals(
                s.Nombre,
                nombre.Trim(),
                StringComparison.OrdinalIgnoreCase));
    }
}