using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Filters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Toolchains.CsProj;
using BenchmarkDotNet.Toolchains.DotNetCli;
using Perfolizer.Horology;

namespace BitsKit.Benchmarks;
internal class MultipleRuntimesConfig : ManualConfig
{
    public MultipleRuntimesConfig(MultipleRuntimesFlags flags, params string[] filters)
    {
        if (File.Exists(@"C:\Program Files\dotnet\dotnet.exe"))
        {
            if (flags.HasFlag(MultipleRuntimesFlags.net6_0_x64))
                AddJob(Job.Default
                    .WithPlatform(Platform.X64)
                    .WithToolchain(CsProjCoreToolchain.From(NetCoreAppSettings
                    .NetCoreApp60
                    .WithCustomDotNetCliPath(@"C:\Program Files\dotnet\dotnet.exe", "64 bit 6.0")))
                    .WithId("64 bit 6.0"));

            if (flags.HasFlag(MultipleRuntimesFlags.net7_0_x64))
                AddJob(Job.Default
                    .WithPlatform(Platform.X64)
                    .WithToolchain(CsProjCoreToolchain.From(NetCoreAppSettings
                    .NetCoreApp70
                    .WithCustomDotNetCliPath(@"C:\Program Files\dotnet\dotnet.exe", "64 bit 7.0")))
                    .WithId("64 bit 7.0"));
        }

        if (File.Exists(@"C:\Program Files (x86)\dotnet\dotnet.exe"))
        {
            if (flags.HasFlag(MultipleRuntimesFlags.net6_0_x86))
                AddJob(Job.Default
                    .WithPlatform(Platform.X86)
                    .WithToolchain(CsProjCoreToolchain.From(NetCoreAppSettings
                    .NetCoreApp60
                    .WithCustomDotNetCliPath(@"C:\Program Files (x86)\dotnet\dotnet.exe", "32 bit 6.0")))
                    .WithId("32 bit 6.0"));

            if (flags.HasFlag(MultipleRuntimesFlags.net7_0_x86))
                AddJob(Job.Default
                    .WithPlatform(Platform.X86)
                    .WithToolchain(CsProjCoreToolchain.From(NetCoreAppSettings
                    .NetCoreApp70
                    .WithCustomDotNetCliPath(@"C:\Program Files (x86)\dotnet\dotnet.exe", "32 bit 7.0")))
                    .WithId("32 bit 7.0"));
        }

        AddLogger(new ConsoleLogger());
        AddExporter(DefaultExporters.Plain);
        AddColumnProvider(DefaultColumnProviders.Instance);        

        if(filters?.Length > 0)
            AddFilter(new AllCategoriesFilter(filters));

        SummaryStyle = new SummaryStyle(null, true, SizeUnit.B, TimeUnit.Nanosecond);
    }

    [Flags]
    public enum MultipleRuntimesFlags
    {
        net6_0_x64 = 1,
        net6_0_x86 = 2,
        net7_0_x64 = 4,
        net7_0_x86 = 8,

        net6_0 = net6_0_x64 | net6_0_x86,
        net7_0 = net7_0_x64 | net7_0_x86,

        all_x64 = net6_0_x64 | net7_0_x64,
        all_x86 = net6_0_x86 | net7_0_x86,
        all = all_x86 | all_x64
    }
}
