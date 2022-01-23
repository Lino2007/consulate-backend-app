using System.Diagnostics.CodeAnalysis;

namespace NSI.DataContracts.Oasis.Responses
{
    [ExcludeFromCodeCoverage]
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; }
        public string Scope { get; set; }
    }
}
