namespace SmartMirror
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using SmartMirror.Dmi;
	using SmartMirror.Google.Authentication;
	using SmartMirror.Google.Calendar;
	using SmartMirror.Sonos.Authentication;
	using SmartMirror.Sonos.Subscription;
	using SmartMirror.Tado.Authentication;
	using SmartMirror.Tado.Basic;
	using SmartMirror.Tado.Home;
	using SmartMirror.Tado.Home.Zones;

	public class Startup
	{
		private const string CorsPolicy = "CorsPolicy";

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			// Utils
			services.AddSingleton<IClock, SystemClock>();

			// Tado
			services.AddSingleton<ITadoAccessTokenProvider, TadoAccessTokenProvider>();
			services.AddTransient<ITadoBasicInfoProvider, TadoBasicInfoProvider>();
			services.AddTransient<ITadoHomeProvider, TadoHomeProvider>();
			services.AddTransient<ITadoZoneFetcher, TadoZoneFetcher>();
			services.AddTransient<ITadoZoneProvider, TadoZoneProvider>();

			// Google
			services.AddSingleton<IGoogleServiceAccountProvider, GoogleServiceAccountProvider>();
			services.AddSingleton<IGoogleCredentialsProvider, GoogleCredentialsProvider>();
			services.AddSingleton<ICalendarServiceProvider, CalendarServiceProvider>();
			services.AddTransient<ICalendarEventProvider, CalendarEventProvider>();

			// Sonos
			services.AddSingleton<ISonosAccessTokenProvider, SonosAccessTokenProvider>();
			services.AddSingleton<ISonosSubscriptionHandler, SonosSubscriptionHandler>();

			// Dmi
			services.AddTransient<IPollenProvider, DmiPollenProvider>();
			services.AddTransient<IDmiPollenFetcher, DmiPhantomPollenFetcher>();
			services.AddTransient<IDmiPollenReader, DmiPollenReader>();

			services.AddMvc().AddNewtonsoftJson();

			services.AddCors(options =>
			{
				options.AddPolicy(CorsPolicy,
					builder => builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader());
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(CorsPolicy);

			//app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
