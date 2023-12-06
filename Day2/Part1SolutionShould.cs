namespace Day2;

[TestFixture]
public class Part1SolutionShould
{
    private Part1Solution _part1Solution;

    [SetUp]
    public void Setup()
    {
        _part1Solution = new Part1Solution(new GameClassifier(new List<Cube>
            { new Cube("red", 12), new Cube("green", 13), new Cube("blue", 14) }));
    }
    
    [Test]
    public void Sum_Possible_Game_Ids()
    {
        // Arrange
        var input = """
                    Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
                    Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
                    Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
                    Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
                    Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
                    """;

        // Act
        var sum = _part1Solution.TotalIdsOfPossibleGames(input);

        // Assert
        Assert.That(sum, Is.EqualTo(8));
    }

    [Test]
    public void Give_The_Part1_Solution()
    {
        // Arrange
        

        // Act
        var sum = _part1Solution.TotalIdsOfPossibleGames(Part1PuzzleInput.Input);

        // Assert
        Assert.That(sum, Is.EqualTo(0));
    }
}