using Microsoft.AspNetCore.Mvc;
using OrderFlow.Application.DTOs;
using OrderFlow.Application.UseCases.Unidades;

namespace OrderFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UnidadesController : ControllerBase
{
    private readonly ListarUnidadesUseCase _listarUnidadesUseCase;
    private readonly CriarUnidadeUseCase _criarUnidadeUseCase;
    private readonly AtualizarUnidadeUseCase _atualizarUnidadeUseCase;
    private readonly DesativarUnidadeUseCase _desativarUnidadeUseCase;

    public UnidadesController(ListarUnidadesUseCase listarUnidadesUseCase,
                              CriarUnidadeUseCase criarUnidadeUseCase,
                              AtualizarUnidadeUseCase atualizarUnidadeUseCase,
                              DesativarUnidadeUseCase desativarUnidadeUseCase)
    {
        _listarUnidadesUseCase = listarUnidadesUseCase;
        _criarUnidadeUseCase = criarUnidadeUseCase;
        _atualizarUnidadeUseCase = atualizarUnidadeUseCase;
        _desativarUnidadeUseCase = desativarUnidadeUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var unidades = await _listarUnidadesUseCase.ExecutarAsync();

        return Ok(unidades);
    }
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarUnidadeDto dto)
    {
        var unidade = await _criarUnidadeUseCase.ExecutarAsync(dto);

        return StatusCode(StatusCodes.Status201Created, unidade);
    }
    [HttpPut("{id:long}")]
    public async Task<IActionResult> Atualizar(
        long id,
        [FromBody] AtualizarUnidadeDto dto)
    {
        var unidade = await _atualizarUnidadeUseCase.ExecutarAsync(id, dto);

        if (unidade is null)
            return NotFound();

        return Ok(unidade);
    }
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Desativar(long id)
    {
        var desativada = await _desativarUnidadeUseCase.ExecutarAsync(id);

        if (!desativada)
            return NotFound();

        return NoContent();
    }
}