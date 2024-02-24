using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StatsCounter.Extensions;
using StatsCounter.Services;
using StatsCounter.Data; // Import the namespace for DbContext

namespace StatsCounter;

public class Startup
{
    private readonly IConfiguration _configuration;
        
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo 
            {
                Title = "StatsCounter API",
                Version = "v1"
            });
        });

        // Register DbContext
        services.AddDbContext<StatsCounterDbContext>(options =>
            options.UseSqlite(_configuration.GetConnectionString("DefaultConnection")));

        // Register services
        services.AddGitHubService(new Uri(_configuration["GitHubSettings:BaseApiUrl"]))
            .AddTransient<IStatsService, StatsService>();

        //    services.AddGitHubService(new Uri(_configuration["GitHubSettings:BaseApiUrl"]))
        //       .AddTransient<IStatsService, StatsService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "StatsCounter API v1");
                c.RoutePrefix = string.Empty;
            });
        }
        else
        {
            app.UseHttpsRedirection();

        }

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}