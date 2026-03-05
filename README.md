# Unit

`Unit` for C# (`Nitpick.Functional.UnitType`) plus optional JSON converters.

This repository contains three libraries:

- `Nitpick.Functional.UnitType`: core `Unit` type
- `Nitpick.Functional.UnitType.SystemTextJson`: `System.Text.Json` converter for `Unit`
- `Nitpick.Functional.UnitType.NewtonsoftJson`: `Newtonsoft.Json` converter for `Unit`

## Packages

```bash
dotnet add package Nitpick.Functional.UnitType
dotnet add package Nitpick.Functional.UnitType.SystemTextJson
dotnet add package Nitpick.Functional.UnitType.NewtonsoftJson
```

Install only the converter package you need for your serializer.

## Core usage

```csharp
using Nitpick.Functional;

Unit value = Unit.Value;
Unit fromTuple = default(ValueTuple);
ValueTuple tuple = Unit.Value;

Console.WriteLine(Unit.Value); // ()
```

`Unit` is a `readonly struct` with a single logical value and implements:

- `IEquatable<Unit>`
- `IComparable<Unit>`

## JSON converters

Both converters serialize `Unit` as JSON `null` and accept only JSON `null` when deserializing.

### System.Text.Json

```csharp
using Nitpick.Functional;
using System.Text.Json;

var options = new JsonSerializerOptions();
options.Converters.Add(UnitJsonConverter.Instance);

string json = JsonSerializer.Serialize(Unit.Value, options); // "null"
Unit unit = JsonSerializer.Deserialize<Unit>("null", options);
```

### Newtonsoft.Json

```csharp
using Nitpick.Functional;
using Newtonsoft.Json;

var settings = new JsonSerializerSettings();
settings.Converters.Add(new UnitJsonConverter());

string json = JsonConvert.SerializeObject(Unit.Value, settings); // "null"
Unit unit = JsonConvert.DeserializeObject<Unit>("null", settings);
```

## Repository layout

- `src/Nitpick.Functional.UnitType`
- `src/Nitpick.Functional.UnitType.SystemTextJson`
- `src/Nitpick.Functional.UnitType.NewtonsoftJson`
- `tests/Nitpick.Functional.UnitType.Tests`
- `tests/Nitpick.Functional.UnitType.SystemTextJson.Tests`
- `tests/Nitpick.Functional.UnitType.NewtonsoftJson.Tests`

## Development

Prerequisite: .NET SDK `10.0.100` (see `global.json`).

```bash
dotnet restore
dotnet build Nitpick.Functional.UnitType.slnx
dotnet test Nitpick.Functional.UnitType.slnx
```

Formatting:

```bash
dotnet tool restore
dotnet csharpier check .
dotnet csharpier format .
```

## Release

Version is defined in `Version.props`.

CI release flow:

1. Set `<Version>` in `Version.props`.
2. Create and push tag `v<Version>` (for example: `v1.2.0`).
3. Publish a GitHub release for that tag.
4. `finish_release` workflow publishes packages to NuGet.org and GitHub Packages.

## License

MIT
