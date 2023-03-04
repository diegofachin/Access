using Application.Handlers.AddCreditCard;
using Application.Handlers.AuthenticatePerson;
using Application.Handlers.RegisterPerson;
using AutoFixture;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Person.Controllers;
using System.Net;

namespace PersonTest.Person.Controllers;

public class PersonControllerTest : IDisposable
{
    protected readonly Fixture Fixture;
    protected readonly Mock<IMediator> MediatorMock;
    protected readonly PersonController PersonController;

    public PersonControllerTest()
    {
        MediatorMock = new();
        Fixture = new Fixture();

        PersonController = new PersonController(MediatorMock.Object);
    }

    public void Dispose()
    {
        MediatorMock.Reset();

        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task RegisterPerson_WithSuccess_WhenReturnCreated()
    {
        var request = Fixture.Create<RegisterPersonRequestDto>();
        var response = Fixture.Create<RegisterPersonResponseDto>();

        MediatorMock.Setup(
            mock => mock.Send(It.IsAny<RegisterPersonRequestDto>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(response);

        var result = await PersonController.RegisterAsync(request) as ObjectResult;

        result.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task DontRegisterPerson_WithBadRequest_WhenReturnNull()
    {
        var request = Fixture.Create<RegisterPersonRequestDto>();
        RegisterPersonResponseDto? response = null;

        MediatorMock.Setup(
            mock => mock.Send(It.IsAny<RegisterPersonRequestDto>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(response);

        var result = await PersonController.RegisterAsync(request) as BadRequestResult;

        result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AuthenticatePerson_WithSuccess_WhenReturnCreated()
    {
        var request = Fixture.Create<AuthenticatePersonRequestDto>();

        MediatorMock.Setup(
            mock => mock.Send(It.IsAny<AuthenticatePersonRequestDto>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(true);

        var result = await PersonController.AuthenticateAsync(request) as OkResult;

        result.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task DontAuthenticatePerson_WithSuccess_WhenReturnUnauthorized()
    {
        var request = Fixture.Create<AuthenticatePersonRequestDto>();

        MediatorMock.Setup(
            mock => mock.Send(It.IsAny<AuthenticatePersonRequestDto>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(false);

        var result = await PersonController.AuthenticateAsync(request) as UnauthorizedResult;

        result.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task AddCreditCard_WithSuccess_WhenReturnCreated()
    {
        var request = Fixture.Create<AddCreditCardRequestDto>();
        var response = Fixture.Create<AddCreditCardResponseDto>();

        MediatorMock.Setup(
            mock => mock.Send(It.IsAny<AddCreditCardRequestDto>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(response);

        var result = await PersonController.AddCreditCardAsync(request) as ObjectResult;

        result.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task DontAddCreditCard_WithBadRequest_WhenReturnNull()
    {
        var request = Fixture.Create<AddCreditCardRequestDto>();
        AddCreditCardResponseDto? response = null;

        MediatorMock.Setup(
            mock => mock.Send(It.IsAny<AddCreditCardRequestDto>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(response);

        var result = await PersonController.AddCreditCardAsync(request) as BadRequestResult;

        result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }
}
