namespace ChallengeContracts;

public class SearchArg
{
    public required string HotelName { get; set; }
    public required string RoomType { get; init; }
    public required SearchArgDate Dates { get; set; }
}