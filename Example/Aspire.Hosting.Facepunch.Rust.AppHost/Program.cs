var builder = DistributedApplication.CreateBuilder(args);

builder.AddRustServer("rustserver");

builder.Build().Run();
