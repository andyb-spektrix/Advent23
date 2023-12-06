namespace Day2;

public class GameClassifier(List<Cube> cubes)
{
    private static long SumOfPowers(IEnumerable<long> powers) => powers.Sum();

    private static IEnumerable<long> ByCalculatingPowers(IEnumerable<MaxCubes> maxCubes) =>
        maxCubes.Select(c => (long)c.Blue * c.Green * c.Red);

    private static IEnumerable<MaxCubes> OfMaxCubesInGames(string[] games)
    {
        return games.Select(g => new MaxCubes(MaximumOf(ParsedGame(g), "red"), MaximumOf(ParsedGame(g), "blue"),
            MaximumOf(ParsedGame(g), "green")));
    }

    private static int MaximumOf(IReadOnlyList<string> game, string colour) =>
        game.Select((p, i) => p == colour ? int.Parse(game[i - 1]) : 0).Max();

    private static Classification ClassifyGame(string gameInput, IEnumerable<Cube> cubes)
    {
        var gameId = AsNumber(RightSideOfSpace(LeftOfColon(gameInput)));
        var isTooBig = NumberIsTooBig(
            ParsedGame(gameInput),
            cubes
        );


        return new Classification(gameId, !isTooBig);
    }

    private static string[] ParsedGame(string gameInput)
    {
        return AsLower(
            SplitAndCollect(
                SplitAndCollect(
                    SplitAndCollect(
                        SplitOnSpace(gameInput), SplitOnComma), SplitOnColon), SplitOnSemiColon));
    }

    private static string LeftOfColon(string input) => SplitOnColon(input)[0];
    private static string[] SplitOnSpace(string input) => input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    private static string[] SplitOnColon(string input) => input.Split(':', StringSplitOptions.RemoveEmptyEntries);
    private static string[] SplitOnSemiColon(string input) => input.Split(';', StringSplitOptions.RemoveEmptyEntries);
    private static string[] SplitOnComma(string input) => input.Split(',', StringSplitOptions.RemoveEmptyEntries);

    private static string[] SplitOnNewLine(string input) =>
        input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

    private static IEnumerable<Classification> ClassifyGames(IEnumerable<string> games, IEnumerable<Cube> cubes) =>
        games.Select(g => ClassifyGame(g, cubes));

    private static string[] AsLower(IEnumerable<string> input) => input.Select(s => s.ToLowerInvariant()).ToArray();

    private static bool NumberIsTooBig(string[] input, IEnumerable<Cube> cubes) => input
        .Select((s, i) => NumberOfCubesIsGreaterThanMax(ValueBefore(input, i, IsColour(s, cubes)), s, cubes))
        .Any(b => b);

    private static bool IsColour(string input, IEnumerable<Cube> cubes) => cubes.Any(c => c.Colour == input);

    private static int ValueBefore(string[] input, int index, bool getValue) =>
        getValue ? AsNumber(input[index - 1]) : -1;

    private static int NumberOfColour(string input, IEnumerable<Cube> cubes) =>
        cubes.Single(c => c.Colour == input).Amount;

    private static IEnumerable<string> SplitAndCollect(IEnumerable<string> input, Func<string, string[]> splitter) =>
        input.SelectMany(splitter);

    private static bool NumberOfCubesIsGreaterThanMax(int number, string colour, IEnumerable<Cube> cubes) =>
        number > -1 && number > NumberOfColour(colour, cubes);

    private static string RightSideOfSpace(string input) => SplitOnSpace(input)[1];

    private static int AsNumber(string input) => int.Parse(input);

    public long CalculatePower(string gameInput)
    {
        return SumOfPowers(ByCalculatingPowers(OfMaxCubesInGames(SplitOnNewLine(gameInput))));
    }


    public IEnumerable<Classification> Classify(string gameInput)
    {
        return ClassifyGames(SplitOnNewLine(gameInput), cubes);
    }
}