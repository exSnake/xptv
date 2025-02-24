using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Serilog;
using xptv.Core.Application.Channels.Services.Files;
using xptv.Core.Application.Channels.Services.Groups;
using xptv.Core.Application.Channels.Services.Parsing;
using xptv.Core.Application.Common.Pagination;
using xptv.Presentation.Channels.ViewModels;
using xptv.Presentation.Common.Components.Loading.ViewModels;
using xptv.Presentation.Common.Components.Loading.Views;
using xptv.Presentation.Player.ViewModels;
using xptv.Presentation.Player.Views;

namespace xptv
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.log");
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            System.Diagnostics.Debug.WriteLine($"Log file path: {logPath}");

            Log.Information("=== Application Starting ===");
            Log.Information("Log File Path: {LogPath}", logPath);

            try
            {
                var builder = MauiApp.CreateBuilder();

                // Add global exception handling
                AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                {
                    var ex = e.ExceptionObject as Exception;
                    System.Diagnostics.Debug.WriteLine($"Unhandled Exception: {ex?.Message}");
                    System.Diagnostics.Debug.WriteLine($"StackTrace: {ex?.StackTrace}");
                };

                builder
                    .UseMauiApp<App>()
                    .UseMauiCommunityToolkitMediaElement()
                    .ConfigureFonts(fonts =>
                    {
                        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    });

                // Aggiungi il servizio per la lettura dei file m3u
                builder.Services.AddSingleton<IM3UService, M3UService>();
                builder.Services.AddSingleton<IM3UFileService, M3UFileService>();
                builder.Services.AddSingleton<IPaginationService, PaginationService>();
                builder.Services.AddSingleton<IChannelGroupService, ChannelGroupService>();

                // Components
                builder.Services.AddTransient<LoadingOverlayView>();
                builder.Services.AddTransient<ILoadingOverlayViewModel, LoadingOverlayViewModel>();

                // Main
                builder.Services.AddSingleton<AppShell>();

                builder.Services.AddSingleton<ChannelsPage>();
                builder.Services.AddSingleton<ChannelsViewModel>();

                builder.Services.AddTransient<VideoPlayerPage>();
                builder.Services.AddTransient<VideoPlayerViewModel>();

                

                System.Diagnostics.Debug.WriteLine("Services registered successfully");

#if DEBUG
                builder.Logging.AddDebug();
                builder.Logging.AddSerilog();
                builder.Logging.SetMinimumLevel(LogLevel.Trace);
                builder.Services.AddLogging(logging =>
                {
                    logging.AddConsole();
                    logging.AddDebug();
                });
#endif
                var app = builder.Build();
                System.Diagnostics.Debug.WriteLine("Application built successfully");
                return app;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"Critical error in CreateMauiApp: {ex.Message}"
                );
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
