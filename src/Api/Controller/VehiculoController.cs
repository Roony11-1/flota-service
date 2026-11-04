using Microsoft.AspNetCore.Mvc;
using RentACar.src.Api.Dto.Request;
using RentACar.src.Api.Mapper;
using RentACar.src.Core.Application.Service;

namespace RentACar.src.Api.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class VehiculoController(
    VehiculoService vehiculoService,
    VehiculoMapper vehiculoMapper) : ControllerBase
{
    [HttpPost("crear")]
    public async Task<IActionResult> Crear(
        [FromBody] VehiculoRequest request)
    {
        var vehiculo = vehiculoMapper.FromRequest(request);

        var vehiculoCreado = await vehiculoService.Crear(vehiculo);

        var response = vehiculoMapper.ToResponse(vehiculoCreado);

        return CreatedAtAction(
            nameof(FindById),
            new { id = vehiculoCreado.Id },
            response);
    }

    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
        var vehiculos = await vehiculoService.FindAll();

        var response = vehiculos
            .Select(vehiculoMapper.ToResponse)
            .ToList();

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> FindById(Guid id)
    {
        var vehiculo = await vehiculoService.FindById(id);

        var response = vehiculoMapper.ToResponse(vehiculo);

        return Ok(response);
    }

    [HttpGet("sucursal/{sucursalId:guid}")]
    public async Task<IActionResult> FindBySucursalId(Guid sucursalId)
    {
        var vehiculos = await vehiculoService.FindBySucursalId(sucursalId);

        var response = vehiculos
            .Select(vehiculoMapper.ToResponse)
            .ToList();

        return Ok(response);
    }
}