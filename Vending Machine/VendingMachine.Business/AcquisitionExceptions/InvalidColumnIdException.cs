using System;

namespace VendingMachine.Business.AcquisitionExceptions
{
    internal class InvalidColumnIdException : Exception
    {
        private const string DefaultMessage = "Invalid product Id inserted: {0}";

        public InvalidColumnIdException()
            : base(DefaultMessage)
        {
        }

        public InvalidColumnIdException(int productId)
            : base(string.Format(DefaultMessage, productId))
        {
        }
    }
}
