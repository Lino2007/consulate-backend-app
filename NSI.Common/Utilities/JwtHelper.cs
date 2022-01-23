using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace NSI.Common.Utilities
{
    [ExcludeFromCodeCoverage]
    public static class JwtHelper
    {
        public static string GetMailFromJwtToken(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityTokenClaims = handler.ReadJwtToken(jwtToken).Claims;

            return jwtSecurityTokenClaims.Where(claim => claim.Type == "preferred_username")
                   .Select(claim => claim.Value).FirstOrDefault(); 
        }
    }
}
