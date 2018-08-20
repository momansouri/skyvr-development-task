using MMansouri.Contracts;

namespace MMansouri.Services
{
	public interface IAuthenticationService
	{
        AuthenticationResponse AuthenticateUser(string emailAddress, string password);
	}
}

