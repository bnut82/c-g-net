using ChallengeContracts.Dto;

namespace ChallengeContracts;

public interface ISearchEngine
{
    public string SearchHandler(SearchArg args);
}