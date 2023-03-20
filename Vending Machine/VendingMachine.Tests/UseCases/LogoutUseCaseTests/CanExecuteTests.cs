using Moq;
using VendingMachine.Business.Authentication;
using VendingMachine.Business.UseCases;

namespace VendingMachine.Tests.UseCases.LogoutUseCaseTests
{
    [TestClass]
    public class CanExecuteTests
    {
        private readonly Mock<IAuthenticationService> authenticationService;

        public CanExecuteTests()
        {
            authenticationService = new Mock<IAuthenticationService>();
        }

        [TestMethod]
        public void HavingAnUnAuthenticatedUser_CanExecuteIsFalse()
        {
            authenticationService
                .Setup(x => x.IsUserAuthenticated)
                .Returns(false);
            LogoutUseCase logoutUseCase = new LogoutUseCase(authenticationService.Object);

            Assert.IsFalse(logoutUseCase.CanExecute);
        }

        [TestMethod]
        public void HavingAnAuthenticatedUser_CanExecuteIsTrue()
        {
            authenticationService
                .Setup(x => x.IsUserAuthenticated)
                .Returns(true);
            LogoutUseCase logoutUseCase = new LogoutUseCase(authenticationService.Object);

            Assert.IsTrue(logoutUseCase.CanExecute);
        }
    }
}

