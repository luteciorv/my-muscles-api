using MyMuscles.Domain.Shared;

namespace MyMuscles.Domain.ValueObjects;

public sealed class SenhaEncriptada(string valor) : ValueObjectBase
{
    public string Valor { get; private set; } = valor;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Valor;
    }

    protected override void Validar()
    {
        throw new NotImplementedException();
    }
}
