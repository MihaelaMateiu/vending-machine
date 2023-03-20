namespace VendingMachine.Business.Payment
{
    internal interface IPaymentService
    {
        public void Execute(decimal price);
    }
}
