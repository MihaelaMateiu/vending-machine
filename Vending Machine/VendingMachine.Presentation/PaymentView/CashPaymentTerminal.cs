using VendingMachine.Business.AcquisitionExceptions;
using VendingMachine.Business.PresentationLayer;

namespace VendingMachine.Presentation.PaymentView
{
    internal class CashPaymentTerminal : DisplayBase, ICashPaymentTerminal
    {
        public decimal AskForMoney()
        {
            Display("Type the amount you want to introduce in the vending machine or press enter to cancel the process: ", ConsoleColor.Cyan);
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                return 0;
            }
            else if (!decimal.TryParse(input, out _))
            {
                throw new CancelException($"Payment process was cancelled by inserting the invalid format: '{input}'");
            }

            return decimal.Parse(input);
        }

        public void GiveBackChange(decimal amount)
        {
            Console.WriteLine();
            DisplayLine($"The amount of {amount} lei has been refunded", ConsoleColor.White);
        }
    }
}
