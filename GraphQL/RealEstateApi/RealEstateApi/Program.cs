using DataAccess.Repositories;
using DataAccess.Repositories.Contracts;
using Database;
using GraphiQl;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using RealEstateApi.Queries;
using RealEstateApi.Schema;
using Types;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RealEstateContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:RealEstateDb"]);
});

builder.Services.AddScoped<IPropertyRepository, PropertyRepository>()
    .AddSingleton<IDocumentExecuter, DocumentExecuter>()
    .AddScoped<PropertyQuery>()
    .AddSingleton<PropertyType>()
    .AddScoped<ISchema, RealEstateSchema>();

var sp = builder.Services.BuildServiceProvider();
builder.Services.AddSingleton<ISchema>(new RealEstateSchema(sp));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseGraphiQl();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RealEstateContext>();
    await context.Database.EnsureCreatedAsync();
    await context.Database.MigrateAsync();
    context.EnsureSeedData();
}

app.Run();
