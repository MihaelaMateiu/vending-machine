using Moq;
using VendingMachine.Business.Models;
using VendingMachine.Business.Payment;
using VendingMachine.Business.PresentationLayer;

namespace VendingMachine.Tests.Services.PaymentServiceTests
{
    [TestClass]
    public class ConstructorTests
    {
        private readonly Mock<IBuyView> buyView;
        private readonly Mock<List<IPaymentAlgorithm>> paymentAlgorithms;

        public ConstructorTests()
        {
            buyView = new Mock<IBuyView>();
            paymentAlgorithms = new Mock<List<IPaymentAlgorithm>>();
        }

        [TestMethod]
        public void HavingNullBuyView_WhenCallingConstructor_ThenThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new PaymentService(null, paymentAlgorithms.Object);
            });
        }

        [TestMethod]
        public void HavingNullPaymentAlgorithmList_WhenCallingConstructor_ThenThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new PaymentService(buyView.Object, null);
            });
        }

        [TestMethod]
        public void HavingNullPaymentAlgorithmList_WhenCallingConstructor_ThenInitializesPaymentMethodsList()
        {
            PaymentMethod paymentMethod= new PaymentMethod()
            {
                Id = 1,
                Name = "cash"
            };

            Assert.AreEqual(1, paymentMethod.Id);
            Assert.AreEqual("cash", paymentMethod.Name);
        }
    }
}
