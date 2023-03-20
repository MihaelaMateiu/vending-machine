using VendingMachine.Business.Models;
using System.Collections.Generic;

namespace VendingMachine.Business.PresentationLayer
{
    internal interface IBuyView
    {
        public int RequestProductId();

        public void DispenseProduct(string productName);

        public int AskForPaymentMethod(IEnumerable<PaymentMethod> paymentMethods);
    }
}
