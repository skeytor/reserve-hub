using Microsoft.AspNetCore.Identity;
using ReserveHub.Domain.Entities;

namespace ReserveHub.Persistence.SampleData;

/// Generate a seeding data class for the ReserveHub.Persistence project.
/// 
internal static class SeedData
{
    private static readonly PasswordHasher<User> passwordHasher = new();
    static SeedData()
    {
        foreach (var user in Users)
        {
            user.PasswordHash = passwordHasher.HashPassword(user, user.PasswordHash);
        }
    }
    internal static List<User> Users { get; set; } = [
        new User
        {
            FirstName = "Guest",
            LastName = "Doe",
            Email = "guest@email.com",
            IsActive = true,
            IsAdministrator = false,
            PasswordHash = "Guest123@"
        },
        new User
        {
            FirstName = "Admin",
            LastName = "Smith",
            Email = "admin@email.com",
            IsAdministrator = true,
            IsActive = true,
            PasswordHash = "Admin123@"
        }
    ];

    internal static List<Space> Spaces { get; set; } =
    [
        new Space
        {
            Name = "Conference Room A",
            Description = "A large conference room with seating for 20 people.",
            Reservations =
            [
                new Reservation
                {
                    User = Users[0],
                    StartTime = DateTime.UtcNow.AddDays(1),
                    EndTime = DateTime.UtcNow.AddDays(1).AddHours(2),
                    Status = Domain.Enums.ReservationStatus.Completed
                },
                new Reservation
                {
                    User = Users[0],
                    StartTime = DateTime.UtcNow.AddDays(6),
                    EndTime = DateTime.UtcNow.AddDays(6).AddHours(2),
                    Status = Domain.Enums.ReservationStatus.Cancelled
                }
            ]
        },
        new Space
        {
            Name = "Meeting Room B",
            Description = "A small meeting room suitable for up to 5 people.",
            Reservations =
            [
                new Reservation
                {
                    User = Users[0],
                    StartTime = DateTime.UtcNow.AddDays(1),
                    EndTime = DateTime.UtcNow.AddDays(1).AddHours(2),
                    Status = Domain.Enums.ReservationStatus.Pending
                },
                new Reservation
                {
                    User = Users[1],
                    StartTime = DateTime.UtcNow.AddDays(6),
                    EndTime = DateTime.UtcNow.AddDays(6).AddHours(2),
                    Status = Domain.Enums.ReservationStatus.Pending
                },
                new Reservation
                {
                    User = Users[1],
                    StartTime = DateTime.UtcNow.AddDays(6),
                    EndTime = DateTime.UtcNow.AddDays(6).AddHours(2),
                    Status = Domain.Enums.ReservationStatus.Pending
                }
            ]
        },
        new Space
        {
            Name = "Open Workspace",
            Description = "An open area with desks and chairs for collaborative work.",
        },
        new Space
        {
            Name = "Quiet Zone",
            Description = "A quiet area for focused work and study.",
        },
        new Space
        {
            Name = "Break Room",
            Description = "A space for relaxation and informal meetings.",
        },
        new Space
        {
            Name = "Training Room",
            Description = "A room equipped for training sessions and workshops.",
        },
        new Space
        {
            Name = "Executive Office",
            Description = "A private office for executive meetings and discussions.",
        },
        new Space
        {
            Name = "Project Room",
            Description = "A room designed for project teams to collaborate and work together.",
        },
        new Space
        {
            Name = "Video Conference Room",
            Description = "A room equipped with video conferencing technology for remote meetings.",
        },
        new Space
        {
            Name = "Library",
            Description = "A quiet space with books and resources for research and study.",
        }
    ];

}
