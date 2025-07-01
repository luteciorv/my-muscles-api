using Bogus;
using MyMuscles.Domain.Mensagens;
using MyMuscles.Domain.ValueObjects.Informacoes;
using Shouldly;

namespace MyMuscles.Domain.UnitTesting.ValueObjects.Informacoes;

public sealed class NomeTests
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    public void Dado_nome_valido_nao_deve_conter_notificacoes()
    {
        var nome = new Nome(primeiro: _faker.Name.FirstName());

        nome.Valido.ShouldBeTrue();
        nome.Notificacoes.ShouldBeEmpty();
    }

    [Fact]
    public void Dado_nome_sem_o_primeiro_nome_deve_conter_notificacoes()
    {
        var nome = new Nome(string.Empty);

        nome.Valido.ShouldBeFalse();
        nome.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.ShouldContain(n => n.Chave == nameof(Nome));
            n.ShouldContain(n => n.Mensagem == MensagensExtension.CampoObrigatorio(nameof(Nome)));
        });
    }

    [Fact]
    public void Dado_nome_deve_exibir_no_nome_completo()
    {
        string primeiroNome = _faker.Name.FirstName();
        var nome = new Nome(primeiroNome);

        nome.NomeCompleto.ShouldBe(primeiroNome);
    }

    [Fact]
    public void Dado_nome_e_sobrenome_deve_exibir_no_nome_completo()
    {
        string primeiroNome = _faker.Name.FirstName();
        string sobrenome = _faker.Name.LastName();
        var nome = new Nome(primeiroNome, sobrenome);

        nome.NomeCompleto.ShouldBe(primeiroNome + " " + sobrenome);
    }

    [Fact]
    public void Dado_dois_nomes_com_valores_iguais_devem_ser_iguais()
    {
        string primeiroNome = _faker.Name.FirstName();
        string sobrenome = _faker.Name.LastName();

        var nome = new Nome(primeiroNome, sobrenome);
        var nome1 = new Nome(primeiroNome, sobrenome);

        (nome == nome1).ShouldBeTrue();
    }

    [Fact]
    public void Dado_dois_nomes_com_valores_diferentes_devem_ser_diferentes()
    {       
        var nome = new Nome(_faker.Name.FirstName(), _faker.Name.LastName());
        var nome1 = new Nome(_faker.Name.FirstName(), _faker.Name.LastName());

        (nome != nome1).ShouldBeTrue();
    }
}
