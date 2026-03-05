---
name: add-package
description: Add a NuGet package to a .NET project using dotnet add package. Use when the user asks to install or reference a NuGet package in a project. Never edit .csproj files manually.
---

Add a NuGet package to a project using `dotnet add package`. Do NOT edit `.csproj` files by hand.

## Steps

1. Run:
   ```bash
   dotnet add <project> package <package-name> [--version <version>]
   ```

   If no project path is given, omit it and let the CLI resolve from the current directory.

2. Report the installed package name and the resolved version.

## Inputs

Use the package name plus any extra flags the user provided (for example, `--version` or `--project`).
If the request does not specify at least a package name, ask the user which package to add and to which project.
