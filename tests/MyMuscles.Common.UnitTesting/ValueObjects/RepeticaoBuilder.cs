using Bogus;
using MyMuscles.Domain.ValueObjects;

namespace MyMuscles.Common.UnitTesting.ValueObjects;

public static class RepeticaoBuilder
{
    public static Repeticao Build()
    {
        var faker = new Faker("pt_BR");
        return new Repeticao(quantidade: faker.Random.Int(1, 100));
    }
}
