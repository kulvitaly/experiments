namespace GraphQLDemo.API.Middlewares;

public class UserAttribute() : GlobalStateAttribute(UserMiddleware.UserContextDataKey)
{
}
