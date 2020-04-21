[![NuGet Status](https://img.shields.io/nuget/v/Altairis.Services.DateProvider.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/Altairis.Services.DateProvider/)

# Altairis.Services.DateProvider

This is solution for working with "current" date and time when `DateTime.Now` won't just fit. It defines the `IDateProvider` interface and several implementations. It targets `netstandard2.0`, so it works with .NET Core as well as with .NET Framework.

## Usage

First, install the latest version using NuGet:

    Install-Package Altairis.Services.DateProvider

Then you can instantiate a particular provider manually. To get current time (trimmed to minute) in Prague, use the following code:

```c#
var p = new TzConvertDateProvider("Central Europe Time", DatePrecision.Minute);
var dt = p.Now;
```

But generally better way is to use IoC/DI and register the particular implementation for `IDateProvider` interface and then injecting it where you need it.

Here I am registering the `TzConvertDateProvider` in the `ConfigureServices` of the `Startup` class in ASP.NET Core application:

```c#
public class Startup {

    public void ConfigureServices(IServiceCollection services) {
        // Add other services here

        // Add date provider
        services.AddSingleton<IDateProvider>(new TzConvertDateProvider("Central Europe Time"));
    }

}
```

To use it, I'll inject it where I need it:

```c#
public class IndexModel : PageModel {
    private readonly IDateProvider dateProvider;

    public IndexModel(IDateProvider dateProvider) {
        this.dateProvider = dateProvider ?? throw new ArgumentNullException(nameof(dateProvider));
    }

    public DateTime ServerDateTime { get; set; }

    public void OnGet() {
        this.ServerDateTime = this.dateProvider.Now;
    }
}

```

## Features

There are two main reasons for this library's existence: Rounding the time and working with time zones.

### Rounding the Time

Getting date and time using .NET `DateTime.Now` or SQL Servers `GETDATE()` is simple and precise. Often _too much_ precíse, actually, as the precision goes well beyond current second. How much? It's hard to say, because it differs depending on the data type used, and it's not the same in C# and database.

Working with miliseconds and beyond may bring simple comparation to solving interval task. So the providers will allow you to trim the precision to last second or even last minute, because that's usually more than enough.

If you import the `Altairis.Services.DateProvider` namespace, the `DateTime`, `DateTimeOffset` and `TimeSpan` will get each a bunch of extension methods:

* `TrimToSecond` will remove anything beyond a second precision.
* `TrimToMinute` will remove anything beyond a minute (resetting minute to `00` seconds).
* `TrimTo` will do the same with configurable precision:
  * `Native` will do nothing and return entire value.
  * `Second` will trim to seconds.
  * `Minute` will trim to minute.

> Please note: This is not rounding to nearest second or minute. So trimming `00:00:59.9999999` to minutes will give you `00:00:00`. It's equivalent of `Math.Floor` for positive numbers.

### Working with time zones

If your server and customers are in single time zone, everything is just fine. But if it differs? You can get your application timezone to be different from server timezone. It's great for cloud settings, where usually everything runs in UTC/GMT.


### Interface and implementations

The `IDateProvider` interface is fairly primitive. It defines just two properties, `Now` and `Today`, which work basically in the same way as the same properties of a `DateTime` struct - will return "current" date and time and "current" date. The trick is how the "current is determined".

There are four implementations:

* The `LocalDateProvider` is the most straightforward of them all. It's just a wrapper around `DateTime.Now` and `DateTime.Today`, so it will return current local computer time. By default, it trims precision to seconds (configurable in constructor). Typical use case: single time zone, but want to be prepared to move to the cloud and/or want just the precision trimming.
* The `UtcDateProvider` is proxy for `DateTime.UtcNow` (with second precision by default). It will return UTC date and time values. Typical use case: Server in local time zone, but worldwide audience, so you want your app to run in UTC.
* The `TzConvertDateProvider` does the time zone conversion for you. It takes the time zone ID (more about them later) and will return time in that time zone, regardless on local server TZ. And will trim the time to a second precision, by default. Typical use case: your server is running in the cloud (and UTC servers), but serving audience in another time zone.
* The `FixedDateProvider` will simply always return the value set in constructor. Not very useful, with exception of testing. In testing, you want your results to be deterministic (predictable), so running your test "always at the same time" may come handy.

### Time Zone IDs

There is [a lot of fun to be had](https://github.com/kdeldycke/awesome-falsehood#dates-and-time) with dates, times and time zones. But for purpose of this text, we rely on .NET to do the heavy lifting. You just need to know the ID of time zone you want to work with.

There is no definitive and authoritative list I can show here, as the various time systems seem to be shifing constantly, with governments introducing the daylight savings here and abolishing it there. Microsoft has a team tracking such things and even then, mistakes happen sometimes, for example by [having a body of water on a map where Poland should be](https://devblogs.microsoft.com/oldnewthing/20061027-00/?p=29213).

Use the `TzList` utility to list time zones currently available on your system.

## Contributor Code of Conduct

This project adheres to No Code of Conduct. We are all adults. We accept anyone's contributions. Nothing else matters.

For more information please visit the [No Code of Conduct](https://github.com/domgetter/NCoC) homepage.