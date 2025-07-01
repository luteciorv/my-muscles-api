using MyMuscles.Domain.Constantes;
using MyMuscles.Domain.Enums;
using MyMuscles.Domain.Mensagens;
using MyMuscles.Domain.Shared;

namespace MyMuscles.Domain.ValueObjects;

public sealed class IMC : ValueObjectBase
{
    public IMC(Peso peso, Altura altura)
    {
        Valor = CalcularImc(peso.ValorEmKg, altura);
        Classificacao = Classificar(Valor);

        Validar(peso, altura);
    }

    public decimal Valor { get; private set; }
    public EClassificacaoIMC Classificacao { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Valor;
    }

    private void Validar(Peso peso, Altura altura)
    {
        if (!peso.Valido)
            AdicionarNotificacoes([.. peso.Notificacoes]);

        if (!altura.Valido)
            AdicionarNotificacoes([.. altura.Notificacoes]);

        if(Valor <= 0)
            AdicionarNotificacao(nameof(IMC), MensagensExtension.ApenasValorPositivo(nameof(IMC)));
    }

    private static decimal CalcularImc(decimal peso, Altura altura)
        => peso / (altura.ValorEmMetros * altura.ValorEmMetros);

    private static EClassificacaoIMC Classificar(decimal imc)
    {
        if (imc < SistemaConstantes.LimiteAbaixoDoPeso)
            return EClassificacaoIMC.AbaixoDoPeso;

        if (imc < SistemaConstantes.LimitePesoNormal)
            return EClassificacaoIMC.PesoNormal;

        if (imc < SistemaConstantes.LimiteSobrepeso)
            return EClassificacaoIMC.Sobrepeso;

        if (imc < SistemaConstantes.LimiteObesidadeGrau1)
            return EClassificacaoIMC.ObesidadeGrau1;

        if (imc < SistemaConstantes.LimiteObesidadeGrau2)
            return EClassificacaoIMC.ObesidadeGrau2;

        return EClassificacaoIMC.ObesidadeGrau3;
    }
}
