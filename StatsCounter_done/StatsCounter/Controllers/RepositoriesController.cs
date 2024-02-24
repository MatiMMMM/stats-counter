using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StatsCounter.Models;
using StatsCounter.Services;

namespace StatsCounter.Controllers;

[Route("repositories")]
[ApiController]
public class RepositoriesController : ControllerBase
{
    //private readonly IStatsService _statsService;

    private static HttpClient sharedClient = new()
    {
        BaseAddress = new Uri("https://api.github.com/"),
        
    };
  
    public RepositoriesController()
    {
        //_statsService = statsService;
    }

    [HttpGet("{owner}")]
    [ProducesResponseType(typeof(RepositoryStats), StatusCodes.Status200OK)]
    public async Task<ActionResult<RepositoryStats>> Get(
        [FromRoute] string owner)
    {
        sharedClient.DefaultRequestHeaders.Add("User-Agent", "C# App");

        var uri = "users/" + owner + "/repos";

        var result = await sharedClient.GetAsync(uri);

        var contentStream = await result.Content.ReadAsStringAsync();


        //var result = await _statsService.GetRepositoryStatsByOwnerAsync(owner).ConfigureAwait(false);
        return Ok(contentStream);
    }
}