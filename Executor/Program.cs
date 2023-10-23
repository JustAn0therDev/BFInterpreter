// See https://aka.ms/new-console-template for more information

using CoreLibrary;

Console.WriteLine("Welcome to the Brainf**k Interpreter...");

Console.WriteLine("Please note that: ");
Console.WriteLine("Any invalid command will be ignored.");
Console.WriteLine("Memory usage is limited to 30000 bytes.");

while (true)
{
    Console.Write($"{DateTime.Now}: ");

    string? input = Console.ReadLine();

    if (string.IsNullOrEmpty(input))
    {
        continue;
    }

    input = input.Trim();

    // It's a file.
    if (input.EndsWith(".b"))
    {
        input = string.Join("", File.ReadAllLines(input));
    }
    else if (string.Equals(input, "exit", StringComparison.CurrentCultureIgnoreCase))
    {
        Environment.Exit(1);
    }
    
    Interpreter interpreter = new(input);

    interpreter.Execute();
}
