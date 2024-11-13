namespace Aspire.Hosting;

public enum StartMode { UpdateAndRun = 0, Update = 1, RunWithoutUpdating = 2 }

public class RustServerBuilder(IResourceBuilder<ContainerResource> containerResourceBuilder) : IResourceBuilder<ContainerResource>
{
    public IDistributedApplicationBuilder ApplicationBuilder => containerResourceBuilder.ApplicationBuilder;

    public ContainerResource Resource => containerResourceBuilder.Resource;

    public IResourceBuilder<ContainerResource> WithAnnotation<TAnnotation>(TAnnotation annotation, ResourceAnnotationMutationBehavior behavior = ResourceAnnotationMutationBehavior.Append) where TAnnotation : IResourceAnnotation => containerResourceBuilder.WithAnnotation(annotation, behavior);

    public RustServerBuilder WithServerIdentity(string identity = "default")
    {
        containerResourceBuilder = containerResourceBuilder.WithEnvironment("RUST_SERVER_IDENTITY", identity);
        return this;
    }

    public RustServerBuilder WithServerPort(int port = 28015)
    {
        containerResourceBuilder = containerResourceBuilder.WithEnvironment("RUST_SERVER_STARTUP_ARGUMENTS", $"-batchmode -nographics +server.secure 1 +server.port {port.ToString()}")
                   .WithEndpoint(endpointName: "rustclient", callback: endpoint =>
                   {
                       endpoint.Port = port;
                       endpoint.TargetPort = port;
                       endpoint.Protocol = System.Net.Sockets.ProtocolType.Udp;
                       endpoint.IsExternal = true;
                       endpoint.IsProxied = false;
                   });

        return this;
    }

    public RustServerBuilder WithoutOxide()
    {
        containerResourceBuilder = containerResourceBuilder.WithEnvironment("RUST_OXIDE_ENABLED", "0");
        return this;
    }

    public RustServerBuilder WithPluginDirectory(string directoryToMount)
    {
        containerResourceBuilder = containerResourceBuilder.WithBindMount(directoryToMount, "/steamcmd/rust/oxide/plugins");
        return this;
    }

    public RustServerBuilder WithLocalFilesDirectory(string directoryToMount = "rustfiles")
    {
        containerResourceBuilder = containerResourceBuilder.WithBindMount(directoryToMount, "/steamcmd/rust");
        return this;
    }

    public RustServerBuilder WithOxide()
    {
        containerResourceBuilder = containerResourceBuilder.WithEnvironment("RUST_OXIDE_ENABLED", "1");
        return this;
    }

    public RustServerBuilder WithMode(StartMode mode = StartMode.UpdateAndRun)
    {
        containerResourceBuilder = containerResourceBuilder.WithEnvironment("RUST_START_MODE", ((int)mode).ToString());
        return this;
    }

    public RustServerBuilder WithRcon(string password = "default", int rconPort = 28016)
    {
        containerResourceBuilder = containerResourceBuilder.WithEnvironment("RUST_RCON_PASSWORD", password)
                                                           .WithHttpEndpoint(port: rconPort, targetPort: rconPort, name: "rcon");
        return this;
    }

    public RustServerBuilder WithoutRcon(string password = "default", int rconPort = 28016)
    {
        containerResourceBuilder = containerResourceBuilder.WithEnvironment("RUST_RCON_PASSWORD", password)
                                                           .WithHttpEndpoint(port: rconPort, targetPort: rconPort, name: "rcon");
        return this;
    }

    public RustServerBuilder WithRustServerName(string serverName = "default")
    {
        containerResourceBuilder = containerResourceBuilder.WithEnvironment("RUST_SERVER_NAME", serverName);
        return this;
    }
}

