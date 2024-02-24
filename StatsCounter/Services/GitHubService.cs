using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StatsCounter.Models;

namespace StatsCounter.Services;

public interface IGitHubService
{
    Task<IEnumerable<RepositoryInfo>> GetRepositoryInfosByOwnerAsync(string owner);
}
    
public class GitHubService : IGitHubService
{
    private readonly HttpClient _httpClient;

    public GitHubService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<RepositoryInfo>> GetRepositoryInfosByOwnerAsync(string owner)
    {
        var response = await _httpClient.GetAsync($"/users/{owner}/repos");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var repositories = JsonConvert.DeserializeObject<IEnumerable<RepositoryInfo>>(content);
        return repositories;
    }
}