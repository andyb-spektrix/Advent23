namespace Day1;

public class CalibrationCalculatorShould
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase("1", 11)]
    [TestCase("2", 22)]
    [TestCase("23", 23)]
    [TestCase("a23a", 23)]
    [TestCase("a1b2c3d4e5f", 15)]
    public void Calibrate_A_Single_Value(string calibrationValue, int correctCalibration)
    {
        // Arrange
        

        // Act
        var calibration = CalibrationCalculator.CalibrateLine(calibrationValue);

        // Assert
        Assert.That(calibration, Is.EqualTo(correctCalibration));
    }
}

public static class CalibrationCalculator
{
    public static int CalibrateLine(string calibrationValue)
    {
        var firstNumber = calibrationValue.First(char.IsNumber).ToString();
        var secondNumber = calibrationValue.Last(char.IsNumber).ToString();
        
        return int.Parse(firstNumber + secondNumber);
    }
}