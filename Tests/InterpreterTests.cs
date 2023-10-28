using CoreLibrary;

namespace Tests;

public class InterpreterTests
{
    [Fact]
    public void InterpreterShouldReturnHelloWorld()
    {
        Interpreter interpreter = new("++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.");
        Assert.Equal("Hello World!\n", interpreter.Execute());
    }

    [Fact]
    public void InterpreterShouldNotReturnEmpty()
    {
        Interpreter interpreter = new("+.");
        Assert.NotEmpty(interpreter.Execute());
    }

    [Fact]
    public void InterpreterShouldOutputEmail()
    {
        Interpreter interpreter = new("--[----->+<]>---.++++++++++++.+.+++++++++.+[-->+<]>+++.++[-->+++<]>.++++++++++++.+.+++++++++.-[-->+++++<]>++.[--->++<]>-.-----------.");
        Assert.Equal("copy@copy.sh", interpreter.Execute());
    }

    [Fact]
    public void InterpreterShouldOutputAllAsciiCharacters()
    {
        Interpreter interpreter = new(".+[.+]");
        Assert.Contains(@"123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~", interpreter.Execute());
    }
}