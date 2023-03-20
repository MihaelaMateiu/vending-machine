using Moq;
using VendingMachine.Business.DataLayer;
using VendingMachine.Business.PresentationLayer;
using VendingMachine.Business.UseCases;

namespace VendingMachine.Tests.UseCases.LookUseCaseTests
{
    [TestClass]
    public class ConstructorTests
    {
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<IShelfView> shelfView;

        public ConstructorTests()
        {
            productRepository = new Mock<IProductRepository>();
            shelfView = new Mock<IShelfView>();
        }

        [TestMethod]
        public void HavingNullProductsRepository_WhenCallingConstructor_ThenThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new LookUseCase(null, shelfView.Object);
            });
        }

        [TestMethod]
        public void HavingNullShelfView_WhenCallingConstructor_ThenThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new LookUseCase(productRepository.Object, null);
            });
        }

        [TestMethod]
        public void WhenInitializingTheUseCase_NameIsCorrect()
        {
            LookUseCase lookUseCase = new LookUseCase(productRepository.Object, shelfView.Object);

            Assert.AreEqual("look", lookUseCase.Name);
        }

        [TestMethod]
        public void WhenInitializingTheUseCase_DescriptionHasValue()
        {
            LookUseCase lookUseCase = new LookUseCase(productRepository.Object, shelfView.Object);

            Assert.AreNotEqual("", lookUseCase.Description);
        }
    }
}
