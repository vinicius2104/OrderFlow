using Microsoft.AspNetCore.Mvc;
using OrderFlow.Application.DTOs;
using OrderFlow.Application.UseCases.Produtos;
using Microsoft.AspNetCore.Authorization;

namespace OrderFlow.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly ListarProdutosUseCase _listarProdutosUseCase;
    private readonly CriarProdutoUseCase _criarProdutoUseCase;
    private readonly BuscarProdutoPorIdUseCase _buscarProdutoPorIdUseCase;
    private readonly AtualizarProdutoUseCase _atualizarProdutoUseCase;
    private readonly DesativarProdutoUseCase _desativarProdutoUseCase;
    

    public ProdutosController(
    ListarProdutosUseCase listarProdutosUseCase,
    CriarProdutoUseCase criarProdutoUseCase,
    BuscarProdutoPorIdUseCase buscarProdutoPorIdUseCase,
    AtualizarProdutoUseCase atualizarProdutoUseCase,
    DesativarProdutoUseCase desativarProdutoUseCase
    
    )
    
{
    _listarProdutosUseCase = listarProdutosUseCase;
    _criarProdutoUseCase = criarProdutoUseCase;
    _buscarProdutoPorIdUseCase = buscarProdutoPorIdUseCase;
    _atualizarProdutoUseCase = atualizarProdutoUseCase;
    _atualizarProdutoUseCase = atualizarProdutoUseCase;
    _desativarProdutoUseCase = desativarProdutoUseCase;
    
}

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var produtos = await _listarProdutosUseCase.ExecutarAsync();

        return Ok(produtos);
    }
    [HttpPost]
    [Authorize(Roles = "GESTOR")]
public async Task<IActionResult> Criar([FromBody] CriarProdutoDto dto)
{
    var produto = await _criarProdutoUseCase.ExecutarAsync(dto);

    return StatusCode(StatusCodes.Status201Created, produto);
}
[HttpGet("{id:long}")]
public async Task<IActionResult> BuscarPorId(long id)
{
    var produto = await _buscarProdutoPorIdUseCase.ExecutarAsync(id);

    if (produto is null)
        return NotFound();

    return Ok(produto);
}
[HttpPut("{id:long}")]
[Authorize(Roles = "GESTOR")]
public async Task<IActionResult> Atualizar(
    long id,
    [FromBody] AtualizarProdutoDto dto)
{
    var produto = await _atualizarProdutoUseCase.ExecutarAsync(id, dto);

    if (produto is null)
        return NotFound();

    return Ok(produto);
}
[HttpDelete("{id:long}")]
[Authorize(Roles = "GESTOR")]
public async Task<IActionResult> Desativar(long id)
{
    var desativado = await _desativarProdutoUseCase.ExecutarAsync(id);

    if (!desativado)
        return NotFound();

    return NoContent();
}
}