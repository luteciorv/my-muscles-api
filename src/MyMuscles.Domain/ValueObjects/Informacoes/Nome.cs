using MyMuscles.Domain.Extensions;
using MyMuscles.Domain.Shared;

namespace MyMuscles.Domain.ValueObjects.Informacoes;

public sealed class Nome : ValueObjectBase
{
    public Nome(string primeiro, string? sobrenome = null)
    {
        Primeiro = primeiro;
        Sobrenome = sobrenome;
        Validar();
    }

    public string Primeiro { get; private set; }
    public string? Sobrenome { get; private set; }
    public string NomeCompleto => Primeiro + (Sobrenome.Vazio() ? string.Empty : $" {Sobrenome}");

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return NomeCompleto;
    }

    protected override void Validar()
    {
        if (Primeiro.Vazio())
            AdicionarNotificacao(nameof(Nome), MensagensExtension.CampoObrigatorio(nameof(Nome)));
    }
}
