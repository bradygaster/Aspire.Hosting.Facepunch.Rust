# Aspire.Hosting.Facepunch.Rust

This repository contains an example of how to build an integration for hosting a Rust game server using .NET Aspire. 

This example makes use of the [Didstopia/rust-server Docker image](https://github.com/Didstopia/rust-server), which is a Rust server image that is available on Docker Hub. Not all of the individual environment variables have been implemented yet, but the most important ones are there. For more information on how the Didstopia image works, please refer to their [blog](https://rust.didscraft.com/rust-server-on-linux-using-docker/) post on the topic.

## How can I use this integration? 

This integration is useful for anyone who wants to host a Rust game server for the purposes of learning to play on a private server where you won't be in danger of being raided by other players or from getting eaten by animals or dying of thirst. If you're thinking of building an Oxide modded server, this integration will also be useful for you. You can use this Aspire integration to host a Rust server on your own machine so you can work on your plugins and mods without having to pay for a server. If building plugins is your thing, you can also use this integration to test your plugins in a controlled environment before deploying them to a live server, and we've given you a sample plugin to get you started. 

## Basic usage

The most basic usage of the integration just fires up a new Rust server with some default settings. This will take a *long* time to run, as the Rust server will need to download about 8GB of files before it starts. Be patient. 

> Note, this is the code in the `App Host` example project: 

```csharp
var builder = DistributedApplication.CreateBuilder(args);

builder.AddRustServer("rustserver");

builder.Build().Run();
```

## Second usage, once you have the game files

Once you have the game files downloaded, you can specify that you want to run the server without updating it. This will significantly reduce the time it takes to start the server. But still, be patient. This extended example also shows how to change the name of the directory where the server files are stored locally. This directory is where the server files are stored on your local machine so they can be mounted in the Docker container.

> Note: The sample ships using the `rustfiles` directory, and the `gitignore` file is set to ignore this directory. If you change the directory name, you will need to update the `.gitignore` file to reflect the new directory name or you'll inadvertently commit lots of files to your repository.

```csharp
var builder = DistributedApplication.CreateBuilder(args);

builder.AddRustServer("rustserver")
       .WithMode(StartMode.RunWithoutUpdating)
       .WithLocalFilesDirectory("rustfiles");

builder.Build().Run();
```

## Enabling Oxide and adding a plugin

Once you want to try your hand at building plugins, change the code to first enable Oxide and then, set the directory from which the server will load plugins. The example code comes with a sample plugin that will send a message to a user when they join the server. 

```csharp
var builder = DistributedApplication.CreateBuilder(args);

builder.AddRustServer("rustserver")
       .WithMode(StartMode.RunWithoutUpdating)
       .WithLocalFilesDirectory("rustfiles")
       .WithOxide()
       .WithPluginDirectory("plugins");

builder.Build().Run();
```