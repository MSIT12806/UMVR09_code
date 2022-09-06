

using CSharp8Learning;

int k = 0;
IEnumerator<int> T1(int val, int times)
{
    for (int i = 0; i < times; i++)
    {
        Console.WriteLine($"T1 {i}");
        yield return k += 1;
    }
}
IEnumerator<int> T2(int val, int times)
{
    for (int i = 0; i < times; i++)
    {
        Console.WriteLine($"T2 {i}");
        yield return k *= 2;
    }
}

TestDel t1 = T1;
TestDel t2 = T2;

TestDel r = null;
r += t1;
r += t2;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var ms = r.GetInvocationList();
var q = Test.ChainExecuteMethods<int>(ms,0,3);
foreach (var item in q)
{

}

Console.WriteLine(k);