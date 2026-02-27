using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserListViewer.Configuration;
using UserListViewer.Data;

namespace UserListViewer
{
    /// <summary>
    /// Main entry point for the application with dependency injection support.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var host = CreateHostBuilder().Build();
            
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var mainForm = services.GetRequiredService<Form1>();
                Application.Run(mainForm);
            }
        }

        /// <summary>
        /// Creates and configures the host builder with dependency injection.
        /// </summary>
        private static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    // Configure settings
                    services.Configure<DatabaseSettings>(
                        context.Configuration.GetSection(nameof(DatabaseSettings)));

                    // Register repositories
                    services.AddScoped<IUserRepository, UserRepository>();

                    // Register forms
                    services.AddScoped<Form1>();
                });
        }
    }
}
