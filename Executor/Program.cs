using CoreLibrary;
using Executor;

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

    InputHandler inputHandler = new(input);
    
    Interpreter interpreter = new(inputHandler.OutputTo, inputHandler.ArgumentToInterpret);
    interpreter.Execute();

    if (inputHandler.OutputTo == OutputTo.StringOutput)
    {
        Console.WriteLine(interpreter.GetStringOutput());
    }
}