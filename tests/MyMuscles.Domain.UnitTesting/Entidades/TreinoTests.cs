using MyMuscles.Common.UnitTesting.ValueObjects;
using MyMuscles.Domain.Entidades;
using MyMuscles.Domain.Enums;
using MyMuscles.Domain.ValueObjects;
using MyMuscles.Domain.ValueObjects.Dados;
using MyMuscles.Domain.ValueObjects.Informacoes;
using Shouldly;

namespace MyMuscles.Domain.UnitTesting.Entidades;

public sealed class TreinoTests
{
    [Fact]
    public void Dado_treino_valido_nao_deve_conter_notificacao()
    {
        var treino = new Treino(EDiaDaSemana.SegundaFeira);
        treino.Valido.ShouldBeTrue();
    }

    [Fact]
    public void Dado_treino_com_dia_da_semana_invalida_deve_conter_notificacao()
    {
        var treino = new Treino((EDiaDaSemana)999);
        treino.Valido.ShouldBeFalse();
    }

    [Fact]
    public void Ao_criar_treino_a_propriedade_concluido_deve_ser_falso()
    {
        var treino = new Treino(EDiaDaSemana.SegundaFeira);
        treino.Concluido.ShouldBeFalse();
    }

    #region Concluir treino
    [Fact]
    public void Ao_concluir_treino_nao_deve_conter_notificacao()
    {
        var treino = new Treino(EDiaDaSemana.TercaFeira);
        var dados = CriarDadoExercicioValido();

        treino.AdicionarExercicio(dados);
        var exercicioId = treino.Exercicios.First().Id;
        treino.ConcluirExercicio(exercicioId);
        treino.Concluir();

        treino.Valido.ShouldBeTrue();
        treino.Concluido.ShouldBeTrue();
    }

    [Fact]
    public void Ao_tentar_concluir_treino_ja_concluido_deve_conter_notificacao()
    {
        var treino = new Treino(EDiaDaSemana.TercaFeira);
        var dados = CriarDadoExercicioValido();

        treino.AdicionarExercicio(dados);
        var exercicioId = treino.Exercicios.First().Id;
        treino.ConcluirExercicio(exercicioId);
        treino.Concluir();
        treino.Concluir();

        treino.Valido.ShouldBeFalse();
    }

    [Fact]
    public void Ao_tentar_concluir_treino_sem_exercicios_deve_conter_notificacao()
    {
        var treino = new Treino(EDiaDaSemana.QuartaFeira);
        treino.Concluir();
        treino.Valido.ShouldBeFalse();
    }

    [Fact]
    public void Ao_tentar_concluir_treino_incompleto_deve_conter_notificacao()
    {
        var treino = new Treino(EDiaDaSemana.QuintaFeira);
        var dados = CriarDadoExercicioValido();
        treino.AdicionarExercicio(dados);
        treino.Concluir();

        treino.Valido.ShouldBeFalse();
    }
    #endregion

    #region Adicionar/remover exercício
    [Fact]
    public void Ao_adicionar_exercicio_valido_nao_deve_conter_notificacao()
    {
        var treino = new Treino(EDiaDaSemana.SextaFeira);
        var dados = CriarDadoExercicioValido();

        treino.AdicionarExercicio(dados);
        treino.Valido.ShouldBeTrue();
    }

    [Fact]
    public void Ao_adicionar_exercicio_invalido_deve_conter_notificacao()
    {
        var treino = new Treino(EDiaDaSemana.Sabado);
        var dados = new DadoDoExercicio(new Nome(""), new Descricao(""), new Repeticao(0), new Repeticao(0), []);

        treino.AdicionarExercicio(dados);
        treino.Valido.ShouldBeFalse();
    }

    [Fact]
    public void Ao_remover_exercicio_nao_deve_conter_notificacao()
    {
        var treino = new Treino(EDiaDaSemana.Domingo);
        var dados = CriarDadoExercicioValido();
        treino.AdicionarExercicio(dados);
        var id = treino.Exercicios.First().Id;

        treino.RemoverExercicio(id);
        treino.Valido.ShouldBeTrue();
    }

    [Fact]
    public void Ao_tentar_remover_exercicio_nao_existente_deve_conter_notificacao()
    {
        var treino = new Treino(EDiaDaSemana.SegundaFeira);
        treino.RemoverExercicio(Guid.NewGuid());
        treino.Valido.ShouldBeFalse();
    }
    #endregion

