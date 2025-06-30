namespace MyMuscles.Domain.Extensions;

public static class StringExtension
{
    public static bool Vazio(this string? palavra) =>
        string.IsNullOrWhiteSpace(palavra);
}
