using System.Text.RegularExpressions;
using VendingMachine.Business.AcquisitionExceptions;
using VendingMachine.Business.Models;
using VendingMachine.Business.PresentationLayer;

namespace VendingMachine.Presentation.UseCasesView
{
    internal class BuyView : DisplayBase, IBuyView
    {
        public int RequestProductId()
        {
            Display("Type the product ID or press enter to cancel the process: ", ConsoleColor.Cyan);
            string input = Console.ReadLine();
            string pattern = "^[0-9]+$";

            if (string.IsNullOrEmpty(input))
            {
                throw new CancelException("Acquisition process was cancelled by user");
            }
            else if (!Regex.IsMatch(input, pattern))
            {
                throw new CancelException($"Acquisition process was cancelled by inserting the invalid format: '{input}'");
            }

            return int.Parse(input);
        }

        public void DispenseProduct(string productName)
        {
            Console.WriteLine();
            DisplayLine($"{productName} has been dispensed", ConsoleColor.Green);
        }

        public int AskForPaymentMethod(IEnumerable<PaymentMethod> paymentMethods)
        {
            DisplayPaymentMethods(paymentMethods);
            return SelectPaymentMethod(paymentMethods);
        }

        private void DisplayPaymentMethods(IEnumerable<PaymentMethod> paymentMethods)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Available payment methods:");
            Console.WriteLine();

            foreach (PaymentMethod paymentMethod in paymentMethods)
                DisplayPaymentMethod(paymentMethod);
        }

        private static void DisplayPaymentMethod(PaymentMethod paymentMethod)
        {
            ConsoleColor oldColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(paymentMethod.Name);

            Console.ForegroundColor = oldColor;
        }

        private int SelectPaymentMethod(IEnumerable<PaymentMethod> paymentMethods)
        {
            while (true)
            {
                string rawValue = ReadPaymentMethodName();

                if (string.IsNullOrEmpty(rawValue))
                {
                    throw new CancelException("Payment process was cancelled by user");
                }

                PaymentMethod selectedPaymentMethod = FindPaymentMethod(paymentMethods, rawValue);

                if (selectedPaymentMethod != null)
                    return selectedPaymentMethod.Id;

                DisplayLine("Invalid payment method. Please try again.", ConsoleColor.Red);
            }
        }

        private PaymentMethod FindPaymentMethod(IEnumerable<PaymentMethod> paymentMethods, string rawValue)
        {
            PaymentMethod selectedPaymentMethod = null;

            foreach (PaymentMethod x in paymentMethods)
            {
                if (x.Name == rawValue)
                {
                    selectedPaymentMethod = x;
                    break;
                }
            }

            return selectedPaymentMethod;
        }

        private string ReadPaymentMethodName()
        {
            Console.WriteLine();
            Display("Choose payment method or press enter to cancel the process:  ", ConsoleColor.Cyan);
            string rawValue = Console.ReadLine();
            Console.WriteLine();

            return rawValue;
        }
    }
}
