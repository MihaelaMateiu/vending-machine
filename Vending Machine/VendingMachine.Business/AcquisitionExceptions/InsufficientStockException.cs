using System;

namespace VendingMachine.Business.AcquisitionExceptions
{
    internal class InsufficientStockException : Exception
    {
        private const string DefaultMessage = "Insufficient stock for product: {0}";

        public InsufficientStockException()
            : base(DefaultMessage)
        {
        }

        public InsufficientStockException(string productName)
            : base(string.Format(DefaultMessage, productName))
        {
        }
    }
}
