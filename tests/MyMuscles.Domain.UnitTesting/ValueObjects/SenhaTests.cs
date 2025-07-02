using MyMuscles.Domain.Extensions;
using MyMuscles.Domain.ValueObjects;
using Shouldly;

namespace MyMuscles.Domain.UnitTesting.ValueObjects;

public sealed class SenhaTests
{
    [Fact]
    public void Dado_senha_valida_nao_deve_conter_notificacao()
    {
        var senha = new Senha("Teste@123");

        senha.Valido.ShouldBeTrue();
        senha.Notificacoes.ShouldBeEmpty();
    }

    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    [Theory]
    public void Dado_senha_vazia_deve_conter_notificacao(string senhaVazia)
    {
        var senha = new Senha(senhaVazia);

        senha.Valido.ShouldBeFalse();
        senha.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.Count.ShouldBe(1);
            n.First().Chave.ShouldBe(nameof(Senha));
            n.First().Mensagem.ShouldBe(MensagensExtension.CampoObrigatorio(nameof(Senha)));
        });
    }

    [Fact]
    public void Dado_senha_com_menos_de_8_caracteres_deve_conter_notificacao()
    {
        var senha = new Senha("Teste@1");

        senha.Valido.ShouldBeFalse();
        senha.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.Count.ShouldBe(1);
            n.First().Chave.ShouldBe(nameof(Senha));
            n.First().Mensagem.ShouldBe(MensagensExtension.MinimoCaracteresObrigatorio(nameof(Senha), 8));
        });
    }

    [Fact]
    public void Dado_senha_sem_letra_maiuscula_deve_conter_notificacao()
    {
        var senha = new Senha("teste@12");

        senha.Valido.ShouldBeFalse();
        senha.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.Count.ShouldBe(1);
            n.First().Chave.ShouldBe(nameof(Senha));
            n.First().Mensagem.ShouldBe(MensagensExtension.LetraMaiusculaObrigatoria(nameof(Senha)));
        });
    }

    [Fact]
    public void Dado_senha_sem_letra_minuscula_deve_conter_notificacao()
    {
        var senha = new Senha("TESTE@12");

        senha.Valido.ShouldBeFalse();
        senha.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.Count.ShouldBe(1);
            n.First().Chave.ShouldBe(nameof(Senha));
            n.First().Mensagem.ShouldBe(MensagensExtension.LetraMinusculaObrigatoria(nameof(Senha)));
        });
    }

    [Fact]
    public void Dado_senha_sem_numero_deve_conter_notificacao()
    {
        var senha = new Senha("Teste@te");

        senha.Valido.ShouldBeFalse();
        senha.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.Count.ShouldBe(1);
            n.First().Chave.ShouldBe(nameof(Senha));
            n.First().Mensagem.ShouldBe(MensagensExtension.NumeroObrigatorio(nameof(Senha)));
        });
    }

    [Fact]
    public void Dado_senha_sem_caracter_especial_deve_conter_notificacao()
    {
        var senha = new Senha("Testee12");

        senha.Valido.ShouldBeFalse();
        senha.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.Count.ShouldBe(1);
            n.First().Chave.ShouldBe(nameof(Senha));
            n.First().Mensagem.ShouldBe(MensagensExtension.CaracterEspecialObrigatorio(nameof(Senha)));
        });
    }

    [Fact]
    public void Dado_duas_senhas_com_valores_iguais_devem_ser_iguais()
    {
        var senha = new Senha("Teste@123");
        var senha1 = new Senha("Teste@123");

        (senha == senha1).ShouldBeTrue();
    }

    [Fact]
    public void Dado_duas_senhas_com_valores_diferentes_devem_ser_diferentes()
    {
        var senha = new Senha("Teste@123");
        var senha1 = new Senha("OutraTeste@123");

        (senha != senha1).ShouldBeTrue();
    }
}
