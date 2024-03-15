using System.Text;

namespace Tester;
class Program
{
    static void Main(string[] args)
    {
        var problemGestor = new ProblemGestor(123);

        foreach (var problem in problemGestor.GetProblems(4, 0, 50, 100, 20))
        {
            Console.WriteLine($"Strings: {string.Join(", ", problem.Item1)}");
            Console.WriteLine($"Chars: {string.Join(", ", problem.Item2)}");
            Console.WriteLine();
        }
    }
}

