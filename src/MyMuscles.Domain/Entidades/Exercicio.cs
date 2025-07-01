using MyMuscles.Domain.Shared;
using MyMuscles.Domain.ValueObjects;
using MyMuscles.Domain.ValueObjects.Informacoes;

namespace MyMuscles.Domain.Entidades;

public sealed class Exercicio : EntidadeBase
{
    public Exercicio(Nome nome, Descricao descricao, Repeticao series, Repeticao repeticao, List<Foto> fotos)
    {
        Nome = nome;
        Descricao = descricao;
        Series = series;
        Repeticao = repeticao;
        _fotos = fotos;
        Concluido = false;
        Validar();
    }

    public Nome Nome { get; private set; }
    public Descricao Descricao { get; private set; }
    public Repeticao Series { get; private set; }
    public Repeticao Repeticao { get; private set; }
    public bool Concluido { get; private set; }

    public IReadOnlyCollection<Foto> Fotos => _fotos;
    private readonly List<Foto> _fotos;

    public void Concluir()
    {
        if (Concluido)
        {
            AdicionarNotificacao(nameof(Exercicio), "Exercício já concluído.");
            return;
        }

        AtualizadoEm = DateTime.UtcNow;
        Concluido = true;
    }

    protected override void Validar()
    {
        if (!Nome.Valido)
            AdicionarNotificacoes([.. Nome.Notificacoes]);

        if (!Series.Valido)
            AdicionarNotificacoes([.. Series.Notificacoes]);

        if (!Repeticao.Valido)
            AdicionarNotificacoes([.. Repeticao.Notificacoes]);

        if (Fotos.Any(f => !f.Valido))
        {
            List<Notificacao> erros = [.. Fotos.Where(f => !f.Valido).SelectMany(f => f.Notificacoes)];
            AdicionarNotificacoes(erros);
        }
    }
}
