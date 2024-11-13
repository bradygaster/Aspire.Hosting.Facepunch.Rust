namespace Aspire.Hosting;

public static class HostingExtensions
{
    public static RustServerBuilder AddRustServer(this IDistributedApplicationBuilder builder, string rustServerName)
    {
        var containerResourceBuilder = builder.AddContainer(rustServerName, "didstopia/rust-server");

        return new RustServerBuilder(containerResourceBuilder)
                                .WithLocalFilesDirectory("rust")
                                .WithServerIdentity("samplerustgameserver")
                                .WithRustServerName("Sample Rust Server in .NET Aspire")
                                .WithServerPort(28015)
                                .WithoutOxide()
                                .WithoutRcon()
                                .WithMode(StartMode.UpdateAndRun);
    }
}

