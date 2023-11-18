using System.Text;

namespace CoreLibrary;

/// <summary>
/// The interpreter class.
/// </summary>
public class Interpreter
{
    private const ushort MemorySize = ushort.MaxValue;
    private readonly byte[] _pointerArray = new byte[MemorySize];
    private ushort _dataPointer;
    private int _instructionPointer;
    private readonly Dictionary<int, int> _matchingClosingBrackets = new();
    private readonly Dictionary<int, int> _matchingOpeningBrackets = new();
    private readonly string _input;
    private readonly Dictionary<char, Action> _commandsToFunctions;
    private readonly StringBuilder? _output;
    private readonly OutputTo _outputTo;

    public Interpreter(OutputTo outputTo, string input)
    {
        _outputTo = outputTo;

        if (_outputTo == OutputTo.StringOutput)
        {
            _output = new StringBuilder();
        }

        Action outputFunction = _outputTo == OutputTo.StringOutput
            ? AddValueAtDataPointerToStringBuilderOutput
            : PrintValueAtDataPointer;

        _commandsToFunctions = new Dictionary<char, Action>
        {
            {'+', IncrementValueAtDataPointer},
            {'-', DecrementValueAtDataPointer},
            {'>', MoveRightInPointerArray},
            {'<', MoveLeftInPointerArray},
            {'.', outputFunction},
            {',', ReadKeyAndSaveAtDataPointer},
            {'[', FindMatchingClosingBracket},
            {']', FindMatchingOpeningBracket}
        };

        _input = input;

        Stack<int> stack = new();

        for (var i = 0; i < _input.Length; i++)
        {
            if (_input[i] == '[')
            {
                stack.Push(i);
            }
            else if (_input[i] == ']')
            {
                int openingBracketsIndex = stack.Pop();
                int closingBracketsIndex = i;

                _matchingClosingBrackets.Add(openingBracketsIndex, closingBracketsIndex);
                _matchingOpeningBrackets.Add(closingBracketsIndex, openingBracketsIndex);
            }
        }
    }

    private void IncrementValueAtDataPointer()
    {
        _pointerArray[_dataPointer]++;
    }

    private void DecrementValueAtDataPointer()
    {
        _pointerArray[_dataPointer]--;
    }

    private void MoveRightInPointerArray()
    {
        _dataPointer++;
    }

    private void MoveLeftInPointerArray()
    {
        _dataPointer--;
    }

    private void PrintValueAtDataPointer()
    {
        Console.Write((char) _pointerArray[_dataPointer]);
    }

    private void AddValueAtDataPointerToStringBuilderOutput()
    {
        _output!.Append((char) _pointerArray[_dataPointer]);
    }

    private void ReadKeyAndSaveAtDataPointer()
    {
        _pointerArray[_dataPointer] = (byte) Console.ReadKey().KeyChar;
    }

    private void FindMatchingClosingBracket()
    {
        if (_pointerArray[_dataPointer] == 0)
        {
            _instructionPointer = _matchingClosingBrackets[_instructionPointer];
        }
    }

    private void FindMatchingOpeningBracket()
    {
        if (_pointerArray[_dataPointer] > 0)
        {
            _instructionPointer = _matchingOpeningBrackets[_instructionPointer];
        }
    }

    public void Execute()
    {
        while (_instructionPointer < _input.Length)
        {
            _commandsToFunctions[_input[_instructionPointer]].Invoke();
            _instructionPointer++;
        }
    }

    public string GetStringOutput()
    {
        if (_outputTo == OutputTo.Stdout)
        {
            throw new NotSupportedException(
            "Interpreter object was created to output results to stdout." +
            "To have a string representing the final result, please call this class' " +
            "constructor with the correct arguments.");
        }

        return _output!.ToString();
    }
}