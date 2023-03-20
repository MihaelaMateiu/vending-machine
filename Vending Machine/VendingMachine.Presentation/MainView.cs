using VendingMachine.Business;
using VendingMachine.Business.PresentationLayer;

namespace VendingMachine.Presentation
{
    internal class MainView : DisplayBase, IMainView
    {
        public void DisplayApplicationHeader()
        {
            ApplicationHeaderControl applicationHeaderControl = new ApplicationHeaderControl();
            applicationHeaderControl.Display();
        }

        public IUseCase ChooseCommand(IEnumerable<IUseCase> useCases)
        {
            CommandSelectorControl commandSelectorControl = new CommandSelectorControl
            {
                UseCases = useCases
            };
            return commandSelectorControl.Display();
        }

        public void DisplayError(Exception e)
        {
            Console.WriteLine();
            DisplayLine(e.Message, ConsoleColor.Red);
        }
    }
}