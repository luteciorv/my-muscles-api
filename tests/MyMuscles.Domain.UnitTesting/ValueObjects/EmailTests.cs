using Bogus;
using MyMuscles.Domain.Mensagens;
using MyMuscles.Domain.ValueObjects;
using Shouldly;

namespace MyMuscles.Domain.UnitTesting.ValueObjects;

public sealed class EmailTests
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    public void Dado_email_valido_nao_deve_conter_notificacoes()
    {
        var email = new Email(_faker.Internet.Email());
        
        email.Valido.ShouldBeTrue();
        email.Notificacoes.ShouldBeEmpty();
    }
    
    [InlineData("email-invalido")]
    [InlineData("email-invalido@")]
    [InlineData("email-invalido@.com")]
    [Theory]
    public void Dado_email_invalido_deve_conter_notificacoes(string emailInvalido)
    {
        var email = new Email(emailInvalido);

        email.Valido.ShouldBeFalse();
        email.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.Count.ShouldBe(1);
            n.First().Chave.ShouldBe(nameof(Email));
            n.First().Mensagem.ShouldBe(MensagensExtension.CampoInvalido(nameof(Email.Endereco)));
        });
    }

    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    [Theory]
    public void Dado_email_vazio_deve_conter_notificacoes(string emailVazio)
    {
        var email = new Email(emailVazio);

        email.Valido.ShouldBeFalse();
        email.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.Count.ShouldBe(1);
            n.First().Chave.ShouldBe(nameof(Email));
            n.First().Mensagem.ShouldBe(MensagensExtension.CampoObrigatorio(nameof(Email.Endereco)));
        });
    }

    [Fact]
    public void Dado_dois_emails_com_valores_iguais_devem_ser_iguais()
    {
        string enderecoEmail = _faker.Internet.Email();
        var email = new Email(enderecoEmail);
        var email1 = new Email(enderecoEmail);

        (email == email1).ShouldBeTrue();
    }

    [Fact]
    public void Dado_dois_emails_com_valores_diferentes_devem_ser_diferentes()
    {        
        var email = new Email(_faker.Internet.Email());
        var email1 = new Email(_faker.Internet.Email());

        (email != email1).ShouldBeTrue();
    }
}
