using ModuleTracking;
using System.Globalization;

Console.Title = "ModuleTracking";

PrintLine("Witaj w programie ModuleTracking", ConsoleColor.White);

bool exitKeyPressed = false;

while (!exitKeyPressed)
{
    PrintLine("\n\nGłówne menu:\n", ConsoleColor.Yellow);
    PrintLine("1 - Wprowadź procent sprawnych modułów do listy i wyświetl statystyki", ConsoleColor.White);
    PrintLine("2 - Wprowadź procent sprawnych modułów do pliku i wyświetl statystyki", ConsoleColor.White);
    PrintLine("q - wyjście\n", ConsoleColor.Green);

    var pressedKey = Console.ReadLine().Trim().ToUpper();

    switch (pressedKey)
    {
        case "1":
            AddPercents(false);

            break;

        case "2":
            AddPercents(true);
            break;

        case "Q":
            exitKeyPressed = true;
            break;

        default:
            PrintLine("\n\nNieznana komenda!", ConsoleColor.DarkRed);
            break;
    }
}

void AddPercents(bool isInFile)
{
    GetModuleData(out string name, out string symbol);

    try
    {
        IModule module = isInFile ? 
            new ModuleInFile(name, symbol) :
            new ModuleInList(name, symbol);

        module.PercentOfOperatableBelow90 += MessageAtLowPercentModuleInFile;

        ShowSubMenu(module.Symbol);

        while (true)
        {
            Print("Podaj procent:", ConsoleColor.Blue);
            var input = Console.ReadLine();

            if (input == null || input.ToLower() == "q")
            {
                break;
            }
            else if (input.ToLower() == "c")
            {
                if (isInFile)
                {
                    PrintLine("\nCzy na pewno chcesz skasować cały plik?", ConsoleColor.Magenta);
                }
                else
                {
                    PrintLine("\nCzy na pewno chcesz skasować wszystki dane?", ConsoleColor.Magenta);
                }

                PrintLine("[ Y ] - kasowanie wszystkich danych.", ConsoleColor.White);
                var confirmation = Console.ReadLine();

                if (confirmation != null && confirmation.ToLower() == "y")
                {
                    module.RemoveAllPercents();
                    PrintLine("Wszystkie dane zostały usunięte.", ConsoleColor.White);
                    PrintLine("", ConsoleColor.White);
                }
                continue;
            }

            try
            {
                module.AddPercent(input);
            }
            catch (Exception ex)
            {
                PrintLine($"{ex.Message}", ConsoleColor.DarkRed);
                PrintLine("", ConsoleColor.White);
            }
        }

        var statistics = module.GetStatistics();

        ShowStatistics(statistics, module.Name, module.Symbol);

    }
    catch (Exception ex)
    {
        PrintLine(ex.Message, ConsoleColor.DarkRed);
    }
}

void GetModuleData(out string name, out string symbol)
{
    Print("\n\nWprowadź nazwę modułu:", ConsoleColor.Blue);
    name = Console.ReadLine().Trim();
    Print("Wprowadź symbol modułu:", ConsoleColor.Blue);
    symbol = Console.ReadLine().Trim().ToUpper();
}

void ShowSubMenu(string symbol)
{
    PrintLine("\n[ C ] - kasowanie wszystkich danych.", ConsoleColor.White);
    PrintLine("[ Q ] - powrót do głównego menu", ConsoleColor.White);
    Print("\nPodaj procent sprawnych modułów ", ConsoleColor.White);
    Print(symbol, ConsoleColor.Yellow);
    PrintLine(" zatwierdzając klawiszem 'enter'.", ConsoleColor.White);
}

void ShowStatistics(Statistics statistics, string name, string symbol)
{
    if (statistics.Count == 0)
    {
        PrintLine("\nBrak danych do wyświetlenia statystyki.", ConsoleColor.Blue);
    }
    else
    {
        Print("\nŚrednio sprawnych modułów ", ConsoleColor.White);
        Print(name, ConsoleColor.Yellow);
        Print(" o symbolu ", ConsoleColor.White);
        Print(symbol, ConsoleColor.Yellow);
        PrintLine($" jest {statistics.Average:N1}%", ConsoleColor.White);
        PrintLine($"Najmniej sprawnych modułów w partii produkcyjnej wynosiło {statistics.Min:N1}%, a najwięcej {statistics.Max}%", ConsoleColor.White);
    }
    PrintLine("Naciśnij klawisz, aby wrócić do głównego menu.", ConsoleColor.White);
    Console.ReadKey();
}
void MessageAtLowPercentModuleInList(object sender, EventArgs e)
{
    ModuleInList module = (ModuleInList)sender;

    PrintLine($"Skontaktuj się z przełożonym w sprawie modułu {module.Symbol}", ConsoleColor.DarkMagenta);
    PrintLine("", ConsoleColor.DarkMagenta);
}

void MessageAtLowPercentModuleInFile(object sender, EventArgs e)
{
    ModuleInFile module = (ModuleInFile)sender;

    PrintLine($"Skontaktuj się z przełożonym w sprawie modułu {module.Symbol}", ConsoleColor.DarkMagenta);
    PrintLine("", ConsoleColor.DarkMagenta);
}

void Print(string message, ConsoleColor color)
{

    Console.ForegroundColor = color;
    Console.Write(message);
    Console.ResetColor();
}

void PrintLine(string message, ConsoleColor color)
{

    Console.ForegroundColor = color;
    Console.WriteLine(message);
    Console.ResetColor();
}
