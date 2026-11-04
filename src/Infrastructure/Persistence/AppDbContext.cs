using Microsoft.EntityFrameworkCore;
using RentACar.src.Core.Domain.Models;

namespace RentACar.src.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Sucursal> Sucursales => Set<Sucursal>();
    public DbSet<Vehiculo> Vehiculos => Set<Vehiculo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigurarSucursal(modelBuilder);
        ConfigurarVehiculo(modelBuilder);
    }

    private static void ConfigurarSucursal(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasMany(s => s.Vehiculos)
                .WithOne()
                .HasForeignKey(v => v.SucursalId)
                .IsRequired(false);
        });
    }

    private static void ConfigurarVehiculo(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.HasKey(v => v.Id);

            entity.Property(v => v.Patente)
                .IsRequired();

            entity.Property(v => v.Marca)
                .IsRequired();

            entity.Property(v => v.Modelo)
                .IsRequired();

            entity.Property(v => v.ConsumoCombustible)
                .HasPrecision(8, 2);

            entity.Property(v => v.Estado)
                .IsRequired();

            entity.Property(v => v.TipoCombustible)
                .IsRequired();

            entity.HasDiscriminator<string>("TipoVehiculo")
                .HasValue<VehiculoSedan>("Sedan")
                .HasValue<VehiculoTurismo>("Turismo");
        });
    }
}