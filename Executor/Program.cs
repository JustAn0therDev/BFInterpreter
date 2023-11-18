using CoreLibrary;
using Executor;

Console.WriteLine("Welcome to the Brainf**k Interpreter!");

Console.WriteLine("Please note that: ");
Console.WriteLine("Any invalid command will be ignored; and");
Console.WriteLine("Memory usage is limited to 30000 bytes.");

Console.WriteLine("By default, every command or file will be written to a string. To have it output normally, use the 'stdout' prefix.");
Console.WriteLine("To close the program, please use the 'exit' command");

while (true)
{
    try 
    {
        Console.Write($"{DateTime.Now.ToString("hh:mm:ss")}: ");
        
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
            Console.Write(interpreter.GetStringOutput());
        }

        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
    }
}