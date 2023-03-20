using Moq;
using VendingMachine.Business.Models;
using VendingMachine.Business.Payment;
using VendingMachine.Business.PresentationLayer;

namespace VendingMachine.Tests.Services.PaymentServiceTests
{
    [TestClass]
    public class ExecuteTests
    {
        private readonly Mock<IBuyView> buyView;
        private readonly List<Mock<IPaymentAlgorithm>> paymentAlgorithms;

        private readonly IPaymentService paymentService;
        private readonly List<PaymentMethod> paymentMethods;


        public ExecuteTests()
        {
            buyView = new Mock<IBuyView>();

            paymentAlgorithms = new List<Mock<IPaymentAlgorithm>>()
            {
                new Mock<IPaymentAlgorithm>(),
                new Mock<IPaymentAlgorithm>()
            };

            paymentAlgorithms[0]
                .Setup(x => x.Name)
                .Returns("cash");

            paymentAlgorithms[1]
                .Setup(x => x.Name)
                .Returns("card");

            var paymentAlgorithmsList =  paymentAlgorithms.Select(x => x.Object).ToList();

            paymentService = new PaymentService(buyView.Object, paymentAlgorithmsList);

            paymentMethods = new List<PaymentMethod>();

            for (int i = 0; i < paymentAlgorithms.Count; i++)
            {
                paymentMethods.Add(new PaymentMethod { Id = i + 1, Name = paymentAlgorithms[i].Name });
            }
        }

        [TestMethod]
        public void HavingAPaymentServiceInstance_WhenExecuted_ThenAsksForPaymentMethod()
        {
            // arrange
            buyView
                .Setup(x => x.AskForPaymentMethod(It.IsAny<List<PaymentMethod>>()))
                .Returns(1);

            // act
            paymentService.Execute(It.IsAny<decimal>());

            // assert
            buyView.Verify(x => x.AskForPaymentMethod(It.IsAny<List<PaymentMethod>>()), Times.Once);
        }

        [TestMethod]
        public void HavingAPaymentServiceInstance_WhenExecuted_ThenChoosesPaymentAlgorithm()
        {
            // arrange
            buyView
               .Setup(x => x.AskForPaymentMethod(It.IsAny<List<PaymentMethod>>()))
               .Returns(1);

            paymentAlgorithms
                .ForEach(x => x.Setup(x => x.Name)
                .Returns("cash"));

            // act
            paymentService.Execute(It.IsAny<decimal>());

            // assert
            buyView.Verify(x => x.AskForPaymentMethod(It.IsAny<List<PaymentMethod>>()), Times.Once);
        }

        [TestMethod]
        public void HavingAPaymentServiceInstance_WhenExecuted_ThenRunsTheChoosenAlgorithm()
        {
            // arrange
            buyView
               .Setup(x => x.AskForPaymentMethod(It.IsAny<List<PaymentMethod>>()))
               .Returns(1);

            paymentAlgorithms[0]
               .Setup(x => x.Name)
               .Returns("cash");

            paymentAlgorithms[1]
                .Setup(x => x.Name)
                .Returns("card");

            // act
            paymentService.Execute(It.IsAny<decimal>());

            // assert
            paymentAlgorithms[0].Verify(x => x.Run(It.IsAny<decimal>()), Times.Once);
        }
    }
}
