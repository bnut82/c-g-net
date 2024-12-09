using System.Collections.Generic;

namespace ChallengeContracts.Dto;

public class RoomType
{
    public string? Code { get; set; }
    public string? Description { get; set; }
    public virtual List<string>? Facilities { get; set; }
    public virtual List<string>? Feature { get; set; }

}