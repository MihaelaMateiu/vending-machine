using VendingMachine.Business.PresentationLayer;

namespace VendingMachine.Business
{
    internal class VendingMachineApplication
    {
        private readonly List<IUseCase> useCases;
        private readonly IMainView mainView;

        public VendingMachineApplication(List<IUseCase> useCases, IMainView mainView)
        {
            this.useCases = useCases ?? throw new ArgumentNullException(nameof(useCases));
            this.mainView = mainView ?? throw new ArgumentNullException(nameof(mainView));
        }

        public void Run()
        {
            mainView.DisplayApplicationHeader();

            while (true)
            {
                List<IUseCase> availableUseCases = GetExecutableUseCases();

                try
                {
                    IUseCase useCase = mainView.ChooseCommand(availableUseCases);
                    useCase.Execute();
                }
                catch (Exception e)
                {
                    mainView.DisplayError(e);
                }
            }
        }

        private List<IUseCase> GetExecutableUseCases()
        {
            List<IUseCase> executableUseCases = new List<IUseCase>();

            foreach (IUseCase useCase in useCases)
            {
                if (useCase.CanExecute)
                    executableUseCases.Add(useCase);
            }

            return executableUseCases;
        }
    }
}