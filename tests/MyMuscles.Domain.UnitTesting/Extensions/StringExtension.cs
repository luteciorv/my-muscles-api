using Bogus;
using MyMuscles.Domain.Extensions;
using Shouldly;

namespace MyMuscles.Domain.UnitTesting.Extensions;

public sealed class StringExtension
{
    private readonly Faker _faker = new("pt_BR");

    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    [Theory]
    public void Dado_palavra_vazia_deve_ser_falso(string palavraVazia)
    {
        palavraVazia.Vazio().ShouldBeTrue();
    }

    [Fact]
    public void Dado_palavra_nao_deve_estar_vazia()
    {
        string palavra = _faker.Lorem.Word();
        palavra.Vazio().ShouldBeFalse();
    }
}
