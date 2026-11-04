using Microsoft.AspNetCore.Mvc;
using RentACar.src.Api.Dto.Request;
using RentACar.src.Api.Mapper;
using RentACar.src.Core.Application.Service;

namespace RentACar.src.Api.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class VehiculoController : ControllerBase
{
    private readonly VehiculoService _vehiculoService;
    private readonly VehiculoMapper _vehiculoMapper;

    public VehiculoController(
        VehiculoService vehiculoService,
        VehiculoMapper vehiculoMapper)
    {
        _vehiculoService = vehiculoService;
        _vehiculoMapper = vehiculoMapper;
    }

    [HttpPost("crear")]
    public IActionResult Crear([FromBody] VehiculoRequest request)
    {
        var vehiculo = _vehiculoMapper.FromRequest(request);

        var vehiculoCreado = _vehiculoService.Crear(vehiculo);

        var response = _vehiculoMapper.ToResponse(vehiculoCreado);

        return CreatedAtAction(nameof(Crear), response);
    }

    [HttpGet]
    public IActionResult FindAll()
    {
        var vehiculos = _vehiculoService.FindAll();

        var response = vehiculos
            .Select(_vehiculoMapper.ToResponse)
            .ToList();

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public IActionResult FindById(Guid id)
    {
        var vehiculo = _vehiculoService.ObtenerPorId(id);

        var response = _vehiculoMapper.ToResponse(vehiculo);

        return Ok(response);
    }

    [HttpGet("sucursal/{sucursalId:guid}")]
    public IActionResult FindBySucursalId(Guid sucursalId)
    {
        var vehiculos = _vehiculoService.FindBySucursalId(sucursalId);

        var response = vehiculos
            .Select(_vehiculoMapper.ToResponse)
            .ToList();

        return Ok(response);
    }
}