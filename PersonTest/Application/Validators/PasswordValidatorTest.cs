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
    public void ValidatePassword_ReturnTrue_WhenIsValid()
    {
        var result = PasswordValidator.Validate("Abb12345@");

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData("Abb123@")]
    [InlineData("abb1234@")]
    [InlineData("AAB1234@")]
    [InlineData("AAB 1234@")]
    public void ValidatePassword_ReturnFalse_WhenIsInValid(string password)
    {
        var result = PasswordValidator.Validate(password);

        result.Should().BeFalse();
    }
}
