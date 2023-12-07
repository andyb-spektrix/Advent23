using System.Text.RegularExpressions;

namespace Day3;

public class SchematicAnalyserShould
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Sum_PartNumbers_Adjacent_To_Symbols()
    {
        // Arrange
        var schematic = """
                        467..114..
                        ...*......
                        ..35..633.
                        ......#...
                        617*......
                        .....+.58.
                        ..592.....
                        ......755.
                        ...$.*....
                        .664.598..
                        """;

        // Act
        var sum = SchematicAnalyser.SumOfPartNumbers(schematic);

        // Assert
        Assert.That(sum, Is.EqualTo(4361));
    }
    
    [Test]
    public void Solve_Part1()
    {
        // Arrange

        // Act
        var sum = SchematicAnalyser.SumOfPartNumbers(Part1Input.Input);

        // Assert
        Assert.That(sum, Is.LessThan(0));
    }
}

public static partial class SchematicAnalyser
{
    public static long SumOfPartNumbers(string schematic) => 
        SumOf(SymbolAdjacent(PartNumbers(schematic), Symbols(schematic)));

    private static long SumOf(IEnumerable<PartNumber> partNumbers) => partNumbers.Sum(n => n.Value);

    private static IEnumerable<PartNumber> SymbolAdjacent(IEnumerable<PartNumber> partNumbers, IEnumerable<Symbol> symbols) => 
        partNumbers.Where(n => IsAdjacentToASymbol(symbols, n));

    private static bool IsAdjacentToASymbol(IEnumerable<Symbol> symbols, PartNumber n) =>
        symbols.Any(s => (s.Row - n.Row) switch
        {
            1 or 0 or -1 => IsColumnAdjacent(s, n),
            _ => false
        });

    private static bool IsColumnAdjacent(Symbol symbol, PartNumber partNumber) =>
        symbol.Column >= partNumber.Start - 1 && 
        symbol.Column <= partNumber.End + 1;

    private static IEnumerable<PartNumber> PartNumbers(string schematic) => FindNumbers(SplitOnNewLine(schematic));

    private static IEnumerable<string> SplitOnNewLine(string input) => 
        input.Split(default(char[]), StringSplitOptions.RemoveEmptyEntries);

    private static IEnumerable<Symbol> Symbols(string schematic) =>
        FindSymbols(SplitOnNewLine(schematic));
    
    private static IEnumerable<Symbol> FindSymbols(IEnumerable<string> input) => 
        input.SelectMany((i, idx) => 
            FindSymbols().Matches(i).Select(m => new Symbol(m.Index, idx)));

    private static IEnumerable<PartNumber> FindNumbers(IEnumerable<string> input) =>
        input.SelectMany((i, idx) => 
            FindPartNumbers().Matches(i).Select(m => 
                new PartNumber(int.Parse(m.Value), m.Index, m.Index + m.Length - 1, idx)));
    
    [GeneratedRegex("([0-9]+\\b)")]
    private static partial Regex FindPartNumbers();
    [GeneratedRegex("([!-/:-@-[\\.]])")]
    private static partial Regex FindSymbols();
}

public record Symbol(int Column, int Row );

public record PartNumber(long Value, int Start, int End, int Row);