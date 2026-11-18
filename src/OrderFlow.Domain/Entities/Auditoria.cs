namespace OrderFlow.Domain.Entities;

public class Auditoria
{
    public long Id { get; private set; }
    public long? UsuarioId { get; private set; }

    public string Entidade { get; private set; } = string.Empty;
    public long RegistroId { get; private set; }
    public string Acao { get; private set; } = string.Empty;
    public string? Detalhes { get; private set; }

    public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;

    private Auditoria() { }

    public Auditoria(
        string entidade,
        long registroId,
        string acao,
        long? usuarioId = null,
        string? detalhes = null)
    {
        if (string.IsNullOrWhiteSpace(entidade))
            throw new ArgumentException("A entidade é obrigatória.");

        if (registroId <= 0)
            throw new ArgumentException("O registro deve ser válido.");

        if (string.IsNullOrWhiteSpace(acao))
            throw new ArgumentException("A ação é obrigatória.");

        Entidade = entidade;
        RegistroId = registroId;
        Acao = acao;
        UsuarioId = usuarioId;
        Detalhes = detalhes;
    }
}