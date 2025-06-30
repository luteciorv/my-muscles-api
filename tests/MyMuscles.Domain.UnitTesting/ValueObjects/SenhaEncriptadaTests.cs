using MyMuscles.Domain.ValueObjects;
using Shouldly;

namespace MyMuscles.Domain.UnitTesting.ValueObjects;

public sealed class SenhaEncriptadaTests
{      
    [Fact]
    public void Dado_duas_senhas_com_valores_iguais_devem_ser_iguais()
    {
        var senhaEncriptada = new SenhaEncriptada("SenhaEncriptada");
        var senhaEncriptada1 = new SenhaEncriptada("SenhaEncriptada");

        (senhaEncriptada == senhaEncriptada1).ShouldBeTrue();
    }

    [Fact]
    public void Dado_duas_senhas_com_valores_diferentes_devem_ser_diferentes()
    {
        var senhaEncriptada = new SenhaEncriptada(Guid.NewGuid().ToString());
        var senhaEncriptada1 = new SenhaEncriptada(Guid.NewGuid().ToString());

        (senhaEncriptada != senhaEncriptada1).ShouldBeTrue();
    }
}
