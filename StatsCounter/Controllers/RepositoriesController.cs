using System.Net.Http;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StatsCounter.Models;
using StatsCounter.Services;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace StatsCounter.Controllers;

[Route("repositories")]
[ApiController]
public class RepositoriesController : ControllerBase
{
    private static HttpClient sharedClient = new()
    {
        BaseAddress = new Uri("https://api.github.com/")

    };

    private readonly IStatsService _statsService;

    public RepositoriesController(IStatsService statsService)
    {
        _statsService = statsService;
    }

    public IStatsService Get_statsService()
    {
        return _statsService;
    }

    [HttpGet("{owner}")]
    [ProducesResponseType(typeof(RepositoryStats), StatusCodes.Status200OK)]
    public async Task<ActionResult<RepositoryStats>> Get([FromRoute] string owner, IStatsService _statsService)
    {
        try
        {
            sharedClient.DefaultRequestHeaders.Add("User-Agent", "C# App");

            var uri = "users/" + owner + "/repos";

            var result = await sharedClient.GetAsync(uri);

            if (!result.IsSuccessStatusCode)
            {
                // If the request was not successful, return appropriate error response
                return StatusCode((int)result.StatusCode, $"Error: {result.ReasonPhrase}");
            }

            var repositoriesJson = await result.Content.ReadAsStringAsync();
            var repositories = JsonConvert.DeserializeObject<List<RepositoryInfo>>(repositoriesJson);

            // Now you have the repositories, you can calculate the statistics
            var repositoryStats = await _statsService.CalculateStatsFromRepositoriesAsync(repositories);

            return Ok(repositoryStats);
        }
        catch (Exception ex)
        {
            // Log the exception or handle it accordingly
            return StatusCode(500, $"An error occurred while fetching repository stats: {ex.Message}");
        }
    }
}