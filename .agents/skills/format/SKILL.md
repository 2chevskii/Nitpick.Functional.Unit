---
name: format
description: Format C# source files using CSharpier. Use after writing or modifying C# code to enforce consistent formatting.
argument-hint: [path]
allowed-tools: Bash(dotnet csharpier *)
---

Format C# source files using CSharpier.

## Steps

1. Run:
   ```bash
   dotnet csharpier format $ARGUMENTS
   ```

   If no path argument is provided, format the whole repository:
   ```bash
   dotnet csharpier format .
   ```

2. Report which files were reformatted (CSharpier prints changed files to stdout).

## Arguments

`$ARGUMENTS` — a file path, directory, or glob to format. Defaults to `.` (entire repo).
