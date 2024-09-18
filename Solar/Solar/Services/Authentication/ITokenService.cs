using Microsoft.AspNetCore.Identity;

namespace Solar.Services.Authentication;

public interface ITokenService
{
    public string CreateToken(IdentityUser user, string role);
}