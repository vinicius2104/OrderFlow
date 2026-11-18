using Microsoft.AspNetCore.Mvc;
using OrderFlow.Application.DTOs;
using OrderFlow.Application.UseCases.Pedidos;

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace OrderFlow.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly CriarPedidoUseCase _criarPedidoUseCase;

    public PedidosController(CriarPedidoUseCase criarPedidoUseCase)
    {
        _criarPedidoUseCase = criarPedidoUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarPedidoDto dto)
    {
        try
        {

            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!long.TryParse(usuarioIdClaim, out var usuarioId))
                return Unauthorized();

            var resultado = await _criarPedidoUseCase.ExecutarAsync(dto, usuarioId);

            return CreatedAtAction(
                nameof(Criar),
                new { id = resultado.PedidoId },
                resultado
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { erro = ex.Message });
        }
    }
}