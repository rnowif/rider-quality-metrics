using System;

public class ClassWithLowTtc
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
}
