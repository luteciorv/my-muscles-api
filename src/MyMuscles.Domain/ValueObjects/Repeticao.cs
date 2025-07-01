using MyMuscles.Domain.Mensagens;
using MyMuscles.Domain.Shared;

namespace MyMuscles.Domain.ValueObjects;

public sealed class Repeticao : ValueObjectBase
{
    public Repeticao(int quantidade)
    {
        Quantidade = quantidade;
        Validar();
    }

    public int Quantidade { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Quantidade;
    }

    protected override void Validar()
    {
        if(Quantidade <= 0)
            AdicionarNotificacao(nameof(Repeticao), MensagensExtension.ApenasValorPositivo(nameof(Repeticao)));
    }
}
