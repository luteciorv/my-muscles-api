using MyMuscles.Domain.Extensions;
using MyMuscles.Domain.Shared;
using System.Net.Mail;

namespace MyMuscles.Domain.ValueObjects.Informacoes;

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
        if(Endereco.Vazio())
        {
            AdicionarNotificacao(nameof(Email), "O campo 'Endereço' é obrigatório.");
            return;
        }

        try
        {
            _ = new MailAddress(Endereco);            
        }
        catch
        {
            AdicionarNotificacao(nameof(Email), "O campo 'Endereço' está inválido.");
        }
    }
}
