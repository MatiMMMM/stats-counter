using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using StatsCounter.Models;
using StatsCounter.Services;
using Xunit;

namespace StatsCounter.Tests.Unit
{
    public class StatsServiceTests
    {
        private readonly Mock<IGitHubService> _gitHubService;
        private readonly IStatsService _statsService;

        public StatsServiceTests()
        {
            _gitHubService = new Mock<IGitHubService>();
            _statsService = new StatsService(_gitHubService.Object);
        }

        // Existing test cases...

        [Fact]
        public async Task ShouldInitializeRepositoryStatsCorrectly()
        {
            // given
            _gitHubService
                .Setup(s => s.GetRepositoryInfosByOwnerAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<RepositoryInfo>());

            // when
            var result = await _statsService.GetRepositoryStatsByOwnerAsync("owner");

            // then
            result.Should().NotBeNull();
            result.Languages.Should().NotBeNull().And.BeEmpty();
            result.PublicRepositories.Should().Be(0); // Assuming there are no repositories
            result.AvgForks.Should().Be(0);
            result.AvgWatchers.Should().Be(0);
            result.Size.Should().Be(0);
        }

        [Fact]
        public async Task ShouldCalculateCountsCorrectly()
        {
            // given
            var languages = new List<string> { "JAVA", "PHP", "TYPESCRIPT" };
            _gitHubService
                .Setup(s => s.GetRepositoryInfosByOwnerAsync(It.IsAny<string>()))
                .ReturnsAsync(
                    new List<RepositoryInfo>
                    {
                new RepositoryInfo(1, "name", 15, 22, 45689, languages)
                    });

            // when
            var result = await _statsService.GetRepositoryStatsByOwnerAsync("owner");

            // then
            result.PublicRepositories.Should().Be(1); // Assuming there is 1 repository
            result.AvgWatchers.Should().BeApproximately(0.0, 1e-6);
            result.AvgForks.Should().BeApproximately(0.0, 1e-6);
            result.Size.Should().Be(45689);
            result.Languages.Should().NotBeNull().And.NotBeEmpty();
            result.Languages.Should().Contain(languages); // Check if all specified languages are present
        }
    }
}