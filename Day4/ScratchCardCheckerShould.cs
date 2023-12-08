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
    
    [Test]
    public void Calculate_The_Number_Of_Points_For_Multiple_Cards()
    {
        var scratchCard = """
                          Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
                          Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
                          Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
                          Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
                          Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
                          Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
                          """;
        
        var points = ScratchCardChecker.CheckCards(scratchCard);
        
        Assert.That(points, Is.EqualTo(13));
    }
    
    [Test]
    public void Solve_Part1()
    {
        var points = ScratchCardChecker.CheckCards(Part1Input.Input);
        
        Assert.That(points, Is.LessThan(0));
    }
}

public static partial class ScratchCardChecker
{
    public static double CheckCard(string scratchCard)
    {
        return CalculatePoints(FromNumberOfWinningNumbers(OfScratchCard(scratchCard)));
    }
    
    public static double CheckCards(string scratchCards)
    {
        return SumOf(SplitOnNewLine(scratchCards),
            card => CalculatePoints(FromNumberOfWinningNumbers(OfScratchCard(card))));
    }

    private static double SumOf(IEnumerable<string> scratchCards, Func<string, double> calculation) =>
        scratchCards.Sum(calculation);

    private static double CalculatePoints(int winningNumbers) =>
        winningNumbers > 0 ? 1 * Math.Pow(2, winningNumbers - 1) : 0d;

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

    private static readonly char[] NewLineSeparator = {'\r', '\n'};

    private static IEnumerable<string> SplitOnNewLine(string input) =>
        input.Split(NewLineSeparator, StringSplitOptions.RemoveEmptyEntries);

    private static string NthPartOf(IReadOnlyList<string> input, int n) => input[n -1];

    private static IEnumerable<int> FindNumbers(string numbers) =>
        FindNumbers().Matches(numbers).Select(m => int.Parse(m.Value));
    
    [GeneratedRegex("\\d+")]
    private static partial Regex FindNumbers();
}

internal record ScratchCard(IEnumerable<int> WinningNumbers, IEnumerable<int> Numbers);