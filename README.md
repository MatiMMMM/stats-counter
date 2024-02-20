# Real life Interview question to implement (DevSkiller) - StatsCounter task

In this task you will finish a simple REST service which allows to return statistics of GitHub user repositories.

 
The statistics include:
 - the distribution of programming languages used across the repositories of a given user,
 - the distribution of repository sizes for a given user (sum),
 - count the number of public repositories owned by the user,
 - average count of watchers,
 - average count of forks,
 
The statistics are returned from an endpoint available under GET /repositories/{owner} in the following fashion:

```
{
    "owner": "...",
    "languages": ["JAVA", "PHP", "TYPESCRIPT"],
    "size": 45689,
    "publicRepositories" 10,
    "avgWatchers": 0.0,
    "avgForks": 0.0,
}
```

## TASK

Your task is to provide implementations of 3 methods:
 - StartupExtensions.AddGitHubService located in Extensions directory,
 - GitHubService.GetRepositoryInfosByOwnerAsync located in Services directory,
 - StatsService.GetRepositoryStatsByOwnerAsync located in Services directory.
 
The methods should contain the following logic:
 - StartupExtensions.AddGitHubService - DI registration of GitHubService as IGitHubService and its dependencies using provided parameters,
 - GitHubService.GetRepositoryInfosByOwnerAsync - retrieving information about repositories for owner directly from GitHub (you don't have to worry about pagination),
 - StatsService.GetRepositoryStatsByOwnerAsync - processing of data provided by GitHubService in order to count required statistics.

## GITHUB API

GitHub API reference can be found at: https://developer.github.com/v3/

The GitHub API provides a comprehensive set of endpoints for accessing various aspects of a user's repositories, organizations, activities, and more. One such essential endpoint is the https://api.github.com/users/{owner}/repos endpoint, which allows developers to retrieve a list of repositories belonging to a specific GitHub user or organization.

There are some tests provided for you in the following projects:
 - StatsCounter.Test.Integration,
 - StatsCounter.Test.Unit.

Update them, and make sure, that all test works well.
 
They are supposed to make development easier for you, but if you find them obstructing, feel free to modify or remove them (though you shouldn't). Feel free to add more tests, if that makes development easier for you. Modifications in these test projects will not affect your final score.
Warning - Please do not modify any existing code in StatsCounter project apart from methods specified above. Modifications to class or member names may cause verification tests to fail and lower your score.
