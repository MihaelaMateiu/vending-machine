using VendingMachine.Business.Authentication;

namespace VendingMachine.Tests.Services.AuthenticationServiceTests
{
    [TestClass]
    public class LogoutTests
    {
        private const string correctPassword = "parola";

        [TestMethod]
        public void HavingAnAuthenticatedUser_WhenLogout_ThenUserIsNotAuthenticated()
        {
            var authenticationService = new AuthenticationService();
            authenticationService.Login(correctPassword);

            authenticationService.Logout();

            Assert.IsFalse(authenticationService.IsUserAuthenticated);
        }

        [TestMethod]
        public void HavingAnUnAuthenticatedUser_WhenLogout_ThenUserIsNotAuthenticated()
        {
            var authenticationService = new AuthenticationService();

            authenticationService.Logout();

            Assert.IsFalse(authenticationService.IsUserAuthenticated);
        }
    }
}
