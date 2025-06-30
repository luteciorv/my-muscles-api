using Bogus;
using MyMuscles.Domain.Mensagens;
using MyMuscles.Domain.ValueObjects;
using Shouldly;

namespace MyMuscles.Domain.UnitTesting.ValueObjects;

public sealed class FotoTests
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    public void Dado_foto_valida_nao_deve_conter_notificacao()
    {
        var foto = new Foto(nome: _faker.Random.Word(), url: _faker.Internet.Url());

        foto.Valido.ShouldBeTrue();
        foto.Notificacoes.ShouldBeEmpty();
    }

    [Fact]
    public void Dado_foto_com_nome_vazio_deve_conter_notificacao()
    {
        var foto = new Foto(nome: string.Empty, url: _faker.Internet.Url());

        foto.Valido.ShouldBeFalse();
        foto.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.ShouldContain(n => n.Chave == nameof(Foto.Nome));
            n.ShouldContain(n => n.Mensagem == MensagensExtension.CampoObrigatorio(nameof(Foto.Nome)));
        });
    }

    [Fact]
    public void Dado_foto_com_url_vazia_deve_conter_notificacao()
    {
        var foto = new Foto(nome: _faker.Random.Word(), url: string.Empty);

        foto.Valido.ShouldBeFalse();
        foto.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.ShouldContain(n => n.Chave == nameof(Foto.Url));
            n.ShouldContain(n => n.Mensagem == MensagensExtension.CampoObrigatorio(nameof(Foto.Url)));
        });
    }

    [Fact]
    public void Dado_foto_com_url_invalida_deve_conter_notificacao()
    {
        var foto = new Foto(nome: _faker.Random.Word(), url: "isso-nao-e-uma-url");

        foto.Valido.ShouldBeFalse();
        foto.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.ShouldContain(n => n.Chave == nameof(Foto.Url));
            n.ShouldContain(n => n.Mensagem == MensagensExtension.CampoInvalido(nameof(Foto.Url)));
        });
    }

    [Fact]
    public void Dado_duas_fotos_com_valores_iguais_devem_ser_iguais()
    {
        var nome = _faker.Random.Word();
        var url = _faker.Internet.Url();

        var f1 = new Foto(nome, url);
        var f2 = new Foto(nome, url);

        f1.ShouldBe(f2);
        (f1 == f2).ShouldBeTrue();
    }

    [Fact]
    public void Dado_duas_fotos_com_valores_diferentes_devem_ser_diferentes()
    {
        var f1 = new Foto(_faker.Random.Word(), _faker.Internet.Url());
        var f2 = new Foto(_faker.Random.Word(), _faker.Internet.Url());

        f1.ShouldNotBe(f2);
        (f1 != f2).ShouldBeTrue();
    }
}
