using VendingMachine.Business.Authentication;

namespace VendingMachine.Tests.Services.AuthenticationServiceTests
{
    [TestClass]
    public class ConstructorTests
    {
        [TestMethod]
        public void HavingAnAuthenticationServiceInstance_ThenUserIsNotAuthenticated()
        {
            AuthenticationService authenticationService = new AuthenticationService();

            Assert.IsFalse(authenticationService.IsUserAuthenticated);
        }
    }
}
