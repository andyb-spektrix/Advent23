namespace Day2;

[TestFixture]
public class Part2SolutionShould
{
    private Part2Solution _part2Solution;

    [SetUp]
    public void Setup()
    {
        _part2Solution = new Part2Solution(new GameClassifier(new List<Cube>()));
    }
    
    [Test]
    public void Give_The_Part2_Solution()
    {
        // Arrange
        

        // Act
        var sum = _part2Solution.TotalPowersOfAllGames(Part2PuzzleInput.Input);

        // Assert
        Assert.That(sum, Is.EqualTo(0));
    }
}