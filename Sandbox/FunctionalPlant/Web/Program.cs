using Application.Persistence;
using Model.Types.Common;
using Model.Types.Products;
using Models.Types;
using TestPersistence;
using Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<IReadOnlyRepository<Part>, PartsReadRepository>()
    .AddScoped<IReadOnlyRepository<(Part part, DiscreteMeasure quantity)>, InventoryRepository>()
    .AddScoped<IReadOnlyRepository<AssemblySpecification>, AssemblySpecificationRepository>();

builder.Services.Configure<BarcodeFormatOptions>(builder.Configuration.GetSection(BarcodeFormatOptions.BarcodeFormats));
builder.Services.AddSingleton<BarcodeGeneratorFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
