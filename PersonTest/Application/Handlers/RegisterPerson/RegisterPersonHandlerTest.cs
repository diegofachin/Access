using Application.Handlers.RegisterPerson;
using AutoFixture;
using Bogus;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace PersonTest.Application.Handlers.RegisterPerson;

public class RegisterPersonHandlerTest : IDisposable
{
    protected readonly Faker Faker;
    protected readonly Fixture Fixture;
    protected readonly Mock<IUnitOfWork> UnitOfWorkMock;
    protected readonly Mock<IPersonRepository> PersonRepositoryMock;
    protected readonly RegisterPersonHandler RegisterPersonHandler;

    public RegisterPersonHandlerTest()
    {
        UnitOfWorkMock = new();
        PersonRepositoryMock = new();
        Faker = new Faker();
        Fixture = new Fixture();

        UnitOfWorkMock.Setup(mock => mock.PersonRepository).Returns(PersonRepositoryMock.Object);

        RegisterPersonHandler = new RegisterPersonHandler(UnitOfWorkMock.Object);

    }

    public void Dispose()
    {
        UnitOfWorkMock.Reset();

        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task RegisterPersonHandler_ReturnCreated_WhenSuccess()
    {
        var request = Fixture.Create<RegisterPersonRequestDto>();
        PersonEntity? person = null;

        PersonRepositoryMock.Setup(
            mock => mock.GetPersonByCpf(It.IsAny<string>())
        ).ReturnsAsync(person);

        var result = await RegisterPersonHandler.Handle(request, CancellationToken.None);

        PersonRepositoryMock.Verify(m => m.Add(It.IsAny<PersonEntity>()), Times.Once);
        UnitOfWorkMock.Verify(m => m.Commit(), Times.Once);

        result.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task RegisterPersonHandler_ReturnError_WhenIsInvalid()
    {
        var request = Fixture.Create<RegisterPersonRequestDto>();
        PersonEntity? person = null;

        PersonRepositoryMock.Setup(
            mock => mock.GetPersonByCpf(It.IsAny<string>())
        ).ReturnsAsync(person);

        UnitOfWorkMock.Setup(m => m.Commit()).Throws(new Exception());

        Func<Task> action = async () => await RegisterPersonHandler.Handle(request, CancellationToken.None);

        await action.Should().ThrowAsync<Exception>();

        PersonRepositoryMock.Verify(m => m.Add(It.IsAny<PersonEntity>()), Times.Once);
        UnitOfWorkMock.Verify(m => m.Commit(), Times.Once);
    }

    [Fact]
    public void RegisterPersonHandler_ReturnError_WhenPersonAlreadyExists()
    {
        var request = Fixture.Create<RegisterPersonRequestDto>();
        var person = new PersonEntity() 
        {
            Cpf = Faker.Random.AlphaNumeric(11),
        };

        PersonRepositoryMock.Setup(
            mock => mock.GetPersonByCpf(It.IsAny<string>())
        ).ReturnsAsync(person);

        Assert.ThrowsAsync<PersonAlreadyExistsException>(() => RegisterPersonHandler.Handle(request, CancellationToken.None));
    }
}
