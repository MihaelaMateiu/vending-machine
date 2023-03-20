using Moq;
using VendingMachine.Business.Authentication;
using VendingMachine.Business.PresentationLayer;
using VendingMachine.Business.UseCases;

namespace VendingMachine.Tests.UseCases.LoginUseCaseTests
{
    [TestClass]
    public class ConstructorTests
    {
        private readonly Mock<IAuthenticationService> authenticationService;
        private readonly Mock<ILoginView> loginView;

        public ConstructorTests()
        {
            authenticationService = new Mock<IAuthenticationService>();
            loginView = new Mock<ILoginView>();
        }

        [TestMethod]
        public void HavingNullAuthenticationService_WhenCallingConstructor_ThenThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new LoginUseCase(null, loginView.Object);
            });
        }

        [TestMethod]
        public void HavingNullLoginView_WhenCallingConstructor_ThenThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new LoginUseCase(authenticationService.Object, null);
            });
        }

        [TestMethod]
        public void WhenInitializingTheUseCase_NameIsCorrect()
        {
            LoginUseCase loginUseCase = new LoginUseCase(authenticationService.Object, loginView.Object);

            Assert.AreEqual("login", loginUseCase.Name);
        }

        [TestMethod]
        public void WhenInitializingTheUseCase_DescriptionHasValue()
        {
            LoginUseCase loginUseCase = new LoginUseCase(authenticationService.Object, loginView.Object);

            Assert.AreNotEqual("", loginUseCase.Description);
        }
    }
}
