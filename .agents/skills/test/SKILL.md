---
name: test
description: Run the test suite using dotnet test. Run this after implementing a new feature or making any logic changes to confirm all tests pass.
argument-hint: [project or --filter expression]
allowed-tools: Bash(dotnet test *)
---

Run the test suite and report results.

## Steps

1. Run:
   ```bash
   dotnet test $ARGUMENTS
   ```

   If no arguments are provided, run from the repository root to discover all test projects automatically.

2. **If any tests fail**, report:
   - Which tests failed and why (failure message + relevant stack trace)
   - Do NOT mark the feature complete or move on until all tests pass

3. **If all tests pass**, confirm with the summary line (passed: N, failed: 0).

## Arguments

`$ARGUMENTS` — optional path to a specific test project, a `--filter` expression, or any other `dotnet test` flags.
