using Assets.Scripts;
using MMansouri.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMansouri.Services
{
    public class LoginService : ILoginService
	{
		private readonly IViewManager _viewManager;
		private readonly IAuthenticationService _authenticationService;
        private static Dictionary<string, string> tokens = new Dictionary<string, string>();

		public LoginService (IViewManager viewManager, IAuthenticationService authenticationService)
		{
            if (viewManager == null || authenticationService == null)
                throw new ArgumentNullException();

			_viewManager = viewManager;
			_authenticationService = authenticationService;
		}

		public void Login (string emailAddress, string password)
		{
            AuthenticationResponse response = _authenticationService.AuthenticateUser(emailAddress, password);

            if (!string.IsNullOrEmpty(response.Token))
            {
                _viewManager.DisplaySuccessPage();
                StoreToken(emailAddress, password, response.Token);
            }
            else
            {
                _viewManager.DisplayFailurePage();
                _viewManager.SetFailureMessage(response.Reason);
            }
            _viewManager.HideSignInPage();
        }

        private void StoreToken(string emailAddress, string password, string token)
        {
            string key = Convert.ToBase64String(Encoding.ASCII.GetBytes(emailAddress + password));
            if(!tokens.ContainsKey(key))
                tokens.Add(key, Convert.ToBase64String(Encoding.ASCII.GetBytes(token)));
        }
    }
}
	


