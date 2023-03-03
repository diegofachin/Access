using AutoFixture;
using Bogus;
using Domain.Validators;
using FluentAssertions;

namespace PersonTest.Application.Validators;

public class PasswordValidatorTest : IDisposable
{
    protected readonly Fixture Fixture;
    protected readonly Faker Faker;
    protected readonly PasswordValidator PasswordValidator;

    public PasswordValidatorTest()
    {
        Fixture = new Fixture();
        Faker = new Faker();

        PasswordValidator = new PasswordValidator();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task ValidatePassword_WithValid_WhenReturnTrue()
    {
        var result = PasswordValidator.Validate("Abb12345@");

        result.Should().BeTrue();
    }

    [Fact]
    public async Task ValidatePassword_WithInvalid_WhenReturnFalse()
    {
        var result = PasswordValidator.Validate(Faker.Random.AlphaNumeric(7));

        result.Should().BeFalse();
    }
}
