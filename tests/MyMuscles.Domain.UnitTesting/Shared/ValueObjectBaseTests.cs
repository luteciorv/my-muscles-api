using MyMuscles.Domain.Shared;
using Shouldly;

namespace MyMuscles.Domain.UnitTesting.Shared;

public sealed class ValueObjectBaseTests
{
    [Fact]
    public void Dado_dois_VO_com_valores_iguais_devem_ser_iguais()
    {
        var vo = new ValueObjectBaseTest("A", "B");
        var vo1 = new ValueObjectBaseTest("A", "B");

        (vo == vo1).ShouldBeTrue();
        (vo != vo1).ShouldBeFalse();
        vo.Equals(vo1).ShouldBeTrue();
    }   

    [Fact]
    public void Dado_dois_VO_com_valores_diferentes_devem_ser_diferentes()
    {
        var vo = new ValueObjectBaseTest("A", "B");
        var vo1 = new ValueObjectBaseTest("A", "C");

        (vo == vo1).ShouldBeFalse();
        (vo != vo1).ShouldBeTrue();
        vo.Equals(vo1).ShouldBeFalse();
    }

    [Fact]
    public void Ao_comparar_VO_com_numm_deve_retornar_falso()
    {
        var vo = new ValueObjectBaseTest("A", "B");

        vo.Equals(null).ShouldBeFalse();
        (vo == null!).ShouldBeFalse();
        (vo != null!).ShouldBeTrue();       
    }

    [Fact]
    public void Dado_dois_VO_iguais_seus_hash_code_devem_ser_iguais()
    {
        var vo = new ValueObjectBaseTest("A", "B");
        var vo1 = new ValueObjectBaseTest("A", "B");

        vo.GetHashCode().ShouldBe(vo1.GetHashCode());
    }

    [Fact]
    public void Dado_dois_VO_diferentes_seus_hash_code_devem_ser_diferentes()
    {
        var vo = new ValueObjectBaseTest("A", "B");
        var vo1 = new ValueObjectBaseTest("A", "C");

        vo.GetHashCode().ShouldNotBe(vo1.GetHashCode());
    }
}

public sealed class ValueObjectBaseTest(string valor1, string valor2) : ValueObjectBase
{
    public string Valor1 { get; private set; } = valor1;
    public string Valor2 { get; private set; } = valor2;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Valor1;
        yield return Valor2;
    }

    protected override void Validar()
    {
        throw new NotImplementedException();
    }
}
