namespace VendingMachine.Business.PresentationLayer
{
    internal interface IMainView
    {
        IUseCase ChooseCommand(IEnumerable<IUseCase> useCases);
        void DisplayApplicationHeader();
        void DisplayError(Exception e);
    }
}