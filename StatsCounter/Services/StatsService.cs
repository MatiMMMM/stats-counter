using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StatsCounter.Models;

namespace StatsCounter.Services
{
    public interface IStatsService
    {
        Task<RepositoryStats> CalculateStatsFromRepositoriesAsync(List<RepositoryInfo> repositories);
        Task<RepositoryStats> GetRepositoryStatsByOwnerAsync(string owner);
    }

    public class StatsService : IStatsService
    {
        public async Task<RepositoryStats> CalculateStatsFromRepositoriesAsync(List<RepositoryInfo> repositories)
        {
            if (repositories == null || repositories.Count == 0)
            {
                throw new ArgumentException("Repository list cannot be null or empty.");
            }

            var repositoryStats = new RepositoryStats();
            repositoryStats.Owner = repositories[0].Owner; // Assuming all repositories have the same owner

            // Calculate statistics
            long totalSize = 0;
            long totalWatchers = 0;
            long totalForks = 0;

            var languages = new Dictionary<string, int>();

            foreach (var repo in repositories)
            {
                totalSize += repo.Size;
                totalWatchers += repo.WatchersCount;
                totalForks += repo.ForksCount;

                // Count languages
                foreach (var language in repo.Languages)
                {
                    if (languages.ContainsKey(language))
                    {
                        languages[language]++;
                    }
                    else
                    {
                        languages[language] = 1;
                    }
                }
            }

            repositoryStats.AvgSize = totalSize / (double)repositories.Count;
            repositoryStats.AvgWatchers = totalWatchers / (double)repositories.Count;
            repositoryStats.AvgForks = totalForks / (double)repositories.Count;
            repositoryStats.Languages = languages;

            // Simulate an asynchronous operation by returning a completed task
            await Task.CompletedTask;

            return repositoryStats;
        }

        public Task<RepositoryStats> GetRepositoryStatsByOwnerAsync(string owner)
        {
            throw new NotImplementedException();
        }
    }
}