
using Xunit;
using System.Linq;
using MatCom.Examen;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
namespace MatCom.Tester;

public enum TestType
{
    SolvingProblems,
    EmptyCharArray,
    EmptyWords
}

public static class Utils
{
    public static string SolveProblem(string[] words, char[] c)
    {
        return JaimesCurse.Solve(words, c);
    }
}

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

    private string[] GenerateArray(int minStringSize, int maxStringSize, int maxArraySize)
    {
        int size = new Random(Seed).Next(1, maxArraySize);
        var result = new string[size];
        for (int i = 0; i < size; i++)
        {
            result[i] = GenerateString(minStringSize, maxStringSize);
        }
        return result;
    }

    private char[] GetCharArray(int aMaxSize)
    {
        int size = randomCharSelector.Next(1, aMaxSize);
        char[] result = new char[size];

        for (int i = 0; i < size; i++)
        {
            result[i] = chars[randomCharSelector.Next(chars.Length)];
        }
        return result;

    }
    public Tuple<string[], char[]> GetProblem(int minStringSize, int maxStringSize, int sMaxArraySize, int cMaxArraySize = -1) => new Tuple<string[], char[]>(GenerateArray(minStringSize, maxStringSize, sMaxArraySize), GetCharArray(cMaxArraySize)!);
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
            return CloneProblems(problems);
        }

        var random = new Random(SeedForLimits);
        for (int i = 0; i < amount; i++)
        {
            var generator = new ProblemGenerator(Seed + i);
            problems.Add(generator.GetProblem(minWordSize, maxWordSize, arraySize, cArraySize));
        }
        return CloneProblems(problems);
    }

    private List<Tuple<string[], char[]>> CloneProblems(List<Tuple<string[], char[]>> inProblems)
    {
        var result = new List<Tuple<string[], char[]>>();
        foreach (var element in inProblems)
            result.Add(element);
        return result;
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

    private static string Solve(string[] words, char[] c)
    {
        var result = new List<string>(words);
        var currentWord = 0;

        foreach (var ch in c)
        {
            var startIndex = currentWord;

            do
            {
                if (result[currentWord].StartsWith(ch))
                {
                    result[currentWord] = result[currentWord][1..];
                    break;
                }
                currentWord = (currentWord + 1) % words.Length;
            } while (startIndex != currentWord);
        }

        return string.Join("", result);
    }

    public List<string> GetSolutions()
    {
        var results = new List<string>();
        foreach (var problem in problems)
        {
            results.Add(Solve(problem.Item1, problem.Item2));
        }
        return results;
    }
}
