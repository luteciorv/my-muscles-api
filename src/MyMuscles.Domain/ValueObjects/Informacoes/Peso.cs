using MyMuscles.Domain.Extensions;
using MyMuscles.Domain.Shared;

namespace MyMuscles.Domain.ValueObjects.Informacoes;

public sealed class Peso : ValueObjectBase
{
    public Peso(decimal valorEmKg)
    {
        ValorEmKg = valorEmKg;
        Validar();
    }

    public decimal ValorEmKg { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ValorEmKg;
    }

    protected override void Validar()
    {
        if (ValorEmKg <= 0)
            AdicionarNotificacao(nameof(Peso), MensagensExtension.ApenasValorPositivo(nameof(Peso)));
    }
}
