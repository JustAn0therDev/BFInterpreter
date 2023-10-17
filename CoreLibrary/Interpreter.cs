namespace CoreLibrary;

/// <summary>
/// The interpreter class. For now, it's all static.
/// </summary>
public class Interpreter
{
    private readonly char[] _validCommands = { '-', '+', '<', '>', '[', ']', ',', '.' };

    private byte[] _pointerArray = new byte[30000];

    private int _pointer;

    public Interpreter()
    {
        for (var i = 0; i < _pointerArray.Length; i++)
        {
            _pointerArray[i] = 0;
        }
    }
    
    public void Execute(string input)
    {
        for (var i = 0; i < input.Length; i++)
        {
            char ch = input[i];
            
            if (!_validCommands.Contains(ch))
            {
                continue;
            }

            switch (ch)
            {
                case '>':
                    _pointer++;
                    break;
                case '<':
                    _pointer--;
                    break;
                case '+':
                    _pointerArray[_pointer]++;
                    break;
                case '-':
                    _pointerArray[_pointer]--;
                    break;
                case '.':
                    Console.Write((char)_pointerArray[_pointer]);
                    break;
                case '[':
                {
                    if (_pointerArray[_pointer] == 0)
                    {
                        do
                        {
                            i++;
                        } while (input[i] != ']' && i < input.Length);

                        i++;
                    }
                    break;
                }

                case ']':
                    if (_pointerArray[_pointer] != 0)
                    {
                        do
                        {
                            i--;
                        } while (input[i] != '[' && i > 0);
                    }
                    break;
            }
        }
        
        Console.WriteLine();
    }
}