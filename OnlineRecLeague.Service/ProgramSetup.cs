using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OnlineRecLeague.AppData;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace OnlineRecLeague
{
	public class ProgramSetup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddLogging();
			services.AddMvcCore().AddJsonFormatters(FixJsonCamelCasing);

			services
				.AddHealthChecks()
				.AddCheck<AppDataHealthCheck>("AppData");

			services.AddDistributedMemoryCache();
			services.AddLocalization();

			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromDays(1);
				options.Cookie.HttpOnly = true;
			});

			services.Configure<RequestLocalizationOptions>(options =>
			{
				var supportedCultures = new List<CultureInfo>
					{
						new CultureInfo("en-US"),
						new CultureInfo("fr"),
					};

				options.DefaultRequestCulture = new RequestCulture("en-US");
				options.SupportedCultures = supportedCultures;
				options.SupportedUICultures = supportedCultures;
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			app.UseStaticFiles(StaticFileOptions);
			app.UseSession();
			app.UseMiddleware(typeof(ServiceRequestExceptionHandler));
			app.UseMvc();
			app.UseHealthChecks("/api/health");
		}

		private void FixJsonCamelCasing(JsonSerializerSettings settings)
		{
			// this unsets the default behavior (camelCase); "what you see is what you get" is now default
			if (settings.ContractResolver is DefaultContractResolver resolver)
			{
				resolver.NamingStrategy = null;
			}
		}

		private static string ProgramDirectory { get; } = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;
		private static StaticFileOptions StaticFileOptions = new StaticFileOptions
		{
			FileProvider = new PhysicalFileProvider(Path.Combine(ProgramDirectory, "Assets")),
			RequestPath = "/assets",
		};
	}
}
