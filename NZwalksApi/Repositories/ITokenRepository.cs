using Microsoft.AspNetCore.Identity;

namespace NZwalksApi.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);

    }
}
