using VendingMachine;
using System;

try
{
    Bootstrapper bootstrapper = new Bootstrapper();
    bootstrapper.Run();
}
catch (Exception ex)
{
    DisplayError(ex);
    Pause();
}

static void DisplayError(Exception ex)
{
    ConsoleColor oldColor = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(ex);
    Console.ForegroundColor = oldColor;
}

static void Pause()
{
    Console.WriteLine();
    Console.Write("Press any key to continue...");
    Console.ReadKey(true);
    Console.WriteLine();
}
