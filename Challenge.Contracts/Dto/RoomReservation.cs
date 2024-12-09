using System;

namespace ChallengeContracts.Dto;

public record RoomReservationDto
{
    public DateTime? Arrival { get; set; }
    public DateTime? Departure { get; set; }
}