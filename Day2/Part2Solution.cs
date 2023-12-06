namespace Day2;

public class Part2Solution(GameClassifier classifier)
{
    public long TotalPowersOfAllGames(string input) => classifier.CalculatePower(input);
}