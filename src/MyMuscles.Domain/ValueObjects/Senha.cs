using MyMuscles.Domain.Extensions;
using MyMuscles.Domain.Shared;

namespace MyMuscles.Domain.ValueObjects;

public sealed class Senha : ValueObjectBase
{
    public Senha(string valor)
    {
        Valor = valor;
        Validar();
    }

    public string Valor { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Valor;
    }

    protected override void Validar()
    {
        if (Valor.Vazio())
        {
            AdicionarNotificacao(nameof(Senha), MensagensExtension.CampoObrigatorio(nameof(Senha)));
            return;
        }

        if (Valor.Length < 8)
            AdicionarNotificacao(nameof(Senha), MensagensExtension.MinimoCaracteresObrigatorio(nameof(Senha), 8));

        if (!Valor.Any(char.IsUpper))
            AdicionarNotificacao(nameof(Senha), MensagensExtension.LetraMaiusculaObrigatoria(nameof(Senha)));

        if (!Valor.Any(char.IsLower))
            AdicionarNotificacao(nameof(Senha), MensagensExtension.LetraMinusculaObrigatoria(nameof(Senha)));

        if (!Valor.Any(char.IsDigit))
            AdicionarNotificacao(nameof(Senha), MensagensExtension.NumeroObrigatorio(nameof(Senha)));

        if (!Valor.Any(c => !char.IsLetterOrDigit(c)))
            AdicionarNotificacao(nameof(Senha), MensagensExtension.CaracterEspecialObrigatorio(nameof(Senha)));
    }
}
