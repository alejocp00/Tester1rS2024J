namespace Tester;
class Program
{
    static void Main(string[] args)
    {
        var generator = new ProblemGenerator(123);
        var array = generator.GenerateArray(5, 10, 10);
        var cArray = generator.GetCharArray();
        foreach (var s in array)
        {
            Console.WriteLine(s);
        }
        foreach (var c in cArray)
        {
            Console.Write(c);
        }
    }
}