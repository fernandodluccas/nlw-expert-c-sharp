using System.Text;
using RocketseatAuction.API.Entities;
using RocketseatAuction.API.Repositories;

namespace RocketseatAuction.API.Services;

public class LoggedUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public LoggedUser(IHttpContextAccessor httpContext)
    {
        _httpContextAccessor = httpContext;
    }
    
    public User User()
    {
        var repository = new RocketseatAuctionDbContext();
        
        var token = TokenOnRequest();
        var email = FromBase64(token);
        
        
        return repository.Users.First(user => user.Email.Equals(email));
    }
    
    private string TokenOnRequest()
    {
        var authentication = _httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();
        
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