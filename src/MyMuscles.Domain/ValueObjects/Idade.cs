using MyMuscles.Domain.Constantes;
using MyMuscles.Domain.Shared;

namespace MyMuscles.Domain.ValueObjects;

public sealed class Idade : ValueObjectBase
{
    public Idade(int valorEmAnos)
    {
        ValorEmAnos = valorEmAnos;
        Validar();
    }

    public int ValorEmAnos { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ValorEmAnos;
    }

    protected override void Validar()
    {        
        if (ValorEmAnos < SistemaConstantes.IdadeMinima)
            AdicionarNotificacao(nameof(Idade), $"O campo '{nameof(Idade)}' precisa ser maior ou igual que '{SistemaConstantes.IdadeMaxima}' anos.");

        if (ValorEmAnos > SistemaConstantes.IdadeMaxima)
            AdicionarNotificacao(nameof(Idade), $"O campo '{nameof(Idade)} precisa ser menor do que '{SistemaConstantes.IdadeMaxima}' anos'.");
    }
}
