namespace MyMuscles.Domain.Extensions;

public static class CollectionExtension
{
    public static bool Vazio<T>(this IEnumerable<T>? collection) => collection is null || !collection.Any();
}
