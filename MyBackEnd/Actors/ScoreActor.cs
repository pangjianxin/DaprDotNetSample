using Shared.Actors;
using Dapr.Actors.Runtime;

namespace MyBackEnd.Actors
{
    public class ScoreActor : Actor, IScoreActor
    {
        public ScoreActor(ActorHost host) : base(host)
        {

        }

        public async Task<int> GetScoreAsync()
        {
            var scoreValue = await StateManager.TryGetStateAsync<int>("score");
            if (scoreValue.HasValue)
            {
                Console.WriteLine("hello.....");
                return scoreValue.Value;
            }

            return 0;

        }

        public async Task<int> IncrementScoreAsync()
        {
            return await StateManager.AddOrUpdateStateAsync(
                    "score",
                    1,
                    (key, currentScore) => currentScore + 1);
        }
    }
}
