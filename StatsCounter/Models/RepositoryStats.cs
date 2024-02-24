using System.Collections.Generic;

namespace StatsCounter.Models;

public class RepositoryStats
{
    public string Owner { get; set; }
    public double AvgWatchers { get; set; }
    public double AvgForks { get; set; }
    public double AvgSize { get; set; }
    public IDictionary<string, int> Languages { get; set; }
}