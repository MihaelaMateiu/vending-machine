using Moq;
using VendingMachine.Business.Authentication;
using VendingMachine.Business.DataLayer;
using VendingMachine.Business.Payment;
using VendingMachine.Business.PresentationLayer;
using VendingMachine.Business.UseCases;

namespace VendingMachine.Tests.UseCases.BuyUseCaseTests
{
    [TestClass]
    public class CanExecuteTests
    {
        private readonly Mock<IAuthenticationService> authenticationService;
        private readonly Mock<IPaymentService> paymentService;
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<IBuyView> buyView;

        public CanExecuteTests()
        {
            authenticationService = new Mock<IAuthenticationService>();
            paymentService = new Mock<IPaymentService>();
            productRepository = new Mock<IProductRepository>();
            buyView = new Mock<IBuyView>();
        }

        [TestMethod]
        public void HavingAnUnAuthenticatedUser_CanExecuteIsTrue()
        {
            authenticationService
                .Setup(x => x.IsUserAuthenticated)
                .Returns(false);
            BuyUseCase buyUseCase = new BuyUseCase(authenticationService.Object, paymentService.Object, productRepository.Object, buyView.Object);

            Assert.IsTrue(buyUseCase.CanExecute);
        }

        [TestMethod]
        public void HavingAnAuthenticatedUser_CanExecuteIsFalse()
        {
            authenticationService
                .Setup(x => x.IsUserAuthenticated)
                .Returns(true);
            BuyUseCase buyUseCase = new BuyUseCase(authenticationService.Object, paymentService.Object, productRepository.Object, buyView.Object);

            Assert.IsFalse(buyUseCase.CanExecute);
        }
    }
}
