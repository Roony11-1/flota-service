using Microsoft.AspNetCore.Mvc;
using RentACar.src.Api.Dto.Request;
using RentACar.src.Api.Mapper;
using RentACar.src.Core.Application.Service;
using RentACar.src.Core.Domain.Models;

namespace RentACar.src.Api.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class SucursalController : ControllerBase
{
    private readonly SucursalService _sucursalService;
    private readonly SucursalMapper _sucursalMapper;

    public SucursalController(SucursalService sucursalService, SucursalMapper sucursalMapper)
    {
        _sucursalService = sucursalService;
        _sucursalMapper = sucursalMapper;
    }

    [HttpPost("crear")]
    public IActionResult Crear([FromBody] SucursalRequest request)
    {
        var sucursal = _sucursalMapper.FromRequest(request);

        var sucursalCreada = _sucursalService.Crear(sucursal);

        var response = _sucursalMapper.ToResponse(sucursalCreada);

        return CreatedAtAction(nameof(Crear), response);
    }

    [HttpPost("{sucursalId:guid}/vehiculos")]
    public IActionResult AñadirVehiculo(
        Guid sucursalId,
        [FromBody] IEnumerable<AsignarVehiculoRequest> vehiculosIds)
    {
        Sucursal sucursal = _sucursalService.AgregarVehiculos(
            sucursalId,
            vehiculosIds.Select(v => v.VehiculoId)
        );

        var response = _sucursalMapper.ToResponse(sucursal);

        return Ok(response);
    }

    [HttpGet]
    public IActionResult FindAll()
    {
        var sucursales = _sucursalService.FindAll();

        var response = sucursales
            .Select(_sucursalMapper.ToResponse)
            .ToList();

        return Ok(response);
    }
}