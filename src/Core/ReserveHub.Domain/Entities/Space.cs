﻿using SharedKernel.Abstractions;

namespace ReserveHub.Domain.Entities;

public class Space : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public ICollection<Reservation> Reservations { get; set; } = [];
}
