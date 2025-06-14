using Moq;
using ReserveHub.Application.Messaging;
using ReserveHub.Application.UseCases.Reservations.Create;
using ReserveHub.Domain.Entities;
using ReserveHub.Domain.Repositories;
using SharedKernel.UnitOfWork;

namespace ReserveHub.UnitTests.Reservations;

public class CreateReservationCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_CreateReservation_WhenASpaceIsAvailable()
    {
        // Arrange
        var request = new CreateReservationRequest(1, DateTime.UtcNow.AddHours(1), DateTime.UtcNow.AddHours(2), "Something");
        Guid userId = Guid.NewGuid();
        var command = new CreateReservationCommand(request, userId);
        Mock<IReservationRepository> reservationRepositoryMock = new();
        Mock<IUserRepository> userRepositoryMock = new();
        Mock<IEmailService> emailServiceMock = new();
        Mock<IConfirmReservationLinkFactory> linkGeneratorMock = new();
        Mock<IUnitOfWork> unitMock = new();

        reservationRepositoryMock.Setup(repo => 
            repo.IsSpaceAvailableAsync(request.SpaceId, request.StartTime, request.EndTime))
            .ReturnsAsync(true);
        reservationRepositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<Reservation>()))
            .ReturnsAsync(new Reservation
            {
                Id = Guid.NewGuid(),
                SpaceId = request.SpaceId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                UserId = userId,
                Notes = request.Notes ?? "",
            });
        emailServiceMock
            .Setup(service =>
                service.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        linkGeneratorMock
            .Setup(service =>
                service.Generate(It.IsAny<NotificationToken>()))
            .Returns("http://example.com/confirm?token=12345");

        var handler = new CreateReservationCommandHandler(
            reservationRepositoryMock.Object,
            userRepositoryMock.Object,
            emailServiceMock.Object,
            linkGeneratorMock.Object,
            unitMock.Object);
        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);
    }
}
