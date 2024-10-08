using Application.RawContent.GetLatest;
using Common.Messaging.Administration;
using Infrastructure.Kafka;
using MediatR;
using Persistence;
using Serilog;
using Api.GraphQL;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(builder.Configuration));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddKafkaProducer(builder.Configuration)
    .AddMediatR(typeof(CreateTopicCommand), typeof(GetLatestContentQuery))
    .AddPersistence(builder.Configuration)
    .AddGraphQl();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseSerilogRequestLogging();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "Hello from NoWasteProcessor");

app.UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapGraphQL();
    });

app.Logger.LogInformation("NoWasteProcessor API started.");

await InitializeDatabase(app);
app.Run();

static async Task InitializeDatabase(IHost app)
{
    using var scope = app.Services.CreateAsyncScope();
    var initialiser = scope.ServiceProvider.GetRequiredService<SourceDbContextInitializer>();
    await initialiser.InitialiseAsync();
}

