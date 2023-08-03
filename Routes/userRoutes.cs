using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

public class UserRoutes
{
    public static void MapRoutes(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapControllerRoute(
            name: "RegisterUser",
            pattern: "user/register",
            defaults: new { controller = "User", action = "RegisterUser" }
        );

        endpoints.MapControllerRoute(
            name: "GetAuth",
            pattern: "user/authenticate/{token}",
            defaults: new { controller = "User", action = "GetAuth" }
        );
    }
}
