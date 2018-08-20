using NUnit.Framework;
using MMansouri.Services;
using Moq;
using Assets.Scripts;
using System;
using MMansouri.Contracts;

public class NewEditModeTest
{

    private LoginService target;
    private Mock<IAuthenticationService> _authenticationService;
    private Mock<IViewManager> _viewManager;

    [SetUp]
    public void Init()
    {
        _authenticationService = new Mock<IAuthenticationService>();
        _viewManager = new Mock<IViewManager>();
        target = new LoginService(_viewManager.Object, _authenticationService.Object);
    }

    [Test]
    public void LoginService_Throws_When_Null_ViewManager()
    {
        //Act, Assert
        Assert.Throws<ArgumentNullException>(() => new LoginService(null, _authenticationService.Object));
    }

    [Test]
    public void LoginService_Throws_When_Null_AuthenticationService()
    {
        //Act, Assert
        Assert.Throws<ArgumentNullException>(() => new LoginService(_viewManager.Object, null));
    }

    [Test]
    public void Login_Sends_Authentication_Request()
    {
        //Arrange
        const string emailAddress = "testUser";
        const string password = "testPassword";

        _authenticationService.Setup(x => x.AuthenticateUser(emailAddress, password))
            .Returns(new AuthenticationResponse
            {
                Token = "12"
            });

        //Act
        target.Login(emailAddress, password);

        //Assert
        _authenticationService.Verify(x => x.AuthenticateUser(emailAddress, password), Times.Once());
    }

    [Test]
    public void Login_Displays_SuccessPage_When_Successful_Authentication()
    {
        //Arrange
        const string emailAddress = "testUser";
        const string password = "testPassword";
        
        _authenticationService.Setup(x => x.AuthenticateUser(emailAddress, password))
        .Returns(new AuthenticationResponse
        {
            Token = "qwwq123"
        });

        //Act
        target.Login(emailAddress, password);

        //Assert
        _viewManager.Verify(x => x.DisplaySuccessPage(), Times.Once());
    }

    [Test]
    public void Login_Hides_SignInPage_When_Successful_Authentication()
    {
        //Arrange
        const string emailAddress = "testUser";
        const string password = "testPassword";

        _authenticationService.Setup(x => x.AuthenticateUser(emailAddress, password))
            .Returns(new AuthenticationResponse
            {
                Token = "qw1212"
            });

        //Act
        target.Login(emailAddress, password);

        //Assert
        _viewManager.Verify(x => x.HideSignInPage(), Times.Once());
    }

    [Test]
    public void Login_Displays_FailurePage_When_Failed_Authentication()
    {
        //Arrange
        const string emailAddress = "testUser";
        const string password = "testPassword";

        _authenticationService.Setup(x => x.AuthenticateUser(emailAddress, password))
            .Returns(new AuthenticationResponse
            {
                Status = "false",
                Reason = "Invalid credentials"
            });

        //Act
        target.Login(emailAddress, password);

        //Assert
        _viewManager.Verify(x => x.DisplayFailurePage(), Times.Once());
    }

    [Test]
    public void Login_Hides_SignInPage_When_Failed_Authentication()
    {
        //Arrange
        const string emailAddress = "testUser";
        const string password = "testPassword";

        _authenticationService.Setup(x => x.AuthenticateUser(emailAddress, password))
            .Returns(new AuthenticationResponse
            {
                Status = "false",
                Reason = "Invalid credentials"
            });

        //Act
        target.Login(emailAddress, password);

        //Assert
        _viewManager.Verify(x => x.HideSignInPage(), Times.Once());
    }

    [Test]
    public void Login_Sets_FailureMessage_When_Failed_Authentication()
    {
        //Arrange
        const string emailAddress = "testUser";
        const string password = "testPassword";
        var response = new AuthenticationResponse
        {
            Status = "false",
            Reason = "Invalid credentials"
        };
        _authenticationService.Setup(x => x.AuthenticateUser(emailAddress, password))
            .Returns(response);

        //Act
        target.Login(emailAddress, password);

        //Assert
        _viewManager.Verify(x => x.SetFailureMessage(response.Reason), Times.Once());
    }
}
