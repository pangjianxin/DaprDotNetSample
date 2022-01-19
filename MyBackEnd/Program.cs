using MyBackEnd.Actors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDaprClient();

builder.Services.AddControllers().AddDapr();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddActors(it =>
{
    it.Actors.RegisterActor<ScoreActor>();
    it.Actors.RegisterActor<TimerActor>();

    // Configure default settings
    //it.ActorIdleTimeout = TimeSpan.FromMinutes(60);
    //it.ActorScanInterval = TimeSpan.FromSeconds(30);
    //it.DrainOngoingCallTimeout = TimeSpan.FromSeconds(60);
    //it.DrainRebalancedActors = true;
    //it.RemindersStoragePartitions = 7;
    // reentrancy not implemented in the .NET SDK at this time
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCloudEvents();

app.MapControllers();

app.MapSubscribeHandler();

app.MapActorsHandlers();

app.Run();
