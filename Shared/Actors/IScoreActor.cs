using Dapr.Actors;

namespace Shared.Actors;
public interface IScoreActor : IActor
{
    Task<int> IncrementScoreAsync();

    Task<int> GetScoreAsync();
}
