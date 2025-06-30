namespace MyMuscles.Domain.Shared;

public abstract class Notificavel
{
    public IReadOnlyCollection<Notificacao> Notificacoes => _notificacoes;
    private readonly List<Notificacao> _notificacoes = [];

    public bool Valido() => _notificacoes.Count == 0;

    protected void AdicionarNotificacao(string chave, string mensagem) =>
        _notificacoes.Add(new Notificacao(chave, mensagem));

    protected void AdicionarNotificacoes(List<Notificacao> notificacoes) =>
        _notificacoes.AddRange(notificacoes);

    protected abstract void Validar();    
}