using Dapr.Actors;
using Dapr.Actors.Client;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.Actors;

namespace MyFrontEnd.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly DaprClient daprClient;

        public PrivacyModel(ILogger<PrivacyModel> logger, DaprClient daprClient)
        {
            _logger = logger;
            this.daprClient = daprClient;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await daprClient.PublishEventAsync<WeatherForecastChangeEvent>(
                "pubsub", "weatherchange", new WeatherForecastChangeEvent { Name = "walleee", Date = DateTime.Now });

            var actorId = new ActorId("scoreActor1");

            var proxy = ActorProxy.Create<IScoreActor>(actorId, "ScoreActor");

            var score = await proxy.IncrementScoreAsync();

            Console.WriteLine($"Current score: {score}");

            return new NoContentResult();
        }
    }
}