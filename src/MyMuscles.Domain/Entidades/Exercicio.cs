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
    private List<Foto> _fotos;

    protected override void Validar()
    {
        if (!Nome.Valido)
            AdicionarNotificacoes([.. Nome.Notificacoes]);

        if (!Series.Valido)
            AdicionarNotificacoes([.. Series.Notificacoes]);

        if (Concluido)
            AdicionarNotificacao(nameof(Exercicio), "O exercício deve iniciar como não concluído.");

        if (!Repeticao.Valido)
            AdicionarNotificacoes([.. Repeticao.Notificacoes]);

        if (Fotos.Any(f => !f.Valido))
        {
            List<Notificacao> erros = [.. Fotos.Where(f => !f.Valido).SelectMany(f => f.Notificacoes)];
            AdicionarNotificacoes(erros);
        }
    }

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

    public void AtualizarNome(Nome novoNome)
    {
        if (!novoNome.Valido)
        {
            AdicionarNotificacoes([.. novoNome.Notificacoes]);
            return;
        }

        AtualizadoEm = DateTime.UtcNow;
        Nome = novoNome;
    }

    public void AtualizarDescricao(Descricao novaDescricao)
    {
        AtualizadoEm = DateTime.UtcNow;
        Descricao = novaDescricao;
    }

    public void AtualizarSeries(Repeticao novaSerie)
    {
        if (!novaSerie.Valido)
        {
            AdicionarNotificacoes([.. novaSerie.Notificacoes]);
            return;
        }

        AtualizadoEm = DateTime.UtcNow;
        Series = novaSerie;
    }

    public void AtualizarRepeticoes(Repeticao novaRepeticao)
    {
        if (!novaRepeticao.Valido)
        {
            AdicionarNotificacoes([.. novaRepeticao.Notificacoes]);
            return;
        }

        AtualizadoEm = DateTime.UtcNow;
        Repeticao = novaRepeticao;
    }

    public void AtualizarFotos(List<Foto> novasFotos)
    {
        if (novasFotos.Any(f => !f.Valido))
        {
            List<Notificacao> erros = [.. novasFotos.Where(f => !f.Valido).SelectMany(f => f.Notificacoes)];
            AdicionarNotificacoes(erros);
            return;
        }

        AtualizadoEm = DateTime.UtcNow;
        _fotos = novasFotos;
    }
}
