using Microsoft.Extensions.Configuration;
using System.IO;

namespace OnlineRecLeague
{
	public interface ISettings
	{
		int WebPort { get; }
		string LogFile { get; }

		string DatabaseUser { get; }
		string DatabasePassword { get; }

		string DatabaseHost { get; }
		int DatabasePort { get; }
		string DatabaseName { get; }

		string SuperadminUsername { get; }
		string SuperadminPassword { get; }
	}

	public class Settings : ISettings
	{
		public Settings(IConfigurationRoot configuration)
		{
			_configuration = configuration;
		}

		public int WebPort => _configuration.GetValue<int>("WebPort");
		public string LogFile => _configuration.GetValue<string>("LogFile") ?? "app.log";

		public string DatabaseUser => _configuration.GetValue<string>("AppDataUser");
		public string DatabasePassword => _configuration.GetValue<string>("AppDataPassword");

		public string DatabaseHost => _configuration.GetValue<string>("AppDataHost");
		public int DatabasePort => _configuration.GetValue<int>("AppDataPort");

		public string DatabaseName => _configuration.GetValue<string>("AppDatabaseName");

		public string SuperadminUsername => _configuration.GetValue<string>("SuperadminUsername");
		public string SuperadminPassword => _configuration.GetValue<string>("SuperadminPassword");

		public string ExecutablePath => Directory.GetCurrentDirectory();

		private readonly IConfigurationRoot _configuration;
	}
}
