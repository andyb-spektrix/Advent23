namespace Day2;

public class GameClassifierShould
{
    private List<Cube> _cubes;
    private GameClassifier _classifier;
    
    [SetUp]
    public void Setup()
    {
        _cubes = new List<Cube> { new Cube("red", 12), new Cube("green", 13), new Cube("blue", 14)};
        _classifier = new GameClassifier(_cubes);
    }

    [Test]
    public void Classify_A_Game_As_Possible()
    {
        var gameInput = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green";

        var classification = _classifier.Classify(gameInput);
        
        Assert.That(classification.Single(), Is.EqualTo(new Classification(1, true)));
    }
    
    [Test]
    public void Classify_A_Game_As_Not_Possible()
    {
        var gameInput = "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red";

        var classification = _classifier.Classify(gameInput);
        
        Assert.That(classification.Single(), Is.EqualTo(new Classification(3, false)));
    }
    
    [Test]
    public void Classify_Multiple_Games()
    {
        var gameInput = """
                        Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
                        Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
                        """;

        var classification = _classifier.Classify(gameInput).ToList();
        
        Assert.Multiple(() =>
        {
            Assert.That(classification.Single(c => c.Id == 1).Possible, Is.True);
            Assert.That(classification.Single(c => c.Id == 3).Possible, Is.False);
        });
    }

    [Test]
    public void Calculate_The_Minimum_Power_Of_A_Game()
    {
        var gameInput = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green";

        var power = _classifier.CalculatePower(gameInput);
        
        Assert.That(power, Is.EqualTo(48));
    }
    
    [Test]
    public void Calculate_The_Minimum_Power_Of_Multiple_Games()
    {
        var gameInput = """
                        Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
                        Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
                        Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
                        Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
                        Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
                        """;

        var power = _classifier.CalculatePower(gameInput);
        
        Assert.That(power, Is.EqualTo(2286));
    }
}

public record Classification(int Id, bool Possible);
public record Cube(string Colour, int Amount);

public record MaxCubes(int Red, int Blue, int Green);