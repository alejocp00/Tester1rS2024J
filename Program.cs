namespace Tester;
class Program
{
    static void Main(string[] args)
    {
        var generator = new ProblemGenerator(123);
        var (array, cArray) = generator.GetProblem(10, 20, 5);
        var (array2, cArray2) = generator.GetProblem(10, 20, 5);

        foreach (var s in array)
        {
            Console.WriteLine(s);
        }
        foreach (var c in cArray)
        {
            Console.Write(c);
        }

        Console.WriteLine("----");
        foreach (var s in array2)
        {
            Console.WriteLine(s);
        }
        foreach (var c in cArray2)
        {
            Console.Write(c);
        }

    }
}

// todo: asd