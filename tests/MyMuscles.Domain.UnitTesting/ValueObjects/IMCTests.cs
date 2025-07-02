using Bogus;
using MyMuscles.Domain.Constantes;
using MyMuscles.Domain.Enums;
using MyMuscles.Domain.ValueObjects;
using MyMuscles.Domain.ValueObjects.Informacoes;
using Shouldly;

namespace MyMuscles.Domain.UnitTesting.ValueObjects;

public sealed class IMCTests
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    public void Dado_imc_valido_nao_deve_conter_notificacao()
    {
        var altura = new Altura(_faker.Random.Decimal(0.1m, SistemaConstantes.AlturaMaxima));
        var peso = new Peso(_faker.Random.Decimal(0.1m, 100));
        var imc = new IMC(peso, altura);

        imc.Valido.ShouldBeTrue();
        imc.Notificacoes.ShouldBeEmpty();
    }

    [Fact]
    public void Dado_imc_com_peso_invalido_deve_conter_notificacao()
    {
        var altura = new Altura(_faker.Random.Decimal(0.1m, SistemaConstantes.AlturaMaxima));
        var peso = new Peso(_faker.Random.Decimal(-100, 0));

        var imc = new IMC(peso, altura);

        imc.Valido.ShouldBeFalse();
        imc.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.ShouldContain(n => n.Chave == nameof(Peso));
        });
    }

    [Fact]
    public void Dado_imc_com_altura_invalida_deve_conter_notificacao()
    {
        var altura = new Altura(_faker.Random.Decimal(-100, 0));
        var peso = new Peso(_faker.Random.Decimal(0.1m, 100));

        var imc = new IMC(peso, altura);

        imc.Valido.ShouldBeFalse();
        imc.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.ShouldContain(n => n.Chave == nameof(Altura));
        });
    }

    [InlineData(EClassificacaoIMC.AbaixoDoPeso, 50, 1.70)]
    [InlineData(EClassificacaoIMC.PesoNormal, 65, 1.70)]
    [InlineData(EClassificacaoIMC.Sobrepeso, 78, 1.70)]
    [InlineData(EClassificacaoIMC.ObesidadeGrau1, 90, 1.70)]
    [InlineData(EClassificacaoIMC.ObesidadeGrau2, 105, 1.70)]
    [InlineData(EClassificacaoIMC.ObesidadeGrau3, 120, 1.70)]
    [Theory]
    public void Dado_imc_valido_deve_corresponder_a_classificacao(EClassificacaoIMC classificacao, decimal pesoEmKg, decimal alturaEmMetros)
    {
        var altura = new Altura(alturaEmMetros);
        var peso = new Peso(pesoEmKg);
        var imc = new IMC(peso, altura);

        imc.Classificacao.ShouldBe(classificacao);
    }

    [Fact]
    public void Dado_dois_imc_com_valores_iguais_devem_ser_iguais()
    {
        decimal pesoEmKg = _faker.Random.Decimal(0.1m, 100);
        var peso = new Peso(pesoEmKg);
        decimal alturaEmMetros = _faker.Random.Decimal(0.1m, SistemaConstantes.AlturaMaxima);
        var altura = new Altura(alturaEmMetros);

        var imc = new IMC(peso, altura);
        var imc1 = new IMC(peso, altura);

        (imc == imc1).ShouldBeTrue();
    }

    [Fact]
    public void Dado_dois_imc_com_valores_diferenes_devem_ser_diferentes()
    {
        var imc = new IMC(peso: new Peso(_faker.Random.Decimal(0.1m, 100)),
                          altura: new Altura(_faker.Random.Decimal(0.1m, SistemaConstantes.AlturaMaxima))
        );
        var imc1 = new IMC(peso: new Peso(_faker.Random.Decimal(0.1m, 100)),
                         altura: new Altura(_faker.Random.Decimal(0.1m, SistemaConstantes.AlturaMaxima))
        );

        (imc != imc1).ShouldBeTrue();
    }
}
