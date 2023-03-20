using Moq;
using VendingMachine.Business.Authentication;
using VendingMachine.Business.PresentationLayer;
using VendingMachine.Business.UseCases;

namespace VendingMachine.Tests.UseCases.LoginUseCaseTests
{
    [TestClass]
    public class CanExecuteTests
    {
        private readonly Mock<IAuthenticationService> authenticationService;
        private readonly Mock<ILoginView> loginView;

        public CanExecuteTests()
        {
            authenticationService = new Mock<IAuthenticationService>();
            loginView = new Mock<ILoginView>();
        }

        [TestMethod]
        public void HavingAnUnAuthenticatedUser_CanExecuteIsTrue()
        {
            authenticationService
                .Setup(x => x.IsUserAuthenticated)
                .Returns(false);
            LoginUseCase loginUseCase = new LoginUseCase(authenticationService.Object, loginView.Object);

            Assert.IsTrue(loginUseCase.CanExecute);
        }

        [TestMethod]
        public void HavingAnAuthenticatedUser_CanExecuteIsFalse()
        {
            authenticationService
                .Setup(x => x.IsUserAuthenticated)
                .Returns(true);
            LoginUseCase loginUseCase = new LoginUseCase(authenticationService.Object, loginView.Object);

            Assert.IsFalse(loginUseCase.CanExecute);
        }
    }
}
