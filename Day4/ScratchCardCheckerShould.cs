using System.Text.RegularExpressions;

namespace Day4;

public class ScratchCardCheckerShould
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Calculate_The_Number_Of_Points_For_A_Card()
    {
        var scratchCard = """
                          Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
                          """;
        
        var points = ScratchCardChecker.CheckCard(scratchCard);
        
        Assert.That(points, Is.EqualTo(8));
    }
}

public partial class ScratchCardChecker
{
    public static double CheckCard(string scratchCard)
    {
        return CalculatePoints(FromNumberOfWinningNumbers(OfScratchCard(scratchCard)));
    }

    private static double CalculatePoints(int winningNumbers) => 1 * Math.Pow(2,winningNumbers - 1);

    private static int FromNumberOfWinningNumbers(ScratchCard scratchCard) =>
        scratchCard.Numbers.Count(n => scratchCard.WinningNumbers.Contains(n));
    
    private static ScratchCard OfScratchCard(string scratchCard) => 
        GetScratchCard(WinningNumbers(scratchCard), Numbers(scratchCard));

    private static ScratchCard GetScratchCard(IEnumerable<int> winningNumbers, IEnumerable<int> numbers) =>
        new(winningNumbers, numbers);

    private static IEnumerable<int> WinningNumbers(string scratchCard) =>
        FindNumbers(NthPartOf(SplitOnColon(NthPartOf(SplitOnPipe(scratchCard), 1)), 2));
    
    private static IEnumerable<int> Numbers(string scratchCard) =>
        FindNumbers(NthPartOf(SplitOnPipe(scratchCard), 2));

    private static string[] SplitOnPipe(string input) =>
        input.Split('|', StringSplitOptions.RemoveEmptyEntries);
    
    private static string[] SplitOnColon(string input) =>
        input.Split(':', StringSplitOptions.RemoveEmptyEntries);

    private static string NthPartOf(string[] input, int n) => input[n -1];

    private static IEnumerable<int> FindNumbers(string numbers) =>
        FindNumbers().Matches(numbers).Select(m => int.Parse(m.Value));
    
    [GeneratedRegex("\\d+")]
    private static partial Regex FindNumbers();
}

internal record ScratchCard(IEnumerable<int> WinningNumbers, IEnumerable<int> Numbers);