using Bogus;
using MyMuscles.Domain.ValueObjects;

namespace MyMuscles.Common.UnitTesting.ValueObjects;

public static class DescricaoBuilder
{
    public static Descricao Build()
    {
        var faker = new Faker("pt_BR");
        return new Descricao(conteudo: faker.Lorem.Paragraph());
    }
}
