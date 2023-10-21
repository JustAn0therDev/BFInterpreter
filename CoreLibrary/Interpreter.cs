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
    
    public Interpreter()
    {
        for (var i = 0; i < _pointerArray.Length; i++)
        {
            _pointerArray[i] = 0;
        }

        _instructionPointer = 0;
    }
    
    public void Execute(string input)
    {
        while (_instructionPointer < input.Length)
        {
            switch (input[_instructionPointer])
            {
                case '>':
                    // Wrap
                    _dataPointer++;
                    break;
                case '<':
                    // Wrap
                    _dataPointer--;
                    break;
                case '+':
                    // Wrap
                    _pointerArray[_dataPointer]++;
                    break;
                case '-':
                    // Wrap
                    _pointerArray[_dataPointer]--;
                    break;
                case '.':
                    Console.Write((char)_pointerArray[_dataPointer]);
                    break;
                case '[':
                {
                    if (_pointerArray[_dataPointer] == 0)
                    {
                        var openingBracketsCount = 0;

                        do
                        {
                            _instructionPointer++;
                            
                            if (input[_instructionPointer] == '[')
                            {
                                openingBracketsCount++;
                            }
                            else if (input[_instructionPointer] == ']' && openingBracketsCount == 0)
                            {
                                break;
                            }
                            else if (input[_instructionPointer] == ']')
                            {
                                openingBracketsCount--;
                            }
                        }
                        while (true);
                    }
                    break;
                }
                case ']':
                {
                    if (_pointerArray[_dataPointer] > 0)
                    {
                        var closingBracketsCount = 0;
                        do
                        {
                            _instructionPointer--;
                            
                            if (input[_instructionPointer] == ']')
                            {
                                closingBracketsCount++;
                            }
                            else if (input[_instructionPointer] == '[' && closingBracketsCount == 0)
                            {
                                break;
                            }  
                            else if (input[_instructionPointer] == '[')
                            {
                                closingBracketsCount--;
                            }
                            

                        }
                        while (true);
                    }
                    
                    break;
                }
            }
            
            _instructionPointer++;
        }
        
        Console.WriteLine();
    }
}