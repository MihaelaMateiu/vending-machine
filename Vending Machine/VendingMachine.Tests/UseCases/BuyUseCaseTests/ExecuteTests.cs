using Moq;
using VendingMachine.Business.AcquisitionExceptions;
using VendingMachine.Business.Authentication;
using VendingMachine.Business.DataLayer;
using VendingMachine.Business.Models;
using VendingMachine.Business.Payment;
using VendingMachine.Business.PresentationLayer;
using VendingMachine.Business.UseCases;

namespace VendingMachine.Tests.UseCases.BuyUseCaseTests
{
    [TestClass]
    public class ExecuteTests
    {
        private readonly Mock<IAuthenticationService> authenticationService;
        private readonly Mock<IPaymentService> paymentService;
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<IBuyView> buyView;

        public ExecuteTests()
        {
            authenticationService = new Mock<IAuthenticationService>();
            paymentService = new Mock<IPaymentService>();
            productRepository = new Mock<IProductRepository>();
            buyView = new Mock<IBuyView>();
        }

        [TestMethod]
        public void HavingABuyUseCaseInstance_WhenExecuted_ThenUserIsAskedToInputProductId()
        { 
            productRepository
                .Setup(x => x.GetByColumnId(It.IsAny<int>()))
                .Returns(new Product { Quantity = 1 });
            BuyUseCase buyUseCase = new BuyUseCase(authenticationService.Object, paymentService.Object, productRepository.Object, buyView.Object);

            buyUseCase.Execute();

            buyView.Verify(x => x.RequestProductId(), Times.Once);
        }

        [TestMethod]
        public void HavingABuyUseCaseInstance_WhenExecuted_ThenGetsProductByColumnId()
        {
            // arrange
            buyView
                .Setup(x => x.RequestProductId())
                .Returns(5);

            productRepository
                .Setup(x => x.GetByColumnId(It.IsAny<int>()))
                .Returns(new Product { Quantity = 1 });

            BuyUseCase buyUseCase = new BuyUseCase(authenticationService.Object, paymentService.Object, productRepository.Object, buyView.Object);

            // act
            buyUseCase.Execute();
            
            // assert
            productRepository.Verify(x => x.GetByColumnId(5), Times.Once);
        }

        [TestMethod]
        public void HavingABuyUseCaseInstance_WhenExecuted_ThenThrowsInvalidColumnIdException()
        {
            productRepository
                .Setup(x => x.GetByColumnId(It.IsAny<int>()));
            BuyUseCase buyUseCase = new BuyUseCase(authenticationService.Object, paymentService.Object, productRepository.Object, buyView.Object);

            Assert.ThrowsException<InvalidColumnIdException>(() =>
            {
                buyUseCase.Execute();
            });
        }

        [TestMethod]
        public void HavingABuyUseCaseInstance_WhenExecuted_ThenThrowsInsufficientStockException()
        {
            productRepository
                .Setup(x => x.GetByColumnId(It.IsAny<int>()))
                .Returns(new Product { Quantity = 0 });
            BuyUseCase buyUseCase = new BuyUseCase(authenticationService.Object, paymentService.Object, productRepository.Object, buyView.Object);

            Assert.ThrowsException<InsufficientStockException>(() =>
            {
                buyUseCase.Execute();
            });
        }

        [TestMethod]
        public void HavingABuyUseCaseInstance_WhenExecuted_ThenExecutesPaymentService()
        {
            // arrange
            var product = new Product { Name = "Croissant", Price = 12, Quantity = 1 };

            productRepository
                .Setup(x => x.GetByColumnId(It.IsAny<int>()))
                .Returns(product);

            BuyUseCase buyUseCase = new BuyUseCase(authenticationService.Object, paymentService.Object, productRepository.Object, buyView.Object);

            // act
            buyUseCase.Execute();

            // assert
            paymentService.Verify(x => x.Execute(product.Price), Times.Once);
        }

        [TestMethod]
        public void HavingABuyUseCaseInstance_WhenExecuted_ThenStockIsDecremented()
        {
            // arrange
            var product = new Product { Quantity = 1 };

            productRepository
                .Setup(x => x.GetByColumnId(It.IsAny<int>()))
                .Returns(product);

            BuyUseCase buyUseCase = new BuyUseCase(authenticationService.Object, paymentService.Object, productRepository.Object, buyView.Object);

            // act
            buyUseCase.Execute();

            // assert
            Assert.AreEqual(0, product.Quantity);
        }

        [TestMethod]
        public void HavingABuyUseCaseInstance_WhenExecuted_ThenProductIsDispensed()
        {
            // arrange
            var product = new Product { Name = "Croissant", Quantity = 1 };

            productRepository
                .Setup(x => x.GetByColumnId(It.IsAny<int>()))
                .Returns(product);

            BuyUseCase buyUseCase = new BuyUseCase(authenticationService.Object, paymentService.Object, productRepository.Object, buyView.Object);

            // act
            buyUseCase.Execute();

            // assert
            buyView.Verify(x => x.DispenseProduct(product.Name), Times.Once);
        }
    }
}
