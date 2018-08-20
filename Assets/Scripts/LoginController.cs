using UnityEngine;
using UnityEngine.UI;
using MMansouri.Services;
using Assets.Scripts;

namespace MMansouri
{
    public class LoginController : MonoBehaviour, IViewManager
    {
        private readonly ILoginService _loginService;
        public InputField Email;
        public InputField Password;
        public GameObject SignInPage;
        public GameObject SignInSuccess;
        public GameObject SignInFail;
        public Text Text;

        public LoginController()
        {
            _loginService = new LoginService(this, new AuthenticationService());
        }

        public void Login()
        {
            _loginService.Login(WWW.EscapeURL(Email.text),
                                WWW.EscapeURL(Password.text));
        }

        public void DisplaySuccessPage()
        {
            SignInSuccess.SetActive(true);
        }

        public void DisplayFailurePage()
        {
            SignInFail.SetActive(true);
        }

        public void HideSignInPage()
        {
            SignInPage.SetActive(false);
        }

        public void SetFailureMessage(string reason)
        {
            Text.text = reason;
        }
    }
}