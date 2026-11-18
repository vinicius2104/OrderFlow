using Microsoft.AspNetCore.Mvc;
using OrderFlow.Application.DTOs;
using OrderFlow.Application.UseCases.Estoques;

namespace OrderFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstoquesController : ControllerBase
{
    private readonly ListarEstoquesUseCase _listarEstoquesUseCase;
    private readonly CriarEstoqueUseCase _criarEstoqueUseCase;

    public EstoquesController(
        ListarEstoquesUseCase listarEstoquesUseCase,
        CriarEstoqueUseCase criarEstoqueUseCase)
    {
        _listarEstoquesUseCase = listarEstoquesUseCase;
        _criarEstoqueUseCase = criarEstoqueUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var estoques = await _listarEstoquesUseCase.ExecutarAsync();
        return Ok(estoques);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarEstoqueDto dto)
    {
        var estoque = await _criarEstoqueUseCase.ExecutarAsync(dto);
        return StatusCode(StatusCodes.Status201Created, estoque);
    }
}