# .agents — Codex Standard Directory

This folder is the standardized home for Codex agent configuration in this repository.

## Contents

- `PROJECT.md` — project context and engineering constraints for agents
- `skills/` — task skills migrated from `.claude/skills`
- `settings.local.json` — local permissions and tool policy preferences

## Skills

The following skills are available under `.agents/skills`:

- `add-package`
- `add-to-solution`
- `build`
- `commit`
- `create-project`
- `format`
- `test`

Each skill keeps its own `SKILL.md` contract and can be invoked by name.

## Migration Notes

- The `.agents/skills` contents were copied from `.claude/skills`.
- Existing Claude-era files are retained for compatibility, but `.agents` is the preferred location going forward.
