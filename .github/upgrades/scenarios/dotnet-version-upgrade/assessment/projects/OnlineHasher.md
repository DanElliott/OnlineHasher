# OnlineHasher\OnlineHasher.csproj

[← Back to the assessment index](../../assessment.md)

## Project Info

- **Current Target Framework:** netstandard2.1✅
- **SDK-style**: True
- **Project Kind:** AspNetCore
- **Dependencies**: 0
- **Dependants**: 0
- **Number of Files**: 14
- **Number of Files with Incidents**: 1
- **Lines of Code**: 23
- **Estimated LOC to modify**: 0+ (at least 0.0% of the project)

## Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["OnlineHasher.csproj"]
        MAIN["<b>📦&nbsp;OnlineHasher.csproj</b><br/><small>netstandard2.1</small>"]
        click MAIN "../projects/OnlineHasher.md"
    end

```

## API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 2 |  |
| ***Total APIs Analyzed*** | ***2*** |  |

## NuGet Package Issues

| Package | Current Version | Suggested Version | Severity | Issue |
| :--- | :---: | :---: | :---: | :--- |
| Microsoft.AspNetCore.Components.WebAssembly | 3.2.1 | 10.0.12 | 🟡 Potential | NuGet package upgrade is recommended |
| Microsoft.AspNetCore.Components.WebAssembly.Build | 3.2.1 | — | 🔴 Mandatory | NuGet package functionality is included with framework reference |
| Microsoft.AspNetCore.Components.WebAssembly.DevServer | 3.2.1 | 10.0.12 | 🟡 Potential | NuGet package upgrade is recommended |
| Microsoft.AspNetCore.Cryptography.KeyDerivation | 3.1.6 | 10.0.12 | 🟡 Potential | NuGet package upgrade is recommended |
| System.Net.Http.Json | 3.2.0 | 10.0.12 | 🟡 Potential | NuGet package upgrade is recommended |
| System.Net.Http.Json | 3.2.0 | — | 🔵 Optional | NuGet package is deprecated |

Every project affected by these packages, and the versions the repository settles on: [aggregate NuGet packages](../nuget/aggregate-packages.md).

