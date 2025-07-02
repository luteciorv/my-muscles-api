using MyMuscles.Domain.ValueObjects.Informacoes;

namespace MyMuscles.Domain.ValueObjects.Dados;

public record DadoDoExercicio(
    Nome Nome,
    Descricao Descricao,
    Repeticao Series,
    Repeticao Repeticao,
    List<Foto> Fotos
);