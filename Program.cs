Console.WriteLine("Hello. Welcome to the Console Application!");
Console.WriteLine("Enter empty line to exit the program.");
Console.WriteLine("Enter your commands here:");

string line;
var commandExecuter = new CommandExecuter();
while (!string.IsNullOrEmpty(line = Console.ReadLine()))
{
    commandExecuter.Execute(line.Split(' ', StringSplitOptions.RemoveEmptyEntries));
}