using CoreLibrary;

namespace Tests;

public class InterpreterTests
{
    [Fact]
    public void InterpreterShouldReturnHelloWorld()
    {
        Interpreter interpreter = new(InterpretAs.StringOutput, 
            "++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.");
        interpreter.Execute();
        Assert.Equal("Hello World!\n", interpreter.GetStringOutput());
    }

    [Fact]
    public void InterpreterShouldNotReturnEmpty()
    {
        Interpreter interpreter = new(InterpretAs.StringOutput, "+.");
        interpreter.Execute();
        Assert.NotEmpty(interpreter.GetStringOutput());
    }

    [Fact]
    public void InterpreterShouldOutputEmail()
    {
        Interpreter interpreter = new(InterpretAs.StringOutput, 
            "--[----->+<]>---.++++++++++++.+.+++++++++.+[-->+<]>+++.++[-->+++<]>.++++++++++++.+.+++++++++.-[-->+++++<]>++.[--->++<]>-.-----------.");
        interpreter.Execute();
        Assert.Equal("copy@copy.sh", interpreter.GetStringOutput());
    }

    [Fact]
    public void InterpreterShouldOutputAllAsciiCharacters()
    {
        Interpreter interpreter = new(InterpretAs.StringOutput, ".+[.+]");
        interpreter.Execute();
        Assert.Contains(@"123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~", interpreter.GetStringOutput());
    }

    [Fact]
    public void InterpreterShouldThrowExceptionWithInvalidCode()
    {
        Interpreter interpreter = new(InterpretAs.StringOutput, 
            "+[-->-[>>+>-----<<]<--<---]>-.>>>+.>>..+++[.>]<<<<.+++.------.<<-.>>>>+.");
        interpreter.Execute();
        Assert.Throws<IndexOutOfRangeException>(interpreter.Execute);
    }
}