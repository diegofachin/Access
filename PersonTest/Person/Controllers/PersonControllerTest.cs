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
    protected readonly Fixture fixture;
    protected readonly Mock<IMediator> mediatorMock;
    protected readonly PersonController personController;

    public PersonControllerTest()
    {
        mediatorMock = new();
        fixture = new Fixture();

        personController = new PersonController(mediatorMock.Object);
    }

    public void Dispose()
    {
        mediatorMock.Reset();

        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task RegisterPerson_WithSuccess_WhenReturnCreated()
    {
        var request = fixture.Create<RegisterPersonRequestDto>();
        var response = fixture.Create<RegisterPersonResponseDto>();

        mediatorMock.Setup(
            mock => mock.Send(It.IsAny<RegisterPersonRequestDto>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(response);

        var result = await personController.RegisterAsync(request) as ObjectResult;

        result.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task DontRegisterPerson_WithBadRequest_WhenReturnNull()
    {
        var request = fixture.Create<RegisterPersonRequestDto>();
        RegisterPersonResponseDto? response = null;

        mediatorMock.Setup(
            mock => mock.Send(It.IsAny<RegisterPersonRequestDto>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(response);

        var result = await personController.RegisterAsync(request) as BadRequestResult;

        result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AuthenticatePerson_WithSuccess_WhenReturnCreated()
    {
        var request = fixture.Create<AuthenticatePersonRequestDto>();

        mediatorMock.Setup(
            mock => mock.Send(It.IsAny<AuthenticatePersonRequestDto>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(true);

        var result = await personController.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>());

    }
}
