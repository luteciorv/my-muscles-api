using MyMuscles.Domain.Extensions;
using Shouldly;

namespace MyMuscles.Domain.UnitTesting.Extensions;

public sealed class CollectionExtensionTests
{
    [Fact]
    public void Dado_colecao_com_elementos_deve_ser_verdadeiro()
    {
        List<object> objetos = [new(), new(), new()];

        objetos.Vazio().ShouldBeFalse();
    }

    [Fact]
    public void Dado_colecao_vazia_deve_ser_falso()
    {
        List<object> objetos = [];

        objetos.Vazio().ShouldBeTrue();
    }

    [Fact]
    public void Dado_colecao_nula_deve_ser_falso()
    {
        List<object>? objetos = null;

        objetos.Vazio().ShouldBeTrue();
    }
}
