using Moq;
using VendingMachine.Business.Authentication;
using VendingMachine.Business.UseCases;

namespace VendingMachine.Tests.UseCases.LogoutUseCaseTests
{
    [TestClass]
    public class ConstructorTests
    {
        [TestMethod]
        public void HavingNullAuthenticationService_WhenCallingConstructor_ThenThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new LogoutUseCase(null);
            });
        }

        [TestMethod]
        public void WhenInitializingTheUseCase_NameIsCorrect()
        {
            Mock<IAuthenticationService> authenticationService = new Mock<IAuthenticationService>();
            LogoutUseCase logoutUseCase = new LogoutUseCase(authenticationService.Object);

            Assert.AreEqual("logout", logoutUseCase.Name);
        }

        [TestMethod]
        public void WhenInitializingTheUseCase_DescriptionHasValue()
        {
            Mock<IAuthenticationService> authenticationService = new Mock<IAuthenticationService>();
            LogoutUseCase logoutUseCase = new LogoutUseCase(authenticationService.Object);

            Assert.AreNotEqual("", logoutUseCase.Description);
        }
    }
}
