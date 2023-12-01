using NUnit.Framework.Constraints;

namespace Day1;

public class CalibrationCalculatorShould
{
    public static IEnumerable<IEnumerable<string>> Day1Calibration = new List<IEnumerable<string>> { PuzzleInput.PuzzleOne.Split() };
    
    [SetUp]
    public void Setup()
    {
        
    }

    [TestCase("1", 11)]
    [TestCase("2", 22)]
    [TestCase("23", 23)]
    [TestCase("a23a", 23)]
    [TestCase("a1b2c3d4e5f", 15)]
    [TestCase("two1nine", 29)]
    [TestCase("eightwothree", 83)]
    [TestCase("abcone2threexyz", 13)]
    [TestCase("xtwone3four", 24)]
    [TestCase("4nineeightseven2", 42)]
    [TestCase("zoneight234", 14)]
    [TestCase("7pqrstsixteen", 76)]
    [TestCase("7cnnnp6lzcjxfsqbbfqgvnqhklcktrvrlmfszmqchfnine", 79)]
    public void Calibrate_A_Single_Value(string calibrationValue, int correctCalibration)
    {
        // Arrange
        

        // Act
        var calibration = CalibrationCalculator.CalibrateValue(calibrationValue);

        // Assert
        Assert.That(calibration, Is.EqualTo(correctCalibration));
    }

    [Test]
    public void Calibrate_A_Whole_Document()
    {
        // Arrange
        var calibrationDocument = new List<string> { "1abc2", "pqr3stu8vwx", "a1b2c3d4e5f", "treb7uchet" };
        var correctCalibration = 142;

        // Act
        var calibration = CalibrationCalculator.CalibrateDocument(calibrationDocument);

        // Assert
        Assert.That(calibration, Is.EqualTo(correctCalibration));
    }

    [TestCaseSource(nameof(Day1Calibration))]
    public void Calibrate_The_Day1_Puzzle_Input(IEnumerable<string> calibrationDocument)
    {
        // Arrange
        
        // Act
        var calibration = CalibrationCalculator.CalibrateDocument(calibrationDocument);

        // Assert
        Assert.That(calibration, Is.GreaterThan(0));
        TestContext.WriteLine($"Day1Part1: {calibration}");
    }
}

public static class CalibrationCalculator
{
    public static int CalibrateValue(string calibrationValue)
    {
        return int.Parse(FirstNumber(calibrationValue) + SecondNumber(calibrationValue));
    }

    public static int CalibrateDocument(IEnumerable<string> calibrationDocument)
    {
        return calibrationDocument.Sum(CalibrateValue);
    }

    private static string FirstNumber(string value)
    {
        var wordsAndNumbers = new List<(string, int)>
        {
            FirstWordInValue(value),
            FirstNumberInValue(value)
        };

        var first = wordsAndNumbers
            .Where(t => t != default)
            .OrderBy(t => t.Item2)
            .FirstOrDefault();

        return first == default ? "0" : first.Item1;
    }

    private static (string, int) FirstNumberInValue(string value)
    {
        return value
            .Where(char.IsNumber)
            .Select(c => (c.ToString(), value.IndexOf(c)))
            .OrderBy(t => t.Item2)
            .FirstOrDefault();
    }

    private static (string n, int) FirstWordInValue(string value)
    {
        return Numbers
            .Where(n => value.ToLower().IndexOf(n, StringComparison.InvariantCulture) > -1)
            .Select(n => ($"{Numbers.IndexOf(n) + 1}", value.ToLower().IndexOf(n, StringComparison.InvariantCulture)))
            .OrderBy(t => t.Item2)
            .FirstOrDefault();
    }
    
    private static (string, int) SecondNumberInValue(string value)
    {
        return value
            .Where(char.IsNumber)
            .Select(c => (c.ToString(), value.LastIndexOf(c)))
            .OrderByDescending(t => t.Item2)
            .FirstOrDefault();
    }

    private static (string n, int) SecondWordInValue(string value)
    {
        return Numbers
            .Where(n => value.ToLower().LastIndexOf(n, StringComparison.InvariantCulture) > -1)
            .Select(n => ($"{Numbers.IndexOf(n) + 1}", value.ToLower().LastIndexOf(n, StringComparison.InvariantCulture)))
            .OrderByDescending(t => t.Item2)
            .FirstOrDefault();
    }

    private static string SecondNumber(string value)
    {
        var wordsAndNumbers = new List<(string, int)>
        {
            SecondWordInValue(value),
            SecondNumberInValue(value)
        };

        var second = wordsAndNumbers
            .Where(t => t != default)
            .OrderByDescending(t => t.Item2)
            .FirstOrDefault();

        return second == default ? "0" : second.Item1;
    }

    private static readonly List<string> Numbers = new() { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
}