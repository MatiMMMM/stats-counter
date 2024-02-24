using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatsCounter.Data;
using StatsCounter.Models;

namespace StatsCounter.Services
{
    public class StatsService : IStatsService
    {
        private readonly IGitHubService _gitHubService;
        private readonly StatsCounterDbContext _dbContext;

        public StatsService(IGitHubService @object)
        {
        }

        public StatsService(IGitHubService gitHubService, StatsCounterDbContext dbContext)
        {
            _gitHubService = gitHubService;
            _dbContext = dbContext;
        }

        public async Task<RepositoryStats> GetRepositoryStatsByOwnerAsync(string owner)
        {
            var repositories = await _gitHubService.GetRepositoryInfosByOwnerAsync(owner);

            var repositoryStats = new RepositoryStats();
            repositoryStats.Languages = new List<string>();
            long count = repositories.Count();
            long watchers = 0;
            long forks = 0;
            long size = 0;
            Dictionary<string, int> languageCounts = new Dictionary<string, int>();

            foreach (var repository in repositories)
            {
                watchers += repository.WatchersCount;
                forks += repository.ForksCount;
                size += repository.Size;

                // Count languages used
                foreach (var language in repository.Languages)
                {
                    if (languageCounts.ContainsKey(language))
                        languageCounts[language]++;
                    else
                        languageCounts[language] = 1;
                }
            }

            repositoryStats.Owner = owner;
            repositoryStats.PublicRepositories = repositories.Count();
            repositoryStats.AvgForks = count > 0 ? forks / (double)count : 0.0;
            repositoryStats.AvgWatchers = count > 0 ? watchers / (double)count : 0.0;
            repositoryStats.Size = size;

            if (count > 0)
            {
                repositoryStats.Languages = languageCounts.Keys.ToList();
            }

            // Save to database
            _dbContext.RepositoryStatsHistories.Add(new RepositoryStatsHistory
            {
                Owner = owner,
                Languages = string.Join(", ", repositoryStats.Languages),
                Size = repositoryStats.Size,
                PublicRepositories = repositoryStats.PublicRepositories,
                AvgWatchers = repositoryStats.AvgWatchers,
                AvgForks = repositoryStats.AvgForks,
                Timestamp = DateTime.UtcNow
            });

            await _dbContext.SaveChangesAsync(); // Save changes to the database

            return repositoryStats;
        }
    }

    public interface IStatsService
    {
        Task<RepositoryStats> GetRepositoryStatsByOwnerAsync(string owner);
    }
}