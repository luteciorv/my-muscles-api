using MyMuscles.Domain.Mensagens;
using MyMuscles.Domain.Shared;
using System.Net.Mail;

namespace MyMuscles.Domain.ValueObjects;

public sealed class Email : ValueObjectBase
{
    public Email(string endereco)
    {
        Endereco = endereco;
        Validar();
    }

    public string Endereco { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Endereco;
    }

    protected override void Validar()
    {
        try
        {
            var endereco = new MailAddress(Endereco);
            if (endereco.Address != Endereco)
                AdicionarNotificacao(nameof(Email), MensagensExtension.CampoObrigatorio(nameof(Endereco)));
        }
        catch
        {
            AdicionarNotificacao(nameof(Email), MensagensExtension.CampoInvalido(nameof(Endereco)));
        }
    }
}
