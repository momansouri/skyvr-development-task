namespace Assets.Scripts
{
    public interface IViewManager
    {
        void DisplaySuccessPage();

        void DisplayFailurePage();

        void HideSignInPage();

        void SetFailureMessage(string reason);
    }
}
