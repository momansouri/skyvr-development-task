namespace MMansouri.Contracts
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
    }
}