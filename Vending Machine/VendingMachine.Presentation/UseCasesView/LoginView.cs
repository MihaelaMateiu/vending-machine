using VendingMachine.Business.PresentationLayer;

namespace VendingMachine.Presentation.UseCasesView
{
    internal class LoginView : DisplayBase, ILoginView
    {
        public string AskForPassword()
        {
            Console.WriteLine();
            Display("Type the admin password: ", ConsoleColor.Cyan);
            return Console.ReadLine();
        }
    }
}