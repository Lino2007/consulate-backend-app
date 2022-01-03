namespace NSI.DataContracts.Oasis.Requests
{
    public class LoginRequest
    {
        public string Audience { get; set; }
        public string GrantType { get; set; }
        public string ClientId { get; set; }
        public string ClientAssertionType { get; set; }
        public string ClientAssertion { get; set; }
        public string Scope { get; set; }
    }
}
