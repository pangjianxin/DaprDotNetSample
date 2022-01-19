using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace MyBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly DaprClient _daprClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        [HttpGet("getcounter/{counter}")]
        public async Task<IEnumerable<WeatherForecast>> Get([FromState("statestore", "counter")] StateEntry<int> counter)
        {
            counter.Value++;

            await counter.SaveAsync();

            //await _daprClient.SaveStateAsync(counter.StoreName, counter.Key, counter.Value);

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                State = counter.Value
            })
            .ToArray();
        }
        [Topic("pubsub", "weatherchange")]
        [HttpPost("/weatherchanged")]
        public async Task Post(WeatherForecastChangeEvent @event)
        {
            Console.WriteLine($"weather changed:{@event.Name}----{@event.Date.ToString("yyyyMMdd")}");
            var currnetDirectyr = Directory.GetCurrentDirectory();
            using (var writer = new StreamWriter($@"{currnetDirectyr}/1.txt"))
            {
                await writer.WriteLineAsync(@event.Name);
                await writer.WriteLineAsync(@event.Date.ToString("yyyyMMdd"));
                await writer.FlushAsync();
            }
        }


        public async Task OutputBindings()
        {
            await _daprClient.InvokeBindingAsync<string>("bindingName", "create", data: "hello world", metadata: new Dictionary<string, string> { });
        }
    }
}