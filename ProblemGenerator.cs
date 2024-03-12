
using System.Text;

namespace Tester;
public class ProblemGenerator
{
    public int Seed { get; }
    private Random randomCharSelector { get; }
    private Random randomWordSizeSelector { get; }

    private string[]? generatedArray;
    private char[]? generatedCharArray;

    public ProblemGenerator(int seed)
    {
        Seed = seed;
        randomCharSelector = new Random(seed);
        randomWordSizeSelector = new Random(seed + 1);

    }

    private string GenerateString(int minSize, int maxSize)
    {
        char[] chars = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'ñ', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };


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
        generatedArray = result.Clone() as string[];
        return result;
    }

    private char[]? GetCharArray(int aSize = -1)
    {
        if (generatedArray == null)
        {
            throw new InvalidOperationException("Array not generated");
        }

        int size = aSize == -1 ? randomCharSelector.Next(0, generatedArray.Length - 1) : aSize;
        char[] result = new char[size];

        List<Tuple<int, int>> indexes = new List<Tuple<int, int>>();

        for (int i = 0; i < result.Length; i++)
        {
            var wordIndex = randomCharSelector.Next(0, generatedArray.Length - 1);
            var charIndex = randomCharSelector.Next(0, generatedArray[wordIndex].Length - 1);
            indexes.Add(new Tuple<int, int>(wordIndex, charIndex));
        }

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = generatedArray[indexes[i].Item1][indexes[i].Item2];
        }

        generatedCharArray = result.Clone() as char[];
        return result;
    }
    public Tuple<string[], char[]> GetProblem(int minStringSize, int maxStringSize, int sArraySize, int cArraySize = -1) => new Tuple<string[], char[]>(GenerateArray(minStringSize, maxStringSize, sArraySize), GetCharArray(cArraySize)!);
}