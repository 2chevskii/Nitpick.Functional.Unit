# PROJECT.md — Agent Guidance

This file provides orientation for AI agents working on the Unit library.

## Project in One Line

A `netstandard2.0` C# library that exposes the `Unit` type, inspired by Rust's `()`, as a NuGet package.

## Authoritative Source of Truth

`SPEC.md` is the canonical specification. When in doubt about intended behavior, consult `SPEC.md` first. If `SPEC.md` and code disagree, treat `SPEC.md` as correct and update the code.

## Repository Layout

```
unit/
|- src/Unit/               # Library source
|- tests/Unit.Tests/       # xUnit v3 tests
|- README.md               # Consumer-facing docs
|- SPEC.md                 # Detailed specification (canonical)
|- AGENTS.md               # Agent entrypoint for Codex
`- .agents/                # Standardized agent config and skills
```

## Key Design Decisions

| Decision | Choice | Reason |
|---|---|---|
| Type kind | `readonly struct` | Zero-allocation; closest to Rust's zero-sized `()` |
| Canonical instance | `Unit.Value` | Explicit, readable; avoids confusion with `default` |
| `ToString()` | `"()"` | Matches Rust's Display format |
| JSON representation | `null` | Smallest possible JSON token for a valueless type |
| Action adapters | `ToFunc()` (all arities) | Consistent naming across all overloads |
| Task helpers | `AsUnit()` | Reads naturally: "treat this Task as Unit-returning" |

## Code Conventions

- All public types live in the root `Unit` namespace. Json types live in `Unit.Json`.
- No comments on self-evident code. Comments only where non-obvious behavior exists.
- No docstrings unless explicitly requested.
- Keep implementations minimal; this library intentionally has no logic beyond type plumbing.
- `UnitExtensions.cs` holds all extension methods (Action adapters + Task helpers).
- Comparer types each implement only the interface they advertise; `UnitComparer` implements both `IComparer<Unit>` and `IEqualityComparer<Unit>`.

## Formatting Policy

- Agents MUST use the `format` skill after introducing any changes to files with these extensions: `.cs`, `.csproj`, `.props`, `.targets`, `.sln`, `.slnx`.
- This rule is mandatory and applies to every turn where any of the listed file types are edited.
- Preferred command via the `format` skill: `dotnet csharpier format <changed-paths>` (or `dotnet csharpier format .` when needed).

## Testing

- Framework: xUnit v3 with the MTP v2 runner.
- One test file per feature area (see structure in `SPEC.md`).
- Every public member must have at least one test.
- Tests assert behavior directly, not implementation details.
- Do not add test helpers/base classes unless shared logic appears in three or more test files.

Run tests:

```bash
dotnet test
```

## What Not to Do

- Do not add types beyond what `SPEC.md` defines (`Option<T>`, `Result<T>`, etc. are out of scope).
- Do not add dependencies beyond `System.Text.Json` and `Newtonsoft.Json`.
- Do not target frameworks other than `netstandard2.0` without explicit user approval.
- Do not introduce source generators, analyzers, or Roslyn tooling.
- Do not auto-commit. Let the user review and commit changes.
