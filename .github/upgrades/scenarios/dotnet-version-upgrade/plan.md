# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade OnlineHasher (Blazor WebAssembly, SDK-style, netstandard2.1) to net10.0
**Scope**: 1 project, ~23 LOC, low difficulty; 4 packages need upgrade, 0 API issues

### Selected Strategy
**All-At-Once** — All projects upgraded simultaneously in a single operation.
**Rationale**: 1 project, already SDK-style, clear structure.

## Upgrade Options

| Option | Selected | Why |
|--------|----------|-----|
| Upgrade Strategy | All-at-Once | Single small project; no tiering needed |

## Tasks

### 01-upgrade-onlinehasher: Upgrade OnlineHasher to net10.0

Verify the .NET 10 SDK and global.json compatibility, then update `OnlineHasher.csproj` TargetFramework to net10.0 and bump packages per assessment: Microsoft.AspNetCore.Components.WebAssembly, .DevServer, Microsoft.AspNetCore.Cryptography.KeyDerivation and System.Net.Http.Json to 10.0.12; remove Microsoft.AspNetCore.Components.WebAssembly.Build (functionality is in the framework/SDK). Restore, then build and fix compilation errors in a single pass, including any Blazor WebAssembly project-model changes (e.g. SDK/Program startup) needed for the new version.

**Done when**: Project targets net10.0, packages updated/removed as listed, restore succeeds, and the solution builds with 0 errors.

---

### 02-final-validation: Final validation

Build the full solution, run any available tests, and note deferred recommendations.

**Done when**: Solution builds cleanly, tests (if any) pass, and deferred items are documented.
