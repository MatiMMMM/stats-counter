using Newtonsoft.Json;
using System.Collections.Generic;

namespace StatsCounter.Models;

public class RepositoryInfo
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("watchers_count")]
    public long WatchersCount { get; set; }

    [JsonProperty("forks_count")]
    public long ForksCount { get; set; }

    [JsonProperty("size")]
    public long Size { get; set; }

    [JsonProperty("languages")]
    public List<string> Languages { get; set; }
    public string Language { get; internal set; }

    [JsonProperty("owner")]
    public string Owner { get; set; }
}