using System.Collections.Generic;

namespace StatsCounter.Models
{
    public class RepositoryStats
    {
        public string Owner { get; set; }
        public List<string> Languages { get; set; }
        public long Size { get; set; }
        public int PublicRepositories { get; set; }
        public double AvgWatchers { get; set; }
        public double AvgForks { get; set; }
    }
}