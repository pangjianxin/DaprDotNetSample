using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.Actors;

namespace MyFrontEnd.Pages
{
    public class ActorsModel : PageModel
    {
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostStartTimerAsync()
        {
            var actorId = new ActorId("timerActor1");

            var proxy = ActorProxy.Create<ITimerActor>(actorId, "TimerActor");

            await proxy.StartTimerAsync("wallee", "hello world");

            return new NoContentResult();
        }

        public async Task<IActionResult> OnPostStopTimerAsync()
        {
            var actorId = new ActorId("timerActor1");

            var proxy = ActorProxy.Create<ITimerActor>(actorId, "TimerActor");

            await proxy.StopTimerAsync("wallee");

            return new NoContentResult();

        }

        public async Task<IActionResult> OnPostDoAnotherThingAsync()
        {
            var actorId = new ActorId("timerActor1");

            var proxy = ActorProxy.Create<ITimerActor>(actorId, "TimerActor");

            await proxy.DoAnotherThing("this is a message i want to tell you");

            return new NoContentResult();
        }
    }
}
