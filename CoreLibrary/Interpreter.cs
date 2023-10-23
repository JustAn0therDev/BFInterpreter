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
    
    public Interpreter(string input)
    {
        _commandsToFunctions = new()
        {
            { '+', IncrementValueAtDataPointer },
            { '-', DecrementValueAtDataPointer },
            { '>', MoveRightInPointerArray },
            { '<', MoveLeftInPointerArray },
            { '.', PrintValueAtDataPointer },
            { '[', FindMatchingClosingBracket },
            { ']', FindMatchingOpeningBracket }
        };

        int i;

        for (i = 0; i < _pointerArray.Length; i++)
        {
            _pointerArray[i] = 0;
        }

        _instructionPointer = 0;

        // Load the input file
        _input = input;

        i = 0;

        Stack<int> stack = new();

        for (; i < input.Length; i++) {
            if (_input[i] == '[') {
                stack.Push(i);
            } else if (_input[i] == ']') {
                int openingBracketsIndex = stack.Pop();
                int closingBracketsIndex = i;

                _matchingClosingBrackets.Add(openingBracketsIndex, closingBracketsIndex);
                _matchingOpeningBrackets.Add(closingBracketsIndex, openingBracketsIndex);
            }
        }
    }

    private void IncrementValueAtDataPointer() {
        _pointerArray[_dataPointer]++;
    }

    private void DecrementValueAtDataPointer() {
        _pointerArray[_dataPointer]--;
    }

    private void MoveRightInPointerArray() {
        _dataPointer++;
    }

    private void MoveLeftInPointerArray() {
        _dataPointer--;
    }

    private void PrintValueAtDataPointer() {
        Console.Write((char)_pointerArray[_dataPointer]);
    }

    private void FindMatchingClosingBracket() {
        if (_pointerArray[_dataPointer] == 0)
        {
            // Have a dictionary for opening brackets
            _instructionPointer = _matchingClosingBrackets[_instructionPointer];
        }
    }

    private void FindMatchingOpeningBracket() {
        if (_pointerArray[_dataPointer] > 0)
        {
            // Have a dictionary for closing brackets
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
        
        Console.WriteLine();
    }
}