using Newtonsoft.Json;
using System.Collections.Generic;

namespace StatsCounter.Models
{
    public class RepositoryInfo
    {
        private string languages;

        public List<string> Languages { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int WatchersCount { get; set; }
        public int ForksCount { get; set; }
        public int Size { get; set; }
        public int Watchers_count { get; }
        public int Forks_count { get; }


        public RepositoryInfo(int id, string name, int watchers_count, int forks_count, int size, string languages)
        {
            Id = id;
            Name = name;
            Watchers_count = watchers_count;
            Forks_count = forks_count;
            Size = size;
            this.languages = languages;
        }
    }
}