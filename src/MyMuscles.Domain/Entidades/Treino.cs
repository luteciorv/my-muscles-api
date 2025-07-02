using MyMuscles.Domain.Enums;
using MyMuscles.Domain.Extensions;
using MyMuscles.Domain.Shared;
using MyMuscles.Domain.ValueObjects;
using MyMuscles.Domain.ValueObjects.Dados;
using MyMuscles.Domain.ValueObjects.Informacoes;
using System.Diagnostics.CodeAnalysis;

namespace MyMuscles.Domain.Entidades;

[ExcludeFromCodeCoverage]
public sealed class Treino : EntidadeBase
{
    public Treino(EDiaDaSemana diaDaSemana, List<Exercicio> exercicios)
    {
        DiaDaSemana = diaDaSemana;
        _exercicios = exercicios;
        Concluido = false;
        Validar();
    }

    public EDiaDaSemana DiaDaSemana { get; private set; }
    public bool Concluido { get; private set; }

    public IReadOnlyCollection<Exercicio> Exercicios => _exercicios;
    private readonly List<Exercicio> _exercicios;

    protected override void Validar()
    {
        if (!Enum.IsDefined(typeof(EDiaDaSemana), DiaDaSemana))
            AdicionarNotificacao(nameof(EDiaDaSemana), MensagensExtension.CampoInvalido(nameof(DiaDaSemana)));

        if (Concluido)
            AdicionarNotificacao(nameof(Concluido), "O treino precisa ser criado como não concluído.");
    }

    public void AtualizarDiaDaSemana(EDiaDaSemana novoDiaDaSemana)
    {
        if (!Enum.IsDefined(typeof(EDiaDaSemana), novoDiaDaSemana))
        {
            AdicionarNotificacao(nameof(EDiaDaSemana), MensagensExtension.CampoInvalido(nameof(DiaDaSemana)));
            return;
        }

        AtualizadoEm = DateTime.UtcNow;
        DiaDaSemana = novoDiaDaSemana;
    }

    public void Concluir()
    {
        if (Concluido)
        {
            AdicionarNotificacao(nameof(Concluido), "O treino já foi concluído.");
            return;
        }

        if (Exercicios.Any(e => !e.Concluido))
        {
            AdicionarNotificacao(nameof(Treino), "Existem exercícios que ainda não foram concluídos.");
            return;
        }

        AtualizadoEm = DateTime.UtcNow;
        Concluido = true;
    }

    public void AdicionarExercicio(DadoDoExercicio dados)
    {
        var exercicio = new Exercicio(dados.Nome, dados.Descricao, dados.Series, dados.Repeticao, dados.Fotos);
        if (!exercicio.Valido)
        {
            AdicionarNotificacoes([.. exercicio.Notificacoes]);
            return;
        }

        AtualizadoEm = DateTime.UtcNow;
        _exercicios.Add(exercicio);
    }

    public void RemoverExercicio(Guid id)
    {
        var exercicio = _exercicios.FirstOrDefault(e => e.Id == id);
        if (exercicio is null)
        {
            AdicionarNotificacao(nameof(Exercicio), "O exercício não foi encontrado.");
            return;
        }

        AtualizadoEm = DateTime.UtcNow;
        _exercicios.Remove(exercicio);
    }

    public void AtualizarNomeExercicio(Guid id, Nome novoNome)
    {
        var exercicio = _exercicios.FirstOrDefault(e => e.Id == id);
        if (exercicio is null)
        {
            AdicionarNotificacao(nameof(Exercicio), Mensagens.EXERCICIO_NAO_ENCONTRADO);
            return;
        }

        exercicio.AtualizarNome(novoNome);
        if (!exercicio.Valido)
        {
            AdicionarNotificacoes([.. exercicio.Notificacoes]);
            return;
        }

        AtualizadoEm = DateTime.UtcNow;
    }

    public void AtualizarDescricaoExercicio(Guid id, Descricao novaDescricao)
    {
        var exercicio = _exercicios.FirstOrDefault(e => e.Id == id);
        if (exercicio is null)
        {
            AdicionarNotificacao(nameof(Exercicio), Mensagens.EXERCICIO_NAO_ENCONTRADO);
            return;
        }

        exercicio.AtualizarDescricao(novaDescricao);
        if (!exercicio.Valido)
        {
            AdicionarNotificacoes([.. exercicio.Notificacoes]);
            return;
        }

        AtualizadoEm = DateTime.UtcNow;
    }

    public void AtualizarSeriesExercicio(Guid id, Repeticao novaSerie)
    {
        var exercicio = _exercicios.FirstOrDefault(e => e.Id == id);
        if (exercicio is null)
        {
            AdicionarNotificacao(nameof(Exercicio), Mensagens.EXERCICIO_NAO_ENCONTRADO);
            return;
        }

        exercicio.AtualizarSeries(novaSerie);
        if (!exercicio.Valido)
        {
            AdicionarNotificacoes([.. exercicio.Notificacoes]);
            return;
        }

        AtualizadoEm = DateTime.UtcNow;
    }

    public void AtualizarRepeticoesExercicio(Guid id, Repeticao novaRepeticao)
    {
        var exercicio = _exercicios.FirstOrDefault(e => e.Id == id);
        if (exercicio is null)
        {
            AdicionarNotificacao(nameof(Exercicio), Mensagens.EXERCICIO_NAO_ENCONTRADO);
            return;
        }

        exercicio.AtualizarRepeticoes(novaRepeticao);
        if (!exercicio.Valido)
        {
            AdicionarNotificacoes([.. exercicio.Notificacoes]);
            return;
        }

        AtualizadoEm = DateTime.UtcNow;
    }

    public void ConcluirExercicio(Guid id)
    {
        var exercicio = _exercicios.FirstOrDefault(e => e.Id == id);
        if (exercicio is null)
        {
            AdicionarNotificacao(nameof(Exercicio), Mensagens.EXERCICIO_NAO_ENCONTRADO);
            return;
        }

        exercicio.Concluir();
        if (!exercicio.Valido)
        {
            AdicionarNotificacoes([.. exercicio.Notificacoes]);
            return;
        }

        AtualizadoEm = DateTime.UtcNow;
    }
}
