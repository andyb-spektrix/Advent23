using NUnit.Framework.Constraints;

namespace Day5;

public class AlmanacShould
{
    private readonly string _testData = """
                                       seeds: 79 14 55 13
                                       
                                       seed-to-soil map:
                                       50 98 2
                                       52 50 48
                                       
                                       soil-to-fertilizer map:
                                       0 15 37
                                       37 52 2
                                       39 0 15
                                       
                                       fertilizer-to-water map:
                                       49 53 8
                                       0 11 42
                                       42 0 7
                                       57 7 4
                                       
                                       water-to-light map:
                                       88 18 7
                                       18 25 70
                                       
                                       light-to-temperature map:
                                       45 77 23
                                       81 45 19
                                       68 64 13
                                       
                                       temperature-to-humidity map:
                                       0 69 1
                                       1 0 69
                                       
                                       humidity-to-location map:
                                       60 56 37
                                       56 93 4
                                       """;
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Determine_Lowest_Location_For_Seeds()
    {
        var result = Almanac.GetSeedLocations(_testData);
        
        Assert.That(result, Is.EqualTo(35));
    }
    
    [Test]
    public void Solve_Part_One()
    {
        var result = Almanac.GetSeedLocations(Part1Input.Input);
        
        Assert.That(result, Is.EqualTo(35));
    }
}