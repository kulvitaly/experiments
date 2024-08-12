using FirebaseAdminAuthentication.DependencyInjection.Models;
using GraphQLDemo.API.Models;
using HotChocolate.Resolvers;
using System.Security.Claims;

namespace GraphQLDemo.API.Middlewares;

public class UserMiddleware(FieldDelegate next)
{
    public const string UserContextDataKey = "User";
    private readonly FieldDelegate _next = next;

    public async ValueTask Invoke(IMiddlewareContext context)
    {
        if (context.ContextData.TryGetValue("ClaimsPrincipal", out object rawClaimsPrincipal)
            && rawClaimsPrincipal is ClaimsPrincipal claimsPrincipal)
        {
            bool emailVerified = bool.Parse(claimsPrincipal.FindFirstValue(FirebaseUserClaimType.EMAIL_VERIFIED)!);
            var user = new User
            {
                Id = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.ID)!,
                Email = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.EMAIL)!,
                Name = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.USERNAME),
                EmailVerified = emailVerified
            };

            context.ContextData.Add(UserContextDataKey, user);
        }

        await _next(context);
    }
}
