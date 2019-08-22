using System.Linq;
using System.Security.Claims;

namespace CommonLayer
{
    public static class UserIdentity
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            var claims = principal.Claims;

            if (claims.Any(x => x.Type.Equals(ClaimTypes.NameIdentifier)))
            {
                return claims.Where(x => x.Type.Equals(ClaimTypes.NameIdentifier)).First().Value;
            }

            return null;
        }
    }
}
