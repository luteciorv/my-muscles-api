using Bogus;
using MyMuscles.Domain.Mensagens;
using MyMuscles.Domain.ValueObjects;
using Shouldly;

namespace MyMuscles.Domain.UnitTesting.ValueObjects;

public sealed class RepeticaoTests
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    public void Dado_repeticao_valido_nao_deve_conter_notificacoes()
    {
        var repeticao = new Repeticao(_faker.Random.Int(1, 100));

        repeticao.Valido.ShouldBeTrue();
        repeticao.Notificacoes.ShouldBeEmpty();
    }

    [Fact]
    public void Dado_repeticao_com_valor_invalido_deve_conter_notificacoes()
    {
        var repeticao = new Repeticao(_faker.Random.Int(-100, 0));

        repeticao.Valido.ShouldBeFalse();
        repeticao.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.ShouldContain(n => n.Chave == nameof(Repeticao));
            n.ShouldContain(n => n.Mensagem == MensagensExtension.ApenasValorPositivo(nameof(Repeticao)));
        });
    }

    [Fact]
    public void Dado_dois_repeticao_com_valores_iguais_devem_ser_iguais()
    {
        int quantidade = _faker.Random.Int(1, 100);
        var repeticao = new Repeticao(quantidade);
        var repeticao1 = new Repeticao(quantidade);

        (repeticao == repeticao1).ShouldBeTrue();
    }

    [Fact]
    public void Dado_dois_repeticao_com_valores_diferentes_devem_ser_diferentes()
    {        
        var repeticao = new Repeticao(_faker.Random.Int(1, 100));
        var repeticao1 = new Repeticao(_faker.Random.Int(101, 100));

        (repeticao != repeticao1).ShouldBeTrue();
    }
}
