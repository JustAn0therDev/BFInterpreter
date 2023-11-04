using CoreLibrary;

Console.WriteLine("Welcome to the Brainf**k Interpreter!");

Console.WriteLine("Please note that: ");
Console.WriteLine("Any invalid command will be ignored; and");
Console.WriteLine("Memory usage is limited to 30000 bytes.");

while (true)
{
    Console.Write($"{DateTime.Now}: ");

    string? input = Console.ReadLine();

    if (string.IsNullOrEmpty(input))
    {
        continue;
    }

    var outputTo = input.StartsWith("stdout") ? OutputTo.Stdout : OutputTo.StringOutput;

    string[] splitInput = input.Split(' ', StringSplitOptions.TrimEntries);

    string argumentToInterpret = splitInput.Length > 1 ? splitInput[^1] : splitInput[0];

    // It's a file.
    if (argumentToInterpret.EndsWith(".b") || argumentToInterpret.EndsWith(".bf"))
    {
        argumentToInterpret = string.Join("", File.ReadAllLines(argumentToInterpret));
    }
    else if (string.Equals(argumentToInterpret, "exit", StringComparison.OrdinalIgnoreCase))
    {
        Environment.Exit(1);
    }

    Interpreter interpreter = new(outputTo, argumentToInterpret);

    interpreter.Execute();

    if (outputTo == OutputTo.StringOutput)
    {
        Console.WriteLine(interpreter.GetStringOutput());
    }
}