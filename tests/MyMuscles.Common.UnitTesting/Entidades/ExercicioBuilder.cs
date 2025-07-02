using MyMuscles.Common.UnitTesting.ValueObjects;
using MyMuscles.Domain.Entidades;
using MyMuscles.Domain.ValueObjects;
using MyMuscles.Domain.ValueObjects.Informacoes;

namespace MyMuscles.Common.UnitTesting.Entidades;

public static class ExercicioBuilder
{
    public static Exercicio Build(
        Nome? nome = null, 
        Descricao? descricao = null, 
        Repeticao? serie = null, 
        Repeticao? repeticao = null, 
        List<Foto>? fotos = null
    )
    {
        var novoNome = nome is null ? NomeBuilder.Build() : nome;
        var novaDescricao = descricao is null ? DescricaoBuilder.Build() : descricao;
        var novaSerie = serie is null ? RepeticaoBuilder.Build() : serie;
        var novaRepeticao = repeticao is null ? RepeticaoBuilder.Build() : repeticao;
        List<Foto> novasFotos = fotos is null ? [FotoBuilder.Build(), FotoBuilder.Build()] : fotos;
        
        return new Exercicio(novoNome, novaDescricao, novaSerie, novaRepeticao, novasFotos);
    }
}
