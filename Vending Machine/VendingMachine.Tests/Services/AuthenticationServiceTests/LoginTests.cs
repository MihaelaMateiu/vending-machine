using VendingMachine.Business.Authentication;

namespace VendingMachine.Tests.Services.AuthenticationServiceTests
{
    [TestClass]
    public class LoginTests
    {
        private const string correctPassword = "parola";

        [TestMethod]
        public void HavingAnAuthenticationService_WhenLoginWithCorrectPassword_ThenUserIsAuthenticated()
        {
            var authenticationService = new AuthenticationService();

            authenticationService.Login(correctPassword);

            Assert.IsTrue(authenticationService.IsUserAuthenticated);
        }

        [TestMethod]
        public void HavingAnAuthenticationService_WhenLoginWithInCorrectPassword_ThenThrowsException()
        {
            var authenticationService = new AuthenticationService();

            Assert.ThrowsException<InvalidPasswordException>(() =>
            {
                authenticationService.Login("incorrect-password");
            });
        }
    }
}
