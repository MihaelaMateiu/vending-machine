using Moq;
using VendingMachine.Business.Authentication;
using VendingMachine.Business.PresentationLayer;
using VendingMachine.Business.UseCases;

namespace VendingMachine.Tests.UseCases.LoginUseCaseTests
{
    [TestClass]
    public class ExecuteTests
    {
        private readonly Mock<IAuthenticationService> authenticationService;
        private readonly Mock<ILoginView> loginView;

        public ExecuteTests()
        {
            authenticationService = new Mock<IAuthenticationService>();
            loginView = new Mock<ILoginView>();
        }

        [TestMethod]
        public void HavingALoginUseCaseInstance_WhenExecuted_ThenUserIsAskedToInputPassword()
        {
            LoginUseCase loginUseCase = new LoginUseCase(authenticationService.Object, loginView.Object);

            loginUseCase.Execute();

            loginView.Verify(x => x.AskForPassword(), Times.Once);
        }

        [TestMethod]
        public void HavingALoginUseCaseInstance_WhenExecuted_ThenUserIsLoggedIn()
        {
            loginView
                .Setup(x => x.AskForPassword())
                .Returns("parola");
            LoginUseCase loginUseCase = new LoginUseCase(authenticationService.Object, loginView.Object);

            loginUseCase.Execute();

            authenticationService.Verify(x => x.Login("parola"), Times.Once);
        }
    }
}
