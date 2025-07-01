using MyMuscles.Domain.Extensions;
using MyMuscles.Domain.Shared;

namespace MyMuscles.Domain.ValueObjects;

public sealed class Descricao(string? conteudo = null) : ValueObjectBase
{
    public string? Conteudo { get; private set; } = conteudo;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Conteudo.Vazio() ? string.Empty : Conteudo!;
    }
}
