using System.Collections.Generic;

namespace ChallengeContracts.Dto;

public sealed class Hotel
{
    public string? HotelId { get; set; }
    public string? Name { get; set; }
    public List<RoomType>? RoomTypes { get; set; }
    public List<Room>? Rooms { get; set; }
}