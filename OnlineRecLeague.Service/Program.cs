using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using OnlineRecLeague.AppData;
using Serilog;
using Serilog.Events;

namespace OnlineRecLeague
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Settings = new ConfigurationBuilder()
				.SetBasePath(Directory)
				.AddJsonFile("Settings.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables()
				.AddCommandLine(args)
				.Build();

			SetupLogging();


			if (Settings.GetValue<bool>("CreateSchema"))
			{
				CreateSchema();
			}
			else
			{
				StartWebHost();
			}
		}

		private static void CreateSchema()
		{
			using (var streamWriter = new StreamWriter(Path.Combine(System.IO.Directory.GetCurrentDirectory(), Settings.GetValue<string>("SchemaOutput"), "schema.sql")))
			{
				streamWriter.Write(AppDataSchema.CreateSchemaScript());
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
			try
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
			catch (Exception exception)
			{
				Log.Fatal(exception, "Service terminated unexpectedly");
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		public static string Directory { get; } = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;

		public static IConfigurationRoot Settings { get; private set; }
		public static IWebHost WebHost { get; private set; }
	}
}
