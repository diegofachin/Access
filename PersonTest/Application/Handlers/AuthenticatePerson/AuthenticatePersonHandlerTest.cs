using Application.Handlers.AuthenticatePerson;
using Application.Handlers.RegisterPerson;
using AutoFixture;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonTest.Application.Handlers.AuthenticatePerson;

public class AuthenticatePersonHandlerTest : IDisposable
{
    protected readonly Fixture fixture;
    protected readonly Mock<IUnitOfWork> UnitOfWorkMock;
    protected readonly Mock<IPersonRepository> PersonRepositoryMock;
    protected readonly AuthenticatePersonHandler AuthenticatePersonHandler;

    public AuthenticatePersonHandlerTest()
    {
        UnitOfWorkMock = new();
        PersonRepositoryMock = new();
        fixture = new Fixture();

        UnitOfWorkMock.Setup(mock => mock.PersonRepository).Returns(PersonRepositoryMock.Object);

        AuthenticatePersonHandler = new AuthenticatePersonHandler(UnitOfWorkMock.Object);
    }

    public void Dispose()
    {
        UnitOfWorkMock.Reset();

        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task AuthenticatePersonHandler_ReturnTrue_WhenSuccess()
    {
        var request = fixture.Create<AuthenticatePersonRequestDto>();

        PersonRepositoryMock.Setup(
           mock => mock.AuthenticatePerson(It.IsAny<string>(), It.IsAny<string>())
        ).ReturnsAsync(true);

        var result = await AuthenticatePersonHandler.Handle(request, CancellationToken.None);

        PersonRepositoryMock.Verify(m => m.AuthenticatePerson(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task DontAuthenticatePersonHandler_ReturnFalse_WhenIsInvalid()
    {
        var request = fixture.Create<AuthenticatePersonRequestDto>();

        PersonRepositoryMock.Setup(
           mock => mock.AuthenticatePerson(It.IsAny<string>(), It.IsAny<string>())
        ).ReturnsAsync(false);

        var result = await AuthenticatePersonHandler.Handle(request, CancellationToken.None);

        PersonRepositoryMock.Verify(m => m.AuthenticatePerson(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

        result.Should().BeFalse();
    }
}
