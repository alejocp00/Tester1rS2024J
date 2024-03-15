
using Xunit;
using System.Linq;

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MatCom.Tester;

public class UnitTest
{


    public static IEnumerable<object[]> ProblemsData()
    {
        int seed = 2024;
        int amount = 100;
        int minStringSize = 1;
        int maxStringSize = 100;
        int sArraySize = 50;
        int cArraySize = 100;

        var gestor = new ProblemGestor(seed);

        var problems = gestor.GetProblems(amount, minStringSize, maxStringSize, sArraySize, cArraySize);
        var solutions = gestor.GetSolutions();

        for (int i = 0; i < amount; i++)
        {
            yield return new object[] { problems[i].Item1, problems[i].Item2, solutions[i] };
        }

    }

    [Theory]
    [MemberData(nameof(ProblemsData))]
    public void SolvingProblems(string[] words, char[] c, string expected)
    {
        var result = Utils.SolveProblem(words, c);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(new string[] { "a" }, new char[] { }, "a")]
    public void EmptyCharArray(string[] words, char[] c, string expected)
    {
        var result = Utils.SolveProblem(words, c);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(new string[] { "a", "", "ab" }, new char[] { 'a', 'b' }, "ab")]
    public void EmptyWords(string[] words, char[] c, string expected)
    {
        var result = Utils.SolveProblem(words, c);
        Assert.Equal(expected, result);
    }

}

