using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace OnlineRecLeague
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Settings = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("Settings.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables()
				.AddCommandLine(args)
				.Build();

			SetupLogging();

			try
			{
				StartWebHost();
			}
			catch (Exception exception)
			{
				Log.Fatal(exception, "Service terminated unexpectedly");
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		private static void SetupLogging()
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
				.Enrich.FromLogContext()
				.WriteTo.Console()
				.WriteTo.RollingFile(Settings.GetValue<string>("LogFile") ?? "app.log")
				.CreateLogger();
		}

		private static void StartWebHost()
		{
			WebHost = new WebHostBuilder()
				.UseKestrel()
				.UseConfiguration(Settings)
				.UseStartup<ProgramSetup>()
				.UseSerilog()
				.UseUrls($"http://*:{Settings.GetValue<int>("WebPort")}")
				.Build();

			WebHost.Run();
		}

		public static IConfigurationRoot Settings { get; private set; }
		public static IWebHost WebHost { get; private set; }
	}
}
