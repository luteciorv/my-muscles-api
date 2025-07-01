using Bogus;
using MyMuscles.Domain.ValueObjects;
using Shouldly;

namespace MyMuscles.Domain.UnitTesting.ValueObjects;

public sealed class DescricaoTests
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    public void Dado_duas_descricao_com_valores_iguais_devem_ser_iguais()
    {
        string conteudo = _faker.Lorem.Paragraph();
        var descricao = new Descricao(conteudo);
        var descricao1 = new Descricao(conteudo);

        (descricao == descricao1).ShouldBeTrue();
    }

    [Fact]
    public void Dado_duas_descricao_com_valores_diferentes_devem_ser_diferentes()
    {            
        var descricao = new Descricao(_faker.Lorem.Paragraph());
        var descricao1 = new Descricao(_faker.Lorem.Paragraph());

        (descricao != descricao1).ShouldBeTrue();
    }
}
