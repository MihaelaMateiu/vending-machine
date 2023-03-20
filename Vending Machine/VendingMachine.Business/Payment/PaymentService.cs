using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Business.Models;
using VendingMachine.Business.PresentationLayer;

namespace VendingMachine.Business.Payment
{
    internal class PaymentService : IPaymentService
    {
        private readonly IBuyView buyView;

        private readonly List<PaymentMethod> paymentMethods;
        private readonly List<IPaymentAlgorithm> paymentAlgorithms;

        public PaymentService(IBuyView buyView, List<IPaymentAlgorithm> paymentAlgorithms)
        {
            this.buyView = buyView ?? throw new ArgumentNullException(nameof(buyView));
            this.paymentAlgorithms = paymentAlgorithms ?? throw new ArgumentNullException(nameof(paymentAlgorithms));

            paymentMethods = InitializePaymentMethodsList(paymentAlgorithms);
        }

        public void Execute(decimal price)
        {
            IPaymentAlgorithm selectedAlgorithm = ChoosePaymentAlgorithm(paymentAlgorithms);
            selectedAlgorithm.Run(price);
        }

        private IPaymentAlgorithm ChoosePaymentAlgorithm(IEnumerable<IPaymentAlgorithm> paymentAlgorithms)
        {
            PaymentMethod selectedPaymentMethod = FindPaymentMethod();
            return paymentAlgorithms.FirstOrDefault(algorithm => algorithm.Name == selectedPaymentMethod.Name);
        }

        private PaymentMethod FindPaymentMethod()
        {
            int paymentMethodId = buyView.AskForPaymentMethod(paymentMethods);
            return paymentMethods.FirstOrDefault(method => method.Id == paymentMethodId);
        }

        private List<PaymentMethod> InitializePaymentMethodsList(List<IPaymentAlgorithm> paymentAlgorithms)
        {
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();

            for (int i = 0; i < paymentAlgorithms.Count; i++)
            {
                paymentMethods.Add(new PaymentMethod { Id = i + 1, Name = paymentAlgorithms[i].Name });
            }

            return paymentMethods;
        }
    }
}

