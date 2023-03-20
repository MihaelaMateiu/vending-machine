using VendingMachine.Business.AcquisitionExceptions;
using System;
using VendingMachine.Business.PresentationLayer;

namespace VendingMachine.Business.Payment
{
    internal class CardPayment : IPaymentAlgorithm
    {
        private readonly ICardPaymentTerminal cardPaymentTerminal;

        public string Name => "credit card";

        public CardPayment(ICardPaymentTerminal cardPaymentTerminal)
        {
            this.cardPaymentTerminal = cardPaymentTerminal ?? throw new ArgumentNullException(nameof(cardPaymentTerminal));
        }

        public void Run(decimal price)
        {
            bool isValidCard = false;
            int attemptsNumber = 3;

            while (!isValidCard)
            {
                string cardNumber = cardPaymentTerminal.AskForCardNumber();

                if (string.IsNullOrEmpty(cardNumber))
                {
                    throw new CancelException("Payment process was cancelled by user");
                }

                isValidCard = IsValidCard(cardNumber);

                if (!isValidCard && attemptsNumber > 0)
                {
                    cardPaymentTerminal.DisplayAttempts(attemptsNumber);
                    attemptsNumber--;
                    //continue;
                }
                else if (attemptsNumber == 0)
                {
                    throw new CancelException("No more attempts left");
                }

                //break;
            }
        }

        private static bool IsValidCard(string cardNumber)
        {
            int digitsNumber = cardNumber.Length;

            int sum = 0;
            bool isSecond = false;
            for (int i = digitsNumber - 1; i >= 0; i--)
            {
                int digit = cardNumber[i] - '0';

                if (isSecond == true)
                {
                    digit *= 2;
                }

                sum += digit / 10;
                sum += digit % 10;

                isSecond = !isSecond;
            }
            return sum % 10 == 0;
        }

        // example of valid card number: 79927398713
    }
}
