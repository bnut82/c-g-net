using System.Collections.Generic;
using ChallengeContracts.Dto;

namespace ChallengeContracts;

public interface IDataMapper
{
    public List<Hotel>? Map();
}