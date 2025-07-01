using MyMuscles.Domain.ValueObjects.Informacoes;
using Shouldly;

namespace MyMuscles.Domain.UnitTesting.ValueObjects.Informacoes;

public sealed class DataNascimentoTests
{
    [Fact]
    public void Dado_data_nascimento_valida_nao_deve_conter_notificacoes()
    {
        var dataValida = DateTime.Today.AddYears(-30);
        var dataNascimento = new DataNascimento(dataValida);

        dataNascimento.Valido.ShouldBeTrue();
        dataNascimento.Notificacoes.ShouldBeEmpty();
    }
   
    [Fact]
    public void Dado_data_nascimento_menor_do_que_16_anos_deve_conter_notificacoes()
    {
        var dataInvalida = DateTime.Today.AddYears(-15);
        var dataNascimento = new DataNascimento(dataInvalida);

        dataNascimento.Valido.ShouldBeFalse();
        dataNascimento.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.Count.ShouldBe(1);
            n.First().Chave.ShouldBe(nameof(DataNascimento));
            n.First().Mensagem.ShouldBe("O campo 'data de nascimento' deve ter no mínimo 16 anos.");
        });
    }

    [Fact]
    public void Dado_data_nascimento_maior_do_que_120_anos_deve_conter_notificacoes()
    {
        var dataMuitoAntiga = DateTime.Today.AddYears(-121);
        var dataNascimento = new DataNascimento(dataMuitoAntiga);

        dataNascimento.Valido.ShouldBeFalse();
        dataNascimento.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.Count.ShouldBe(1);
            n.First().Chave.ShouldBe(nameof(DataNascimento));
            n.First().Mensagem.ShouldBe("O campo 'data de nascimento' não pode indicar mais de 120 anos.");
        });
    }

    [Fact]
    public void Dado_data_nascimento_maior_do_que_hoje_deve_conter_notificacoes()
    {
        var dataFutura = DateTime.Today.AddDays(1);
        var dataNascimento = new DataNascimento(dataFutura);

        dataNascimento.Valido.ShouldBeFalse();
        dataNascimento.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.Count.ShouldBe(1);
            n.First().Chave.ShouldBe(nameof(DataNascimento));
            n.First().Mensagem.ShouldBe("O campo 'data de nascimento' não pode ser no futuro.");
        });        
    }

    [Fact]
    public void Dado_duas_data_nascimento_com_valores_iguais_devem_ser_iguais()
    {
        var data = DateTime.Today.AddYears(-20);
        var d1 = new DataNascimento(data);
        var d2 = new DataNascimento(data);

        (d1 == d2).ShouldBeTrue();
    }

    [Fact]
    public void Dado_duas_data_nascimento_com_valores_diferentes_devem_ser_diferentes()
    {
        var d1 = new DataNascimento(DateTime.Today.AddYears(-20));
        var d2 = new DataNascimento(DateTime.Today.AddYears(-25));
        
        (d1 != d2).ShouldBeTrue();
    }
}
