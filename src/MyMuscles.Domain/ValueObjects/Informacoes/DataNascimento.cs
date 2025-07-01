using MyMuscles.Domain.Shared;

namespace MyMuscles.Domain.ValueObjects.Informacoes;

public sealed class DataNascimento : ValueObjectBase
{
    public DataNascimento(DateTime valor)
    {
        Valor = valor;
        Validar();
    }

    public DateTime Valor { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Valor;
    }

    protected override void Validar()
    {
        var hoje = DateTime.Today;
        var limiteMin = hoje.AddYears(-120);
        var limiteMax = hoje.AddYears(-16);

        if (Valor > hoje)
        {
            AdicionarNotificacao(nameof(DataNascimento), "O campo 'data de nascimento' não pode ser no futuro.");
            return;
        }

        if (Valor < limiteMin)
            AdicionarNotificacao(nameof(DataNascimento), "O campo 'data de nascimento' não pode indicar mais de 120 anos.");

        if (Valor > limiteMax)
            AdicionarNotificacao(nameof(DataNascimento), "O campo 'data de nascimento' deve ter no mínimo 16 anos.");
    }
}
