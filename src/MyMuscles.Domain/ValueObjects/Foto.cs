using MyMuscles.Domain.Extensions;
using MyMuscles.Domain.Mensagens;
using MyMuscles.Domain.Shared;

namespace MyMuscles.Domain.ValueObjects;

public sealed class Foto : ValueObjectBase
{
    public Foto(string nome, string url, bool principal = false)
    {
        Nome = nome;
        Url = url;
        Principal = principal;
        Validar();
    }

    public string Nome { get; private set; }
    public string Url { get; private set; }
    public bool Principal { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Nome;            
    }

    protected override void Validar()
    {
        if (Nome.Vazio())
            AdicionarNotificacao(nameof(Nome), MensagensExtension.CampoObrigatorio(nameof(Nome)));

        if(Url.Vazio())
            AdicionarNotificacao(nameof(Url), MensagensExtension.CampoObrigatorio(nameof(Url)));

        if (!Uri.TryCreate(Url, UriKind.Absolute, out var uriResult) || (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
            AdicionarNotificacao(nameof(Url), MensagensExtension.CampoInvalido(nameof(Url)));
    }
}
