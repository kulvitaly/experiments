using AppAny.HotChocolate.FluentValidation;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using FirebaseAdminAuthentication.DependencyInjection.Extensions;
using FirebaseAdminAuthentication.DependencyInjection.Models;
using FluentValidation.AspNetCore;
using Google.Apis.Auth.OAuth2;
using GraphQLDemo.API.DataLoaders;
using GraphQLDemo.API.Schema.Mutations;
using GraphQLDemo.API.Schema.Queries;
using GraphQLDemo.API.Schema.Subscriptions;
using GraphQLDemo.API.Services;
using GraphQLDemo.API.Services.Courses;
using GraphQLDemo.API.Services.Instructors;
using GraphQLDemo.API.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connctionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddPooledDbContextFactory<SchoolDbContext>(options => options.UseSqlServer(connctionString).LogTo(Console.WriteLine))
    .AddScoped<CoursesRepository>()
    .AddScoped<InstructorsRepository>()
    .AddScoped<InstructorDataLoader>()
    .AddScoped<UserDataLoader>()
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddTransient<CourseTypeInputValidator>()
    .AddTransient<InstructorTypeInputValidator>();

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddTypeExtension<CourseQuery>()
    .AddTypeExtension<InstructorQuery>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<CourseMutation>()
    .AddTypeExtension<InstructorMutation>()
    .AddSubscriptionType<Subscription>()
    .AddType<CourseType>()
    .AddType<InstructorType>()
    .AddInMemorySubscriptions()
    .AddAuthorization()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddFluentValidation(o => o.UseDefaultErrorMapper())
    .RegisterService<CourseTypeInputValidator>()
    .RegisterService<InstructorTypeInputValidator>()
    ;

builder.Services
    .AddSingleton(FirebaseApp.Create(new AppOptions
    {
        Credential = GoogleCredential.FromJson(builder.Configuration.GetValue<string>("FIREBASE_CONFIG"))
    }))
    .AddFirebaseAuthentication();

builder.Services.AddSingleton(FirebaseAuth.DefaultInstance);

builder.Services.AddAuthorization(o => o.AddPolicy("IsAdmin", p => p.RequireClaim(FirebaseUserClaimType.EMAIL, "vku@gmail.com")));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseRouting()
    .UseAuthentication()
    .UseWebSockets()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapGraphQL();
    });

using (var scope = app.Services.CreateScope())
{
    var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<SchoolDbContext>>();
    using (var context = contextFactory.CreateDbContext())
    {
        await context.Database.MigrateAsync();
    }
}

app.Run();
