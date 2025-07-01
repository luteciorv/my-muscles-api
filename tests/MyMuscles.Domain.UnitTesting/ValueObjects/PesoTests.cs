using Bogus;
using MyMuscles.Domain.Mensagens;
using MyMuscles.Domain.ValueObjects;
using Shouldly;

namespace MyMuscles.Domain.UnitTesting.ValueObjects;

public sealed class PesoTests
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    public void Dado_um_peso_valido_nao_deve_conter_notificacoes()
    {
        var peso = new Peso(_faker.Random.Decimal(0.1m, 1000));

        peso.Valido.ShouldBeTrue();
        peso.Notificacoes.ShouldBeEmpty();
    }

    [Fact]
    public void Dado_peso_com_valor_invalido_deve_conter_notificacoes()
    {
        var peso = new Peso(_faker.Random.Decimal(min: -500, 0));

        peso.Valido.ShouldBeFalse();
        peso.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.ShouldContain(n => n.Chave == nameof(Peso));
            n.ShouldContain(n => n.Mensagem == MensagensExtension.ApenasValorPositivo(nameof(Peso)));
        });
    }

    [Fact]
    public void Dado_dois_pesos_com_valores_iguais_devem_ser_iguais()
    {
        decimal pesoEmKg = (_faker.Random.Decimal(min: 0.1m, 1000));
        var peso = new Peso(pesoEmKg);
        var peso1 = new Peso(pesoEmKg);

        (peso == peso1).ShouldBeTrue();
    }

    [Fact]
    public void Dado_dois_pesos_com_valores_diferentes_devem_ser_diferentes()
    {
        var peso = new Peso(_faker.Random.Decimal(min: 0.1m, 1000));
        var peso1 = new Peso(_faker.Random.Decimal(min: 0.1m, 1000));        

        (peso != peso1).ShouldBeTrue();
    }
}
