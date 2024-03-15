
using System.Text;
using System.Text.Json;

namespace Tester;
public class ProblemGenerator
{
    public int Seed { get; }
    private Random randomCharSelector { get; }
    private Random randomWordSizeSelector { get; }
    private char[] chars = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'ñ', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

    public ProblemGenerator(int seed)
    {
        Seed = seed;
        randomCharSelector = new Random(seed);
        randomWordSizeSelector = new Random(seed + 1);

    }

    private string GenerateString(int minSize, int maxSize)
    {
        var size = randomWordSizeSelector.Next(minSize, maxSize);
        var result = new StringBuilder(size);
        for (int i = 0; i < size; i++)
        {
            result.Append(chars[randomCharSelector.Next(chars.Length)]);
        }

        return result.ToString();
    }

    private string[] GenerateArray(int minStringSize, int maxStringSize, int arraySize)
    {
        var result = new string[arraySize];
        for (int i = 0; i < arraySize; i++)
        {
            result[i] = GenerateString(minStringSize, maxStringSize);
        }
        return result;
    }

    private char[] GetCharArray(int aSize = -1)
    {
        int maxArraySize = 100;
        int size = aSize == -1 ? randomCharSelector.Next(0, maxArraySize) : aSize;
        char[] result = new char[size];

        for (int i = 0; i < size; i++)
        {
            result[i] = chars[randomCharSelector.Next(chars.Length)];
        }
        return result;

    }
    public Tuple<string[], char[]> GetProblem(int minStringSize, int maxStringSize, int sArraySize, int cArraySize = -1) => new Tuple<string[], char[]>(GenerateArray(minStringSize, maxStringSize, sArraySize), GetCharArray(cArraySize)!);
}

public class ProblemGestor
{
    public int Seed { get; }
    public int SeedForLimits { get; }
    private List<Tuple<string[], char[]>> problems { get; }
    public ProblemGestor(int seed)
    {
        Seed = seed;
        SeedForLimits = seed - 1;
        problems = new List<Tuple<string[], char[]>>();
    }

    public List<Tuple<string[], char[]>> GetProblems(int amount, int minWordSize, int maxWordSize, int arraySize, int cArraySize)
    {

        if (problems.Count > 0)
        {
            return problems;
        }

        var random = new Random(SeedForLimits);
        for (int i = 0; i < amount; i++)
        {
            var generator = new ProblemGenerator(Seed + i);
            problems.Add(generator.GetProblem(minWordSize, maxWordSize, arraySize, cArraySize));
        }
        return problems;
    }

    public void ExportProblems(string path)
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("seed", Seed);
        data.Add("seedForLimits", SeedForLimits);
        data.Add("problems", problems);
        var json = JsonSerializer.Serialize(data);
        File.WriteAllText(path, json);
    }
}