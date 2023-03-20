using VendingMachine.Business.AcquisitionExceptions;
using System;
using VendingMachine.Business.PresentationLayer;

namespace VendingMachine.Business.Payment
{
    internal class CashPayment : IPaymentAlgorithm
    {
        private readonly ICashPaymentTerminal cashPaymentTerminal;

        public string Name => "cash";

        public CashPayment(ICashPaymentTerminal cashPaymentTerminal)
        {
            this.cashPaymentTerminal = cashPaymentTerminal ?? throw new ArgumentNullException(nameof(cashPaymentTerminal));
        }

        public void Run(decimal price)
        {
            decimal introducedAmount, totalAmount = 0;

            while (totalAmount < price)
            {
                introducedAmount = cashPaymentTerminal.AskForMoney();

                if (introducedAmount == 0)
                {
                    if (totalAmount > 0)
                    {
                        cashPaymentTerminal.GiveBackChange(totalAmount);
                    }
                    throw new CancelException("Payment process was cancelled by user");
                }

                totalAmount += introducedAmount;
            }

            if (totalAmount > price)
            {
                cashPaymentTerminal.GiveBackChange(totalAmount - price);
            }
        }
    }
}
