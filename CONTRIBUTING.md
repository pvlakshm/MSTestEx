# Contributing to MSTestEx
Welcome, and thank you for your interest in contributing to MSTestEx!

Here are many ways to contribute to MSTestEx
## Submit issues
Have you identified a reproducible problem in MSTestExV? Have a feature request?[Submit issues](https://github.com/pvlakshm/MSTestEx/issues), and help verify changes as they are checked in. Before you create a new issue, please do a search in open issues to see if the issue or feature request has already been filed. Be sure to scan through the most popular feature requests.

If you find your issue already exists, make relevant comments and add your reaction. Use a reaction in place of a "+1" comment:

- 👍 - upvote
- 👎 - downvote

If you cannot find an existing issue that describes your bug or feature, create a new issue.

## Review source code changes
Review the [source code changes](https://github.com/pvlakshm/MSTestEx/pulls).

## Contribute features and fixes
Contribute features and fixes.
Please create one pull request per issue and link the issue in the pull request. Do not merge multiple requests in one unless they have the same root cause. Keep code changes as small as possible. Avoid pure formatting changes to code that has not been modified otherwise. Pull requests should contain tests whenever possible.

## Contribute to the documentation
Contribute to the [documentation](./docs).

# Build
## Dependencies
- MSTest Test Framework v1.2.0
- Dotnet Core 1.0

### MSTest Test Framework v1.2.0
The MSTestEx solution already contains a NuGet reference to the MSTest Test Framework v1.2.0.

### Dotnet Core 1.0
Install DotNet Core from here: [.NET Core 1.0](https://dotnet.microsoft.com/download/dotnet-core/1.0). For e.g. you may use the following versions: v1.0.12

## Building MSTestEx
Run the following commands to build MSTestEx
- change directory to the folder containing ```MSTestEx.sln```
- ```dotnet restore```
- ```dotnet build```

## Executing the tests
Run the following commands to execute the tests.
- change directory to the folder containing ```MSTest.TestFramework.ExtensionsTests.csproj```
- dotnet test
