using Dapr.Actors;
using Dapr.Actors.Runtime;
using Shared.Actors;
using System.Text;

namespace MyBackEnd.Actors
{
    public class TimerActor : Actor, ITimerActor
    {
        public TimerActor(ActorHost host) : base(host)
        {
        }

        public async Task StartTimerAsync(string name, string text)
        {
            await RegisterTimerAsync(
                name,
                nameof(TimerCallbackAsync),
                Encoding.UTF8.GetBytes(text),
                TimeSpan.Zero,
                TimeSpan.FromSeconds(3));
        }

        public Task TimerCallbackAsync(byte[] state)
        {
            var text = Encoding.UTF8.GetString(state);

            Logger.LogInformation($"Timer fired: {text}");

            return Task.CompletedTask;
        }

        public async Task StopTimerAsync(string name)
        {
            await UnregisterTimerAsync(name);
        }

        public async Task DoAnotherThing(string message)
        {
            await Task.Delay(5000);
            Logger.LogInformation(message);
        }
    }
}
