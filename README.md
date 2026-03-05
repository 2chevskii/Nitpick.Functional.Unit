# Unit

A minimal C# library providing the `Unit` type — the C# equivalent of Rust's `()`.

`Unit` is a `readonly struct` with exactly one possible value. It exists to fill the semantic hole left by `void`: unlike `void`, `Unit` is a real type that can be used as a generic type argument, stored in variables, and returned from functions.

---

## Installation

```
dotnet add package <package-id>
```

> Package ID TBD.

---

## Agent Setup

This repository uses a standardized Codex agent layout:

- `AGENTS.md` as the root entrypoint
- `.agents/README.md` for agent configuration overview
- `.agents/PROJECT.md` for project-specific agent guidance
- `.agents/skills/*/SKILL.md` for reusable agent skills

---

## At a Glance

### Core type

```csharp
using Unit;

// The one and only value
Unit value = Unit.Value;

// Prints "()"
Console.WriteLine(Unit.Value);

// All Unit values are equal
Assert.Equal(Unit.Value, Unit.Value);

// Implicit conversion from ValueTuple — mirrors Rust's ()
Unit fromTuple = default(ValueTuple);
```

### Void → Func adapters

Turn any `Action` into a `Func<…, Unit>` so it fits into generic pipelines that expect a return value.

```csharp
Action<string> log = msg => Console.WriteLine(msg);
Func<string, Unit> logFunc = log.ToFunc();

// Now usable wherever Func<string, Unit> is required
Unit result = logFunc("hello");
```

### Task helpers

Normalize async APIs that return `Task` (non-generic) into `Task<Unit>`.

```csharp
Task SaveAsync() => File.WriteAllTextAsync("out.txt", "data");

Task<Unit> normalized = SaveAsync().AsUnit();

// Discard the result of Task<T>
Task<int> computeAsync = Task.FromResult(42);
Task<Unit> discarded = computeAsync.AsUnit();
```

### JSON serialization

#### System.Text.Json

`Unit` serializes to and from JSON `null`.

```csharp
var options = new JsonSerializerOptions();
options.Converters.Add(new UnitJsonConverter());

string json = JsonSerializer.Serialize(Unit.Value, options); // "null"
Unit back = JsonSerializer.Deserialize<Unit>(json, options); // Unit.Value
```

#### Newtonsoft.Json

```csharp
var settings = new JsonSerializerSettings();
settings.Converters.Add(new UnitNewtonsoftConverter());

string json = JsonConvert.SerializeObject(Unit.Value, settings); // "null"
Unit back = JsonConvert.DeserializeObject<Unit>(json, settings); // Unit.Value
```

### Comparers

```csharp
var dict = new Dictionary<Unit, string>(UnitEqualityComparer.Instance);
dict[Unit.Value] = "only entry";

var sorted = new SortedSet<Unit>(UnitComparer.Instance);
```

---

## Why Unit?

| Scenario | Without Unit | With Unit |
|---|---|---|
| Generic command handler | `IHandler<TCommand, object>` (lies about the return type) | `IHandler<TCommand, Unit>` |
| Functional pipeline | Can't mix `Action` and `Func` | `.ToFunc()` bridges the gap |
| `Task` normalization | `Task` and `Task<T>` are incompatible in generic code | `.AsUnit()` unifies them |
| Discriminated union member | No clean representation of "no data" | `Unit` is the conventional choice |

---

## Target Framework

`netstandard2.0` — compatible with .NET Framework 4.6.1+, .NET Core 2.0+, .NET 5+.

---

## License

MIT
