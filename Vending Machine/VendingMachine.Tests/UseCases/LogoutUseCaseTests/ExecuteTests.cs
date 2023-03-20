using Moq;
using VendingMachine.Business.Authentication;
using VendingMachine.Business.UseCases;

namespace VendingMachine.Tests.UseCases.LogoutUseCaseTests
{
    [TestClass]
    public class ExecuteTests
    {
        [TestMethod]
        public void HavingALogoutUseCaseInstance_WhenExecuted_ThenUserIsLoggedOut()
        {
            Mock<IAuthenticationService> authenticationService = new Mock<IAuthenticationService>();
            LogoutUseCase logoutUseCase = new LogoutUseCase(authenticationService.Object);

            logoutUseCase.Execute();

            authenticationService.Verify(x => x.Logout(), Times.Once);
        }
    }
}
