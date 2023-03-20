using System;

namespace VendingMachine.Business.AcquisitionExceptions
{
    internal class CancelException : Exception
    {
        private const string DefaultMessage = "Cancelled process";

        public CancelException()
            : base(DefaultMessage)
        {
        }

        public CancelException(string message)
            : base(message)
        {
        }
    }
}