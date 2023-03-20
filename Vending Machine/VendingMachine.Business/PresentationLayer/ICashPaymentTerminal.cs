namespace VendingMachine.Business.PresentationLayer
{
    internal interface ICashPaymentTerminal
    {
        decimal AskForMoney();
        void GiveBackChange(decimal amount);
    }
}