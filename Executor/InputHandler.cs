using CoreLibrary;

namespace Executor;

public class InputHandler
{
    private readonly string _input;
    public OutputTo OutputTo { get; private set; }
    public string ArgumentToInterpret { get; private set; }

    public InputHandler(string? input)
    {
        _input = string.IsNullOrEmpty(input) ? string.Empty : input;
        ArgumentToInterpret = string.Empty;

        ReadOutputTo();
        ReadSplitInput();
    }
    
    private void ReadOutputTo()
    {
        OutputTo = _input.StartsWith("stdout") ? OutputTo.Stdout : OutputTo.StringOutput;
    }

    private void ReadSplitInput()
    {
        string[] splitInput = _input.Split(' ', StringSplitOptions.TrimEntries);

        string argumentToInterpret = splitInput.Length > 1 ? splitInput[^1] : splitInput[0];
        
        ReadArgumentToInterpret(argumentToInterpret);
        ReadCommands(argumentToInterpret);
    }

    private void ReadArgumentToInterpret(string argumentToInterpret)
    {
        if (argumentToInterpret.EndsWith(".b") || argumentToInterpret.EndsWith(".bf"))
        {
            ArgumentToInterpret = string.Join("", File.ReadAllLines(argumentToInterpret));
            return;
        }

        ArgumentToInterpret = argumentToInterpret;
    }
    
    private static void ReadCommands(string argumentToInterpret)
    {
        if (string.Equals("exit", argumentToInterpret, StringComparison.OrdinalIgnoreCase))
        {
            Environment.Exit(1);
        }
    }
}