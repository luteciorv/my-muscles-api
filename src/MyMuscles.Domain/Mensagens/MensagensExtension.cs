namespace MyMuscles.Domain.Mensagens;

public static class MensagensExtension
{
    public static string CampoObrigatorio(string nomeCampo) =>
        string.Format(Mensagens.CAMPO_OBRIGATORIO, nomeCampo);
    
    public static string CampoInvalido(string nomeCampo) =>
        string.Format(Mensagens.CAMPO_INVALIDO, nomeCampo);
}
