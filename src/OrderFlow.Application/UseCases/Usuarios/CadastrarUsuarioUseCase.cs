
using OrderFlow.Application.DTOs;
using OrderFlow.Application.Interfaces;
using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.UseCases.Usuarios;

public class CadastrarUsuarioUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IServicoDeSenha _servicoDeSenha;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public CadastrarUsuarioUseCase(
        IUsuarioRepository usuarioRepository,
        IServicoDeSenha servicoDeSenha,
        IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _usuarioRepository = usuarioRepository;
        _servicoDeSenha = servicoDeSenha;
        _unidadeDeTrabalho = unidadeDeTrabalho;
    }

    public async Task<Usuario> ExecutarAsync(CadastrarUsuarioDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));

        if (string.IsNullOrWhiteSpace(dto.Nome))
            throw new ArgumentException("O nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(dto.Email))
            throw new ArgumentException("O e-mail é obrigatório.");

        if (string.IsNullOrWhiteSpace(dto.Senha))
            throw new ArgumentException("A senha é obrigatória.");

        var email = dto.Email.Trim().ToLowerInvariant();

        var usuarioExistente =
            await _usuarioRepository.ObterPorEmailAsync(email);

        if (usuarioExistente is not null)
            throw new InvalidOperationException("Este e-mail já está cadastrado.");

        var hash = _servicoDeSenha.GerarHash(dto.Senha);

        var usuario = new Usuario(
            dto.Nome.Trim(),
            email,
            hash,
            "CLIENTE"
        );

        await _usuarioRepository.AdicionarAsync(usuario);
        await _unidadeDeTrabalho.SalvarAlteracoesAsync();

        return usuario;
    }
}
