namespace Day2;

public class Part1Solution(GameClassifier classifier)
{
    public int TotalIdsOfPossibleGames(string input) => SumOfGameIds(PossibleGames(classifier.Classify(input)));

    private static IEnumerable<Classification> PossibleGames(IEnumerable<Classification> games) =>
        games.Where(g => g.Possible);

    private static int SumOfGameIds(IEnumerable<Classification> games) => games.Sum(g => g.Id);
}