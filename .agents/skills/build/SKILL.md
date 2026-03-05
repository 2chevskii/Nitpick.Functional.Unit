---
name: build
description: Validate that the .NET solution compiles by running dotnet build. Run this after finishing any task to confirm nothing is broken before moving on.
argument-hint: [project-or-solution]
allowed-tools: Bash(dotnet build *)
---

Validate that the solution compiles.

## Steps

1. Run from the repository root:
   ```bash
   dotnet build $ARGUMENTS
   ```

   If `$ARGUMENTS` is empty, the CLI will locate the solution file automatically.

2. **If the build fails**: report the error messages clearly. Do not proceed with other tasks or mark any work complete until the build is green.

3. **If the build succeeds**: confirm with the summary line (errors: 0, warnings: N).

## Arguments

`$ARGUMENTS` — optional path to a specific project or solution file. Defaults to the solution in the current directory.
