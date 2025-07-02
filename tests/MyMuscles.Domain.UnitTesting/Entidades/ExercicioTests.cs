using MyMuscles.Common.UnitTesting.Entidades;
using MyMuscles.Common.UnitTesting.ValueObjects;
using MyMuscles.Domain.ValueObjects;
using MyMuscles.Domain.ValueObjects.Informacoes;
using Shouldly;

namespace MyMuscles.Domain.UnitTesting.Entidades;

public sealed class ExercicioTests
{
    [Fact]
    public void Dado_um_exercicio_valido_nao_deve_conter_notificacoes()
    {       
        var exercicio = ExercicioBuilder.Build();

        exercicio.Valido.ShouldBeTrue();
        exercicio.Notificacoes.ShouldBeEmpty();
    }

    [Fact]
    public void Ao_criar_um_exercicio_valido_concluido_deve_ser_falso()
    {
        var exercicio = ExercicioBuilder.Build();

        exercicio.Concluido.ShouldBeFalse();
    }

    [Fact]
    public void Dado_um_exercicio_com_nome_invalido_deve_conter_notificacoes()
    {
        var nomeInvalido = new Nome(string.Empty);
        var exercicio = ExercicioBuilder.Build(nome: nomeInvalido);

        exercicio.Valido.ShouldBeFalse();
        exercicio.Notificacoes.ShouldContain(n => n.Chave == nameof(Nome));        
    }

    [Fact]
    public void Dado_um_exercicio_com_serie_invalida_deve_conter_notificacoes()
    {       
        var serieInvalida = new Repeticao(0);     
        var exercicio = ExercicioBuilder.Build(serie: serieInvalida);

        exercicio.Valido.ShouldBeFalse();
        exercicio.Notificacoes.ShouldContain(n => n.Chave == nameof(Repeticao));
    }

    [Fact]
    public void Dado_um_exercicio_com_repeticao_invalida_deve_conter_notificacoes()
    {
        var repeticaoInvalida = new Repeticao(0);
        var exercicio = ExercicioBuilder.Build(repeticao: repeticaoInvalida);

        exercicio.Valido.ShouldBeFalse();
        exercicio.Notificacoes.ShouldContain(n => n.Chave == nameof(Repeticao));
    }

    [Fact]
    public void Dado_um_exercicio_com_foto_invalida_deve_conter_notificacoes()
    {       
        List<Foto> fotosInvalidas = [new Foto(nome: string.Empty, url: string.Empty)];
        var exercicio = ExercicioBuilder.Build(fotos: fotosInvalidas);

        exercicio.Valido.ShouldBeFalse();
    }    

    [Fact]
    public void Ao_concluir_um_exercicio_deve_alterar_seus_status()
    {
        var exercicio = ExercicioBuilder.Build();

        exercicio.Concluir();

        exercicio.Concluido.ShouldBeTrue();
    }

    [Fact]
    public void Ao_atualizar_o_nome_com_um_valor_valido_nao_deve_conter_notificacoes()
    {
        var exercicio = ExercicioBuilder.Build();

        var novoNome = NomeBuilder.Build();

        exercicio.AtualizarNome(novoNome);

        exercicio.Valido.ShouldBeTrue();
        exercicio.Nome.ShouldBe(novoNome);
    }

    [Fact]
    public void Ao_atualizar_o_nome_com_um_valor_invalido_deve_conter_notificacoes()
    {
        var exercicio = ExercicioBuilder.Build();

        var novoNome = new Nome(string.Empty);

        exercicio.AtualizarNome(novoNome);

        exercicio.Valido.ShouldBeFalse();
        exercicio.Notificacoes.ShouldContain(n => n.Chave == nameof(Nome));
    }

    [Fact]
    public void Ao_atualizar_a_descricao_nao_deve_conter_notificacoes()
    {
        var exercicio = ExercicioBuilder.Build();

        var novaDescricao = DescricaoBuilder.Build();

        exercicio.AtualizarDescricao(novaDescricao);

        exercicio.Valido.ShouldBeTrue();
        exercicio.Descricao.ShouldBe(novaDescricao);
    }

    [Fact]
    public void Ao_atualizar_series_com_um_valor_valido_nao_deve_conter_notificacoes()
    {
        var exercicio = ExercicioBuilder.Build();

        var novaSerie = RepeticaoBuilder.Build();

        exercicio.AtualizarSeries(novaSerie);

        exercicio.Valido.ShouldBeTrue();
        exercicio.Series.ShouldBe(novaSerie);
    }

    [Fact]
    public void Ao_atualizar_series_com_um_valor_invalido_deve_conter_notificacoes()
    {
        var exercicio = ExercicioBuilder.Build();

        var novaSerie = new Repeticao(0);

        exercicio.AtualizarSeries(novaSerie);

        exercicio.Valido.ShouldBeFalse();
        exercicio.Notificacoes.ShouldContain(n => n.Chave == nameof(Repeticao));
    }

    [Fact]
    public void Ao_atualizar_repeticoes_com_um_valor_valido_nao_deve_conter_notificacoes()
    {
        var exercicio = ExercicioBuilder.Build();

        var novaRepeticao = RepeticaoBuilder.Build();

        exercicio.AtualizarRepeticoes(novaRepeticao);

        exercicio.Valido.ShouldBeTrue();
        exercicio.Repeticao.ShouldBe(novaRepeticao);
    }

    [Fact]
    public void Ao_atualizar_repeticoes_com_um_valor_invalido_deve_conter_notificacoes()
    {
        var exercicio = ExercicioBuilder.Build();

        var novaRepeticao = new Repeticao(0);

        exercicio.AtualizarRepeticoes(novaRepeticao);

        exercicio.Valido.ShouldBeFalse();
        exercicio.Notificacoes.ShouldContain(n => n.Chave == nameof(Repeticao));
    }

    [Fact]
    public void Ao_atualizar_fotos_com_um_valor_valido_nao_deve_conter_notificacoes()
    {
        var exercicio = ExercicioBuilder.Build();

        List<Foto> novasFotos = [FotoBuilder.Build(), FotoBuilder.Build()];

        exercicio.AtualizarFotos(novasFotos);

        exercicio.Valido.ShouldBeTrue();
        exercicio.Fotos.ShouldSatisfyAllConditions(f =>
        {
            f.First().ShouldBe(novasFotos.First());
            f.Last().ShouldBe(novasFotos.Last());
        });
    }

    [Fact]
    public void Ao_atualizar_fotos_com_um_valor_invalido_nao_deve_conter_notificacoes()
    {
        var exercicio = ExercicioBuilder.Build();

        List<Foto> novasFotos = [new Foto(string.Empty, string.Empty)];

        exercicio.AtualizarFotos(novasFotos);

        exercicio.Valido.ShouldBeFalse();        
    }
}