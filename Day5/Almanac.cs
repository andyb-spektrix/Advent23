namespace Day5;

public class Almanac
{
    public static long GetSeedLocations(string data)
    {
        var groups = data.Split(Environment.NewLine + Environment.NewLine);

        var seeds = groups
            .Single(x => x.Contains("seeds"))
            .Split(':')
            .Single(x => !x.Contains("seeds"))
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(s => Convert.ToInt64(s));

        var maps = groups
            .Where(x => !x.Contains("seeds"))
            .Select(s => s.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            .Select(a => (Name: a[0].Split(' ').First(), Lines: a.Skip(1)))
            .Select(x => 
                (x.Name, Maps: x.Lines.Select(l => 
                    l.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(i => Convert.ToInt64(i))).ToList()))
            .Select(x => 
                (x.Name, Maps: x.Maps
                    .Select(m => 
                        (Destination: m.Take(1).Single(), Source: m.Skip(1).Take(1).Single(), Count: m.Skip(2).Single() )) ));

        var locations = seeds
            .Select(s => maps.Aggregate(s,
                (acc, val) => val.Maps
                    .Where(m => acc >= m.Source && acc < m.Source + m.Count)
                    .Select(m => (long?)m.Destination + (acc - m.Source))
                    .FirstOrDefault() ?? acc))
            .OrderBy(x => x);

        return locations.First();
    }
}