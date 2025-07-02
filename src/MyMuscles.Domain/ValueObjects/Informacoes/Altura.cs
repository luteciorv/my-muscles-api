using MyMuscles.Domain.Constantes;
using MyMuscles.Domain.Extensions;
using MyMuscles.Domain.Shared;

namespace MyMuscles.Domain.ValueObjects.Informacoes;

public sealed class Altura : ValueObjectBase
{
    public Altura(decimal valorEmMetros)
    {
        ValorEmMetros = valorEmMetros;
        Validar();
    }

    public decimal ValorEmMetros { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ValorEmMetros;
    }

    protected override void Validar()
    {
        if(ValorEmMetros <= 0)
            AdicionarNotificacao(nameof(Altura), MensagensExtension.ApenasValorPositivo(nameof(Altura)));

        if(ValorEmMetros > SistemaConstantes.AlturaMaxima)
            AdicionarNotificacao(nameof(Altura), MensagensExtension.ValorMaximo(nameof(Altura), $"{SistemaConstantes.AlturaMaxima} m"));
    }
}
