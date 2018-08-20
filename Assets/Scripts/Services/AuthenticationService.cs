using Assets.Scripts.Services;
using MMansouri.Contracts;
using System;
using System.Linq;

namespace MMansouri.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationResponse AuthenticateUser(string emailAddress, string password)
        {
            string response = string.Empty;
            Server.SignIn(emailAddress, password, x => response = x);
            return new AuthenticationResponse
            {
                Reason = GetReason(response),
                Status = GetStatus(response),
                Token = GetToken(response)
            };
        }

        private string GetToken(string response)
        {
            return (response != null && response.IndexOf(Constants.Token) != -1) ?
                response
                .Substring(response.IndexOf(Constants.Token, StringComparison.OrdinalIgnoreCase)
                            + Constants.Token.Length + 3)
                .Replace("\" }", ""):
                string.Empty;
        }

        private string GetStatus(string response)
        {
            return (response != null && response.IndexOf(Constants.Status) != -1) ?
                response.Split(new char[] { ',' }).Single(x => x.IndexOf(Constants.Status) != -1)
                        .Replace(Constants.Status, "")
                        .Replace("{ : ", "").Replace("\"", "") :
                string.Empty;
        }

        private string GetReason(string response)
        {
            return response != null &&
                response.IndexOf(Constants.Reason, StringComparison.OrdinalIgnoreCase) != -1 ?
                response
                .Substring(response.IndexOf(Constants.Reason, StringComparison.OrdinalIgnoreCase)
                            + Constants.Reason.Length + 3)
                .Replace("\" }", "") :
                string.Empty;
        }
    }
}

