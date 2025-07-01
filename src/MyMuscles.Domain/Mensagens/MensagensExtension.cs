namespace MyMuscles.Domain.Mensagens;

public static class MensagensExtension
{
    public static string CampoObrigatorio(string nomeCampo) =>
        string.Format(Mensagens.CAMPO_OBRIGATORIO, nomeCampo);
    
    public static string CampoInvalido(string nomeCampo) =>
        string.Format(Mensagens.CAMPO_INVALIDO, nomeCampo);

    public static string CaracterEspecialObrigatorio(string nomeCampo) =>
        string.Format(Mensagens.CARACTER_ESPECIAL_OBRIGATORIO, nomeCampo);

    public static string LetraMaiusculaObrigatoria(string nomeCampo) =>
        string.Format(Mensagens.LETRA_MAIUSCULA_OBRIGATORIA, nomeCampo);

    public static string LetraMinusculaObrigatoria(string nomeCampo) =>
        string.Format(Mensagens.LETRA_MINUSCULA_OBRIGATORIA, nomeCampo);

    public static string NumeroObrigatorio(string nomeCampo) =>
        string.Format(Mensagens.NUMERO_OBRIGATORIO, nomeCampo);

    public static string MinimoCaracteresObrigatorio(string nomeCampo, int quantidade) =>
        string.Format(Mensagens.MINIMO_CARACTERES, nomeCampo, quantidade);

    public static string ApenasValorPositivo(string nomeCampo) =>
        string.Format(Mensagens.CAMPO_APENAS_POSITIVO, nomeCampo);

    public static string ValorMaximo(string nomeCampo, string valor) =>
        string.Format(Mensagens.VALOR_MAXIMO, nomeCampo, valor);
}
