namespace ChallengeContracts.Dto;

public class Room
{
    public string? RoomType { get; set; }
    public string? RoomId { get; set; }
    public RoomReservationDto? RoomReservation { get; set; }

    public override string ToString()
    {
        return $"RoomType: {RoomType}, RoomId: {RoomId}, RoomReservation: {RoomReservation}";
    }
}