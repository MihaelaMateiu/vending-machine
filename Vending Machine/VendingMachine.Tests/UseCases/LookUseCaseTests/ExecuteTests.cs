using Moq;
using VendingMachine.Business.DataLayer;
using VendingMachine.Business.Models;
using VendingMachine.Business.PresentationLayer;
using VendingMachine.Business.UseCases;

namespace VendingMachine.Tests.UseCases.LookUseCaseTests
{
    [TestClass]
    public class ExecuteTests
    {
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<IShelfView> shelfView;

        public ExecuteTests()
        {
            productRepository = new Mock<IProductRepository>();
            shelfView = new Mock<IShelfView>();
        }

        [TestMethod]
        public void HavingALookUseCaseInstance_WhenExecuted_ThenAllProductsAreRetrieved()
        {
            LookUseCase lookUseCase = new LookUseCase(productRepository.Object, shelfView.Object);

            lookUseCase.Execute();

            productRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [TestMethod]
        public void HavingALookUseCaseInstance_WhenExecuted_ThenAvailableProductsAreDisplayed()
        {
            // arrange
            var repositoryList = new List<Product>
            {
                new Product
                {
                Name = "Croissant",
                Price = 12,
                Quantity = 7,
                ColumnId = 5
                }
            };

            productRepository
                .Setup(x => x.GetAll())
                .Returns(repositoryList.Where(p => p.Quantity > 0));

            LookUseCase lookUseCase = new LookUseCase(productRepository.Object, shelfView.Object);

            // act
            lookUseCase.Execute();

            // assert
            var anotherList = repositoryList;
            shelfView.Verify(x => x.DisplayProducts(anotherList), Times.Once);
        }
    }
}
