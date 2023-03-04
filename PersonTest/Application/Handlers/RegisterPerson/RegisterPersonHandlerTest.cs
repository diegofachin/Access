using Application.Handlers.RegisterPerson;
using AutoFixture;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace PersonTest.Application.Handlers.RegisterPerson;

public class RegisterPersonHandlerTest : IDisposable
{
    protected readonly Fixture fixture;
    protected readonly Mock<IUnitOfWork> UnitOfWorkMock;
    protected readonly Mock<IPersonRepository> PersonRepositoryMock;
    protected readonly RegisterPersonHandler RegisterPersonHandler;

    public RegisterPersonHandlerTest()
    {
        UnitOfWorkMock = new();
        PersonRepositoryMock = new();
        fixture = new Fixture();

        UnitOfWorkMock.Setup(mock => mock.PersonRepository).Returns(PersonRepositoryMock.Object);

        RegisterPersonHandler = new RegisterPersonHandler(UnitOfWorkMock.Object);

    }

    public void Dispose()
    {
        UnitOfWorkMock.Reset();

        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task RegisterPersonHandler_WithSuccess_WhenReturnCreated()
    {
        var request = fixture.Create<RegisterPersonRequestDto>();

        var result = await RegisterPersonHandler.Handle(request, CancellationToken.None);

        PersonRepositoryMock.Verify(m => m.Add(It.IsAny<PersonEntity>()), Times.Once);
        UnitOfWorkMock.Verify(m => m.Commit(), Times.Once);

        result.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task DontRegisterPersonHandler_WithSuccess_WhenReturnError()
    {
        var request = fixture.Create<RegisterPersonRequestDto>();

        UnitOfWorkMock.Setup(m => m.Commit()).Throws(new Exception());

        Func<Task> action = async () => await RegisterPersonHandler.Handle(request, CancellationToken.None);

        await action.Should().ThrowAsync<Exception>();

        PersonRepositoryMock.Verify(m => m.Add(It.IsAny<PersonEntity>()), Times.Once);
        UnitOfWorkMock.Verify(m => m.Commit(), Times.Once);
    }
}
