using Bogus;
using MyMuscles.Domain.Constantes;
using MyMuscles.Domain.Mensagens;
using MyMuscles.Domain.ValueObjects.Informacoes;
using Shouldly;

namespace MyMuscles.Domain.UnitTesting.ValueObjects.Informacoes;

public sealed class AlturaTests
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    public void Dado_altura_valida_nao_deve_conter_notificacoes()
    {
        var altura = new Altura(_faker.Random.Decimal(0.1m, SistemaConstantes.AlturaMaxima));

        altura.Valido.ShouldBeTrue();
        altura.Notificacoes.ShouldBeEmpty();
    }

    [Fact]
    public void Dado_altura_com_valor_nao_postivo_deve_conter_notificacoes()
    {
        var altura = new Altura(_faker.Random.Decimal(max: 0));

        altura.Valido.ShouldBeFalse();
        altura.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.Count.ShouldBe(1);
            n.ShouldContain(n => n.Chave == nameof(Altura));
            n.ShouldContain(n => n.Mensagem == MensagensExtension.ApenasValorPositivo(nameof(Altura)));
        });
    }    

    [Fact]
    public void Dado_altura_com_valor_maior_do_que_o_maximo_permitido_deve_conter_notificacoes()
    {
        var altura = new Altura(_faker.Random.Decimal(min: SistemaConstantes.AlturaMaxima + 0.1m, max: 10));

        altura.Valido.ShouldBeFalse();
        altura.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.Count.ShouldBe(1);
            n.ShouldContain(n => n.Chave == nameof(Altura));
            n.ShouldContain(n => n.Mensagem == MensagensExtension.ValorMaximo(nameof(Altura), $"{SistemaConstantes.AlturaMaxima} m"));
        });
    }

    [Fact]
    public void Dado_duas_alturas_com_valores_iguais_devem_ser_iguais()
    {
        decimal alturaEmMetros = _faker.Random.Decimal(0.1m, SistemaConstantes.AlturaMaxima);
        var altura = new Altura(alturaEmMetros);
        var altura1 = new Altura(alturaEmMetros);

        (altura == altura1).ShouldBeTrue();
    }

    [Fact]
    public void Dado_duas_alturas_com_valores_diferentes_devem_ser_diferentes()
    {
        var altura = new Altura(_faker.Random.Decimal(0.1m, SistemaConstantes.AlturaMaxima));
        var altura1 = new Altura(_faker.Random.Decimal(0.1m, SistemaConstantes.AlturaMaxima));

        (altura != altura1).ShouldBeTrue();
    }
}
