
using Microsoft.AspNetCore.Mvc;
using OrderFlow.Application.DTOs;
using OrderFlow.Application.UseCases.Usuarios;

namespace OrderFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly CadastrarUsuarioUseCase _cadastrarUsuario;
    private readonly LoginUseCase _login;

    public AuthController(
        CadastrarUsuarioUseCase cadastrarUsuario,
        LoginUseCase login)
    {
        _cadastrarUsuario = cadastrarUsuario;
        _login = login;
    }

    [HttpPost("cadastro")]
    public async Task<IActionResult> Cadastrar(
        [FromBody] CadastrarUsuarioDto dto)
    {
        try
        {
            var usuario = await _cadastrarUsuario.ExecutarAsync(dto);

            return Created("", new
            {
                usuario.Id,
                usuario.Nome,
                usuario.Email,
                usuario.Perfil
            });
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

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginDto dto)
    {
        try
        {
            var resultado = await _login.ExecutarAsync(dto);
            return Ok(resultado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { erro = ex.Message });
        }
    }
}
