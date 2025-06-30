using Bogus;
using MyMuscles.Domain.Shared;
using Shouldly;

namespace MyMuscles.Domain.UnitTesting.Shared;

public sealed class NotificavelTests
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    public void Ao_tentar_adicionar_uma_notificacao_deve_dar_certo()
    {
        var notificavel = new NotificavelTest();
        
        Notificacao notificacao = GerarNotificacao();

        notificavel.Adicionar(notificacao);

        notificavel.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.Count.ShouldBe(1);
            n.First().Chave.ShouldBe(notificacao.Chave);
            n.First().Mensagem.ShouldBe(notificacao.Mensagem);
        });
        notificavel.Valido().ShouldBeFalse();
    }

    [Fact]
    public void Ao_tentar_adicionar_notificacoes_deve_dar_certo()
    {
        var notificavel = new NotificavelTest();

        Notificacao notificacao = GerarNotificacao();
        Notificacao notificacao1 = GerarNotificacao();

        notificavel.Adicionar([notificacao, notificacao1]);

        notificavel.Notificacoes.ShouldSatisfyAllConditions(n =>
        {
            n.Count.ShouldBe(2);
            n.ShouldContain(notificacao);
            n.ShouldContain(notificacao1);            
        });
        notificavel.Valido().ShouldBeFalse();
    }

    private Notificacao GerarNotificacao()
    {
        string chave = Guid.NewGuid().ToString();
        string mensagem = _faker.Lorem.Paragraph();
        return new(chave, mensagem);
    }
}

public sealed class NotificavelTest : Notificavel
{
    public void Adicionar(Notificacao notificacao) =>
        AdicionarNotificacao(notificacao.Chave, notificacao.Mensagem);

    public void Adicionar(List<Notificacao> notificacaos) =>
        AdicionarNotificacoes(notificacaos);

    protected override void Validar()
    {
        throw new NotImplementedException();
    }
}
