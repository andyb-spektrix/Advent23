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
    public void Sum_Gear_Ratios()
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
        var sum = SchematicAnalyser.SumOfGearRatios(schematic);

        // Assert
        Assert.That(sum, Is.EqualTo(467835));
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
    
    [Test]
    public void Solve_Part2()
    {
        // Arrange

        // Act
        var sum = SchematicAnalyser.SumOfGearRatios(Part2Input.Input);

        // Assert
        Assert.That(sum, Is.LessThan(0));
    }
}

public static partial class SchematicAnalyser
{
    public static long SumOfGearRatios(string schematic) =>
        SumOf(GearRatios(PartNumbers(schematic), GearSymbols(schematic)), r => r);
    public static long SumOfPartNumbers(string schematic) => 
        SumOf(SymbolAdjacent(PartNumbers(schematic), Symbols(schematic)), n => n.Value);

    private static long SumOf<T>(IEnumerable<T> partNumbers, Func<T,long> selector) => partNumbers.Sum(selector);

    private static IEnumerable<long> GearRatios(IEnumerable<PartNumber> partNumbers, IEnumerable<Symbol> symbols) =>
        CalculateRatioOf(Gears(partNumbers, symbols));

    private static IEnumerable<long> CalculateRatioOf(IEnumerable<Gear> gears) =>
        gears.Select(g => g.One.Value * g.Two.Value);

    private static IEnumerable<Gear> Gears(IEnumerable<PartNumber> partNumbers, IEnumerable<Symbol> symbols) =>
        symbols.Select(s => AsAGear(s, partNumbers));

    private static IEnumerable<PartNumber> AdjacentPartNumbers(Symbol symbol, IEnumerable<PartNumber> partNumbers) =>
        partNumbers.Where(n => IsAdjacentToASymbol(new[] { symbol }, n));

    private static Gear AsAGear(Symbol symbol, IEnumerable<PartNumber> partNumbers) =>
        AdjacentPartNumbers(symbol, partNumbers).ToArray() switch
        {
            { Length: 2 } x => new Gear(x.First(), x.Last()),
            _ => new Gear(EmptyPartNumber(), EmptyPartNumber())
        };

    private static PartNumber EmptyPartNumber()
    {
        return new PartNumber(0, 0, 0, 0);
    }

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

    private static IEnumerable<Symbol> GearSymbols(string schematic) =>
        FindGearSymbols(SplitOnNewLine(schematic));
    
    private static IEnumerable<Symbol> FindGearSymbols(IEnumerable<string> input) => 
        input.SelectMany((i, idx) => 
            FindGearSymbols().Matches(i).Select(m => new Symbol(m.Index, idx)));
    
    [GeneratedRegex("([0-9]+\\b)")]
    private static partial Regex FindPartNumbers();
    [GeneratedRegex("([!-/:-@-[\\.]])")]
    private static partial Regex FindSymbols();
    [GeneratedRegex("([*])")]
    private static partial Regex FindGearSymbols();
}

public record Symbol(int Column, int Row );

public record PartNumber(long Value, int Start, int End, int Row);

public record Gear(PartNumber One, PartNumber Two);