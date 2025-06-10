﻿using SharedKernel.Abstractions;

namespace ReserveHub.Domain.Entities;

public class User : BaseEntity<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public bool IsAdministrator { get; set; } = false;
    public ICollection<Reservation> Reservations { get; set; } = [];
}
