using Microsoft.AspNetCore.Mvc;
using RentACar.src.Api.Dto.Request;
using RentACar.src.Api.Mapper;
using RentACar.src.Core.Application.Service;
using RentACar.src.Core.Domain.Models;

namespace RentACar.src.Api.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class SucursalController(
    SucursalService sucursalService,
    SucursalMapper sucursalMapper) : ControllerBase
{
    [HttpPost("crear")]
    public async Task<IActionResult> Crear(
        [FromBody] SucursalRequest request)
    {
        var sucursal = sucursalMapper.FromRequest(request);

        var sucursalCreada = await sucursalService.Crear(sucursal);

        var response = sucursalMapper.ToResponse(sucursalCreada);

        return CreatedAtAction(
            nameof(FindById),
            new { id = sucursalCreada.Id },
            response);
    }

    [HttpPost("{sucursalId:guid}/vehiculos")]
    public async Task<IActionResult> AñadirVehiculo(
        Guid sucursalId,
        [FromBody] IEnumerable<AsignarVehiculoRequest> vehiculosIds)
    {
        Sucursal sucursal = await sucursalService.AgregarVehiculos(
            sucursalId,
            vehiculosIds.Select(v => v.VehiculoId));

        var response = sucursalMapper.ToResponse(sucursal);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
        var sucursales = await sucursalService.FindAll();

        var response = sucursales
            .Select(sucursalMapper.ToResponse)
            .ToList();

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> FindById(Guid id)
    {
        var sucursal = await sucursalService.FindById(id);

        return Ok(sucursalMapper.ToResponse(sucursal));
    }
}