
using OrderFlow.Application.DTOs;
using OrderFlow.Application.Interfaces;

namespace OrderFlow.Application.UseCases.Usuarios;

public class LoginUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IServicoDeSenha _servicoDeSenha;
    private readonly ITokenService _tokenService;

    public LoginUseCase(
        IUsuarioRepository usuarioRepository,
        IServicoDeSenha servicoDeSenha,
        ITokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _servicoDeSenha = servicoDeSenha;
        _tokenService = tokenService;
    }

    public async Task<LoginRespostaDto> ExecutarAsync(LoginDto dto)
    {
        if (dto is null ||
            string.IsNullOrWhiteSpace(dto.Email) ||
            string.IsNullOrWhiteSpace(dto.Senha))
        {
            throw new ArgumentException("E-mail e senha são obrigatórios.");
        }

        var usuario = await _usuarioRepository.ObterPorEmailAsync(
            dto.Email.Trim().ToLowerInvariant()
        );

        if (usuario is null || !usuario.Ativo)
            throw new UnauthorizedAccessException("E-mail ou senha inválidos.");

        var senhaValida = _servicoDeSenha.VerificarSenha(
            dto.Senha,
            usuario.SenhaHash
        );

        if (!senhaValida)
            throw new UnauthorizedAccessException("E-mail ou senha inválidos.");

        var token = _tokenService.GerarToken(
            usuario.Id,
            usuario.Nome,
            usuario.Perfil
        );

        return new LoginRespostaDto
        {
            AccessToken = token,
            TokenType = "Bearer",
            ExpiresIn = 3600
        };
    }
}
