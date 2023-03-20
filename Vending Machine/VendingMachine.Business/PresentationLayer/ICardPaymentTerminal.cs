namespace VendingMachine.Business.PresentationLayer
{
    internal interface ICardPaymentTerminal
    {
        string AskForCardNumber();
        void DisplayAttempts(int attemptsNumber);
    }
}