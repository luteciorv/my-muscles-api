using Bogus;
using MyMuscles.Domain.Constantes;
using MyMuscles.Domain.ValueObjects.Informacoes;
using Shouldly;

namespace MyMuscles.Domain.UnitTesting.ValueObjects.Informacoes;

public sealed class IdadeTests
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    public void Dado_idade_valida_nao_deve_conter_notificacoes()
    {
        var idade = new Idade(_faker.Random.Int(16, 120));

        idade.Valido.ShouldBeTrue();
        idade.Notificacoes.ShouldBeEmpty();
    }

    [Fact]
    public void Dado_idade_menor_do_que_idade_minima_deve_conter_notificacoes()
    {
        var idade = new Idade(_faker.Random.Int(max: SistemaConstantes.IdadeMinima - 1));

        idade.Valido.ShouldBeFalse();
        idade.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.Count.ShouldBe(1);
            n.ShouldContain(n => n.Chave == nameof(Idade));
            n.ShouldContain(n => n.Mensagem == $"O campo '{nameof(Idade)}' precisa ser maior ou igual que '{SistemaConstantes.IdadeMaxima}' anos.");
        });
    }

    [Fact]
    public void Dado_idade_maior_do_que_idade_maxima_deve_conter_notificacoes()
    {
        var idade = new Idade(_faker.Random.Int(min: SistemaConstantes.IdadeMaxima + 1));

        idade.Valido.ShouldBeFalse();
        idade.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.Count.ShouldBe(1);
            n.ShouldContain(n => n.Chave == nameof(Idade));
            n.ShouldContain(n => n.Mensagem == $"O campo '{nameof(Idade)} precisa ser menor do que '{SistemaConstantes.IdadeMaxima}' anos'.");
        });
    }

    [Fact]
    public void Dado_duas_idades_com_o_mesmo_valor_devem_ser_iguais()
    {
        int idadeEmAnos = _faker.Random.Int(SistemaConstantes.IdadeMinima, SistemaConstantes.IdadeMaxima);        
        
        var idade = new Idade(idadeEmAnos);
        var idade1 = new Idade(idadeEmAnos);

        (idade == idade1).ShouldBeTrue();
    }

    [Fact]
    public void Dado_duas_idades_com_os_valores_diferentes_devem_ser_diferentes()
    {
        var idade = new Idade(20);
        var idade1 = new Idade(25);

        (idade != idade1).ShouldBeTrue();
    }
}
