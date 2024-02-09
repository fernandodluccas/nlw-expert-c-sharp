using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RocketseatAuction.API.Repositories;

namespace RocketseatAuction.API.Filters;

public class AuthenticationUserAttribute: AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context.HttpContext);

            var repository = new RocketseatAuctionDbContext();

            var exists = repository.Users.Any(user => user.Email.Equals(FromBase64(token)));

            if (exists == false)
            {
                context.Result = new UnauthorizedObjectResult("Invalid credentials");
            }
        }
        catch (Exception e)
        {
            context.Result = new UnauthorizedObjectResult(e.Message);
        }
    }
    
    private string TokenOnRequest(HttpContext context)
    {
        var authentication = context.Request.Headers.Authorization.ToString();
        
        if (string.IsNullOrEmpty(authentication) || !authentication.StartsWith("Bearer "))
        {
            throw new Exception("Token not found");
        }
        return authentication["Bearer ".Length..];
    }
    
    private string FromBase64(string base64)
    {
        var data = Convert.FromBase64String(base64);
        return Encoding.UTF8.GetString(data);
    }
    
}