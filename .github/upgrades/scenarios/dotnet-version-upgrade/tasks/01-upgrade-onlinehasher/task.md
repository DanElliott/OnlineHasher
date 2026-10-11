# 01-upgrade-onlinehasher: Upgrade OnlineHasher to net10.0

Verify the .NET 10 SDK and global.json compatibility, then update `OnlineHasher.csproj` TargetFramework to net10.0 and bump packages per assessment: Microsoft.AspNetCore.Components.WebAssembly, .DevServer, Microsoft.AspNetCore.Cryptography.KeyDerivation and System.Net.Http.Json to 10.0.12; remove Microsoft.AspNetCore.Components.WebAssembly.Build (functionality is in the framework/SDK). Restore, then build and fix compilation errors in a single pass, including any Blazor WebAssembly project-model changes (e.g. SDK/Program startup) needed for the new version.

## Research findings
- Single project OnlineHasher/OnlineHasher.csproj (Sdk.Web, netstandard2.1, Blazor WASM 3.2.1 style); no global.json; SDK 10.0.301 installed.
- Files: Program.cs (RootComponents.Add<App>("app")), App.razor (LayoutView), wwwroot/index.html (<app>), Pages/Index.razor.
- Plan: switch to Microsoft.NET.Sdk.BlazorWebAssembly, net10.0, drop RazorLangVersion, drop .Build package and DevServer-compatible bump; replace System.Net.Http.Json (in framework) ; fix compile errors (selector "app" -> "#app").
- Decomposition: atomic (single project, Execution stage/hints not needed beyond this).

**Done when**: Project targets net10.0, packages updated/removed as listed, restore succeeds, and the solution builds with 0 errors.
