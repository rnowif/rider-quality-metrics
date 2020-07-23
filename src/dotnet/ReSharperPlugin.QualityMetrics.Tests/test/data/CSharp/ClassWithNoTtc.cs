using System;

public class ClassWithNoTtc
{
    private void Method2(bool camelCase)
    {
        Console.WriteLine(camelCase);
    }

    public void Method1()
    {
        Method2(true);
    }

    public void Method3()
    {
        Method2(false);
    }

    public void Method4()
    {
        Console.WriteLine("toto");
    }
}
