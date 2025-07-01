using System.ComponentModel;

namespace MyMuscles.Domain.Enums;

public enum EClassificacaoIMC
{
    [Description("Abaixo do peso (IMC < 18,5)")]
    AbaixoDoPeso = 1,

    [Description("Peso normal (IMC entre 18,5 e 24,9)")]
    PesoNormal = 2,

    [Description("Sobrepeso (IMC entre 25,0 e 29,9)")]
    Sobrepeso = 3,

    [Description("Obesidade Grau I (IMC entre 30,0 e 34,9)")]
    ObesidadeGrau1 = 4,

    [Description("Obesidade Grau II (IMC entre 35,0 e 39,9)")]
    ObesidadeGrau2 = 5,

    [Description("Obesidade Grau III (IMC ≥ 40,0)")]
    ObesidadeGrau3 = 6
}
