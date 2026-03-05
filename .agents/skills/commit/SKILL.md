---
name: commit
description: Stage and commit changes using git. Writes commit messages conforming to the Conventional Commits specification and splits unrelated changes into separate commits. Use when the user asks to commit work.
argument-hint: [description or hint]
allowed-tools: Bash(git *)
---

Commit changes using git following the Conventional Commits specification.

## Conventional Commits format

```
<type>[optional scope]: <description>

[optional body]

[optional footer(s)]
```

**Type reference**

| Type | Use for |
|---|---|
| `feat` | New feature |
| `fix` | Bug fix |
| `docs` | Documentation only |
| `refactor` | Code change that is neither a fix nor a feature |
| `test` | Adding or updating tests |
| `chore` | Maintenance (dependencies, config, tooling) |
| `build` | Build system or project file changes |
| `ci` | CI/CD pipeline changes |
| `perf` | Performance improvement |
| `style` | Formatting, whitespace — no logic change |

## Steps

1. Inspect current state:
   ```bash
   git status
   git diff
   ```

2. Group changed files by concern. Unrelated or loosely related changes **must** go into separate commits.

3. For each logical group:
   a. Stage only the relevant files — do NOT use `git add -A` or `git add .` blindly.
   b. Write a Conventional Commits message.
   c. Commit:
      ```bash
      git commit -m "type(scope): description"
      ```

4. Never use `--no-verify`, `--no-gpg-sign`, or any flag that bypasses hooks.

5. Confirm with:
   ```bash
   git log --oneline -5
   ```

## Arguments

`$ARGUMENTS` — optional hint describing what is being committed, used to guide grouping and message writing.
