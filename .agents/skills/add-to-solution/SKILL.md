---
name: add-to-solution
description: Add an existing .NET project to a solution file using dotnet sln add. Use when the user wants to include a project in a solution.
argument-hint: <project-path> [--solution <solution-file>]
allowed-tools: Bash(dotnet sln *)
---

Add an existing project to a solution file using `dotnet sln add`. Do NOT edit `.sln` or `.slnx` files by hand.

## Steps

1. Locate the solution file if not specified. If there is exactly one solution file in the working directory, use it. If there are multiple, ask the user which one to use.

2. Run:
   ```bash
   dotnet sln <solution-file> add <path/to/project.csproj>
   ```

3. Confirm the project was added successfully and report the solution file and project path.

## Arguments

`$ARGUMENTS` — path to the project file to add, and optionally the solution file to add it to.
If no project path is provided, ask the user which project to add before proceeding.
