using Application.Handlers.AddCreditCard;
using Application.Handlers.RegisterPerson;
using AutoFixture;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace PersonTest.Application.Handlers.AddCreditCard;

public class AddCreditCardHandlerTest : IDisposable
{
    protected readonly Fixture fixture;
    protected readonly Mock<IUnitOfWork> UnitOfWorkMock;
    protected readonly Mock<ICreditCardRepository> CreditCardRepositoryMock;
    protected readonly AddCreditCardHandler AddCreditCardHandler;

    public AddCreditCardHandlerTest()
    {
        UnitOfWorkMock = new();
        CreditCardRepositoryMock = new();
        fixture = new Fixture();

        UnitOfWorkMock.Setup(mock => mock.CreditCardRepository).Returns(CreditCardRepositoryMock.Object);

        AddCreditCardHandler = new AddCreditCardHandler(UnitOfWorkMock.Object);
    }

    public void Dispose()
    {
        UnitOfWorkMock.Reset();

        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task AddCreditCardHandler_WithSuccess_WhenReturnCreated()
    {
        var request = fixture.Create<AddCreditCardRequestDto>();

        var result = await AddCreditCardHandler.Handle(request, CancellationToken.None);

        CreditCardRepositoryMock.Verify(m => m.Add(It.IsAny<CreditCardEntity>()), Times.Once);
        UnitOfWorkMock.Verify(m => m.Commit(), Times.Once);

        result.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task DontAddCreditCardHandler_WithSuccess_WhenReturnError()
    {
        var request = fixture.Create<AddCreditCardRequestDto>();

        UnitOfWorkMock.Setup(m => m.Commit()).Throws(new Exception());

        Func<Task> action = async () => await AddCreditCardHandler.Handle(request, CancellationToken.None);

        await action.Should().ThrowAsync<Exception>();

        CreditCardRepositoryMock.Verify(m => m.Add(It.IsAny<CreditCardEntity>()), Times.Once);
        UnitOfWorkMock.Verify(m => m.Commit(), Times.Once);
    }
}
