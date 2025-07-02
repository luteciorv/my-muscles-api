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
            AdicionarNotificacao(nameof(Senha), "A senha é obrigatória.");
            return;
        }

        if (Valor.Length < 8)
            AdicionarNotificacao(nameof(Senha), "A senha precisa ter ao menos 8 caracteres.");

        if (!Valor.Any(char.IsUpper))
            AdicionarNotificacao(nameof(Senha), "A senha precisa ter ao menos 1 caracter maiúsculo.");

        if (!Valor.Any(char.IsLower))
            AdicionarNotificacao(nameof(Senha),"A senha precisa ter ao menos 1 caracter minúsculo.");

        if (!Valor.Any(char.IsDigit))
            AdicionarNotificacao(nameof(Senha), "A senha precisa ter ao menos 1 número.");

        if (!Valor.Any(c => !char.IsLetterOrDigit(c)))
            AdicionarNotificacao(nameof(Senha), "A senha precisa ter ao menos 1 caracter especial (@#$%¨&* etc).");
    }
}