    #region Atualizar exercício
    [Fact]
    public void Ao_tentar_atualizar_nome_exercicio_nao_deve_conter_notificacao()
    {
        var treino = new Treino(EDiaDaSemana.TercaFeira);
        var dados = CriarDadoExercicioValido();
        treino.AdicionarExercicio(dados);
        var id = treino.Exercicios.First().Id;

        treino.AtualizarNomeExercicio(id, new Nome("Novo Nome"));
        treino.Valido.ShouldBeTrue();
    }

    [Fact]
    public void Ao_tentar_atualizar_nome_exercicio_nao_existente_deve_conter_notificacao()
    {
        var treino = new Treino(EDiaDaSemana.TercaFeira);
        treino.AtualizarNomeExercicio(Guid.NewGuid(), new Nome("Teste"));
        treino.Valido.ShouldBeFalse();
    }

    [Fact]
    public void Ao_tentar_atualizar_nome_invalido_do_exercicio_deve_conter_notificacao()
    {
        var treino = new Treino(EDiaDaSemana.TercaFeira);
        var dados = CriarDadoExercicioValido();
        treino.AdicionarExercicio(dados);
        var id = treino.Exercicios.First().Id;

        treino.AtualizarNomeExercicio(id, new Nome(""));
        treino.Valido.ShouldBeFalse();
    }

    [Fact]
    public void Ao_tentar_atualizar_descricao_do_exercicio_nao_deve_conter_notificacao()
    {
        var treino = new Treino(EDiaDaSemana.TercaFeira);
        var dados = CriarDadoExercicioValido();
        treino.AdicionarExercicio(dados);
        var id = treino.Exercicios.First().Id;

        treino.AtualizarDescricaoExercicio(id, new Descricao("Nova descrição"));
        treino.Valido.ShouldBeTrue();
    }

    [Fact]
    public void Ao_tentar_atualizar_descricao_do_exercicio_nao_existente_deve_conter_notificacao()
    {
        var treino = new Treino(EDiaDaSemana.TercaFeira);
        
        treino.AtualizarDescricaoExercicio(Guid.NewGuid(), new Descricao("Nova descrição"));
        
        treino.Valido.ShouldBeFalse();
    }

    //[Fact]
    //public void Ao_tentar_atualizar_series_do_exercicio_nao_deve_conter_notificacao()
    //{
       
    //}

    //[Fact]
    //public void Ao_tentar_atualizar_series_do_exercicio_nao_existente_deve_conter_notificacao()
    //{

    //}

    //[Fact]
    //public void Ao_tentar_atualizar_series_invalida_do_exercicio_deve_conter_notificacao()
    //{

    //}

    //[Fact]
    //public void Ao_tentar_atualizar_repeticoes_do_exercicio_nao_deve_conter_notificacao()
    //{

    //}

    //[Fact]
    //public void Ao_tentar_atualizar_repeticoes_do_exercicio_nao_existente_deve_conter_notificacao()
    //{

    //}

    //[Fact]
    //public void Ao_tentar_atualizar_repeticoes_invalida_do_exercicio_deve_conter_notificacao()
    //{

    //}

    [Fact]
    public void Ao_tentar_concluir_exercicio_nao_deve_conter_notificacao()
    {
        var treino = new Treino(EDiaDaSemana.QuartaFeira);
        var dados = CriarDadoExercicioValido();
        treino.AdicionarExercicio(dados);
        var id = treino.Exercicios.First().Id;

        treino.ConcluirExercicio(id);
        treino.Valido.ShouldBeTrue();
    }

    [Fact]
    public void Ao_tentar_concluir_exercicio_nao_existente_deve_conter_notificacao()
    {
        var treino = new Treino(EDiaDaSemana.QuartaFeira);
        treino.ConcluirExercicio(Guid.NewGuid());
        treino.Valido.ShouldBeFalse();
    }

    [Fact]
    public void Ao_tentar_concluir_invalido_exercicio_deve_conter_notificacao()
    {
        var treino = new Treino(EDiaDaSemana.QuartaFeira);
        var dados = CriarDadoExercicioValido();

        treino.AdicionarExercicio(dados);
        var id = treino.Exercicios.First().Id;
        treino.ConcluirExercicio(id);
        treino.ConcluirExercicio(id);

        treino.Valido.ShouldBeFalse();
    }
    #endregion

    private static DadoDoExercicio CriarDadoExercicioValido()
    {
        return new DadoDoExercicio(
            new Nome("Supino Reto"),
            new Descricao("Exercício para peitoral"),
            new Repeticao(3),
            new Repeticao(12),
            []
        );
    }
}
