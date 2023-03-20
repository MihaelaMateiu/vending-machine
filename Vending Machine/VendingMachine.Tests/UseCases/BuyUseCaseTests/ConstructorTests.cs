using Moq;
using VendingMachine.Business.Authentication;
using VendingMachine.Business.DataLayer;
using VendingMachine.Business.Payment;
using VendingMachine.Business.PresentationLayer;
using VendingMachine.Business.UseCases;

namespace VendingMachine.Tests.UseCases.BuyUseCaseTests
{
    [TestClass]
    public class ConstructorTests
    {
        private readonly Mock<IAuthenticationService> authenticationService;
        private readonly Mock<IPaymentService> paymentService;
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<IBuyView> buyView;

        public ConstructorTests()
        {
            authenticationService = new Mock<IAuthenticationService>();
            paymentService= new Mock<IPaymentService>();
            productRepository = new Mock<IProductRepository>();
            buyView = new Mock<IBuyView>();
        }

        [TestMethod]
        public void HavingNullAuthenticationService_WhenCallingConstructor_ThenThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new BuyUseCase(null, paymentService.Object, productRepository.Object, buyView.Object);
            });
        }

        public void HavingNullPaymentService_WhenCallingConstructor_ThenThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new BuyUseCase(authenticationService.Object, null, productRepository.Object, buyView.Object);
            });
        }

        [TestMethod]
        public void HavingNullProductsRepository_WhenCallingConstructor_ThenThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new BuyUseCase(authenticationService.Object, paymentService.Object, null, buyView.Object);
            });
        }

        [TestMethod]
        public void HavingNullBuyView_WhenCallingConstructor_ThenThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new BuyUseCase(authenticationService.Object, paymentService.Object, productRepository.Object, null);
            });
        }

        [TestMethod]
        public void WhenInitializingTheUseCase_NameIsCorrect()
        {
            BuyUseCase buyUseCase = new BuyUseCase(authenticationService.Object, paymentService.Object, productRepository.Object, buyView.Object);

            Assert.AreEqual("buy", buyUseCase.Name);
        }

        [TestMethod]
        public void WhenInitializingTheUseCase_DescriptionHasValue()
        {
            BuyUseCase buyUseCase = new BuyUseCase(authenticationService.Object, paymentService.Object, productRepository.Object, buyView.Object);

            Assert.AreNotEqual("", buyUseCase.Description);
        }
    }
}
