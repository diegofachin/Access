using AutoFixture;
using Bogus;
using Domain.Validators;
using FluentAssertions;

namespace PersonTest.Application.Validators;

public class NumberCardValidatorTest : IDisposable
{
    protected readonly Fixture Fixture;
    protected readonly Faker Faker;
    protected readonly NumberCardValidator NumberCardValidator;

    public NumberCardValidatorTest()
    {
        Fixture = new Fixture();
        Faker = new Faker();

        NumberCardValidator = new NumberCardValidator();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    [Fact]
    public void ValidateNumberCard_ReturnTrue_WhenValid()
    {
        var result = NumberCardValidator.Validate("5149450592914871");

        result.Should().BeTrue();
    }

    [Fact]
    public void ValidateNumberCard_ReturnFalse_WhenIsInValid()
    {
        var result = NumberCardValidator.Validate(Faker.Random.AlphaNumeric(10));

        result.Should().BeFalse();
    }

    [Fact]
    public void ValidateNumberCard_ReturnFalse_WhenIsNull()
    {
        var result = NumberCardValidator.Validate(null);

        result.Should().BeFalse();
    }
}
