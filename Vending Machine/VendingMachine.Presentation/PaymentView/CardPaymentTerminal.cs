using System.Text.RegularExpressions;
using VendingMachine.Business.AcquisitionExceptions;
using VendingMachine.Business.PresentationLayer;

namespace VendingMachine.Presentation.PaymentView
{
    internal class CardPaymentTerminal : DisplayBase, ICardPaymentTerminal
    {
        public string AskForCardNumber()
        {
            Display("Type the card number or press enter to cancel the process: ", ConsoleColor.Cyan);
            string input = Console.ReadLine();
            string pattern = "^[0-9]+$";

            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            else if (!Regex.IsMatch(input, pattern))
            {
                throw new CancelException($"Acquisition process was cancelled by inserting the invalid format: '{input}'");
            }

            return input;
        }

        public void DisplayAttempts(int attemptsNumber)
        {
            DisplayLine($"Invalid card number. You have {attemptsNumber} attempts left", ConsoleColor.White);
            Console.WriteLine();
        }
    }
}
