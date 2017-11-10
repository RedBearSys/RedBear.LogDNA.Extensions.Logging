# RedBear.LogDNA.Extensions.Logging
LogDNA provider for [Microsoft.Extensions.Logging](https://www.nuget.org/packages/Microsoft.Extensions.Logging) written in .NET Standard 2.0.

## Installation

```
Install-Package RedBear.LogDNA.Extensions.Logging
```

## Configuring the provider

To add the LogDNA provider in **.NET Core 2.0+**:

```csharp
public void ConfigureServices(IServiceCollection services)
  {
      services.AddLogging(loggingBuilder =>
      	loggingBuilder.AddLogDNA("ingestion_key", LogLevel.Debug));
      
      // Other services ...
  }
```

Or in **.NET Core 1.x**:

```csharp
  public void Configure(IApplicationBuilder app,
                        IHostingEnvironment env,
                        ILoggerFactory loggerfactory,
                        IApplicationLifetime appLifetime)
  {
      loggerfactory.AddLogDNA("ingestion_key", LogLevel.Debug);
      
      // Ensure any buffered events are sent at shutdown
      appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
```

## Host tags

Host tags can be applied using the overload of `AddLogDNA`:

```csharp
loggerfactory.AddLogDNA("ingestion_key", LogLevel.Debug, new [] { "tag1", "tag2" });
```

## Logging enumerables

To ensure that enumerables appear as follows in LogDNA:

![An Array](docs/array.png)

log any enumerables using the `EnumerableWrapper<T>` class:

```csharp
var array = new string[] { "one", "two", "three" };
logger.LogInformation("An array", new EnumerableWrapper<string>(array));
```

## Notes

Please remember that indexing of log entries only happens on paid accounts. This means you **won't** see JSON representations of objects or coloured highlighting of `INFO`, `WARN`, etc, if you are using a free account.