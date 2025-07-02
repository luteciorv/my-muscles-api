using Bogus;
using MyMuscles.Domain.ValueObjects;

namespace MyMuscles.Common.UnitTesting.ValueObjects;

public static class FotoBuilder
{
    public static Foto Build()
    {
        var faker = new Faker("pt_BR");
        return new Foto(nome: faker.Name.FirstName(), url: faker.Image.LoremFlickrUrl());
    }
}
