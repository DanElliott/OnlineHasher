# Progress 01-upgrade-onlinehasher
- OnlineHasher.csproj: SDK -> Microsoft.NET.Sdk.BlazorWebAssembly, net10.0, removed RazorLangVersion, removed WebAssembly.Build; WebAssembly/DevServer/KeyDerivation -> 10.0.12; System.Net.Http.Json removed (in framework; NU1510 warning otherwise).
- Program.cs: root component selector "app" -> "#app"; wwwroot/index.html: `<app>` -> `<div id="app">`.
- Build: 0 errors, 0 warnings. No tests in solution.
- Not verified at runtime: app startup in browser; App.razor still uses LayoutView (obsolete-free, builds clean).
