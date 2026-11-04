namespace RentACar.src.Api.Dto.Response;

public record SucursalResponse(Guid Id, string Nombre, List<VehiculoResponse> Vehiculos);