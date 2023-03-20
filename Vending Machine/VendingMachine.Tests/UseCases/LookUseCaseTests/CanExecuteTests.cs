using Moq;
using VendingMachine.Business.DataLayer;
using VendingMachine.Business.PresentationLayer;
using VendingMachine.Business.UseCases;

namespace VendingMachine.Tests.UseCases.LookUseCaseTests
{
    [TestClass]
    public class CanExecuteTests
    {
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<IShelfView> shelfView;

        public CanExecuteTests()
        {
            productRepository = new Mock<IProductRepository>();
            shelfView = new Mock<IShelfView>();
        }

        [TestMethod]
        public void HavingAnyUser_CanExecuteIsTrue()
        {
            LookUseCase lookUseCase = new LookUseCase(productRepository.Object, shelfView.Object);

            Assert.IsTrue(lookUseCase.CanExecute);
        }
    }
}
